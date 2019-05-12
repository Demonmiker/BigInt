using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace BigIntLibrary
{
    public class BigInt : IComparable<BigInt>, ICloneable
    {
        #region Структура
        public int size;

        bool sign = false;

        static BigInt BASE;

        static ulong BASEul;

        public List<uint> value = new List<uint>(); // значения (лимбы)

        
        #endregion

        #region Конструкторы
        static BigInt()
        {
            BASE = new BigInt();
            BASE.value.Add(1u);
            BASEul = (ulong)uint.MaxValue + 1;
        }

        public BigInt()
        {
            size = 1;

            value.Add(0u);

        }

        public BigInt(uint num)
        {
            value.Add(0u);
            this.Set(num);
        }

       
   
        #endregion

        #region Добавление и Вычитание

        /// <summary>
        /// операция на одинаковые знаки
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="sign"></param>
        /// <returns></returns>
        static BigInt SignOp(BigInt a, BigInt b) 
        {
            bool swapped = false;
            if (a.size < b.size) { Swap(ref a, ref b);swapped = true; }
            uint carry = 0;// флаг переноса
            BigInt c = new BigInt();

            for (int j = 0; j < a.size - 1; j++)
                c.value.Add(0u);
            c.size = c.value.Count;
            int i = 0;
            for (i = 0; i < b.size; i++)
            {
                c.value[i] = a.value[i] + b.value[i] + carry;
                carry = ((a.value[i] & b.value[i]) | ((a.value[i] | b.value[i]) & (~c.value[i]))) >> 31;
            }
            for (; i < a.size; i++)
            {
                c.value[i] = a.value[i] + carry;
                carry = ((a.value[i] & 0u) | ((a.value[i] | 0u) & (~c.value[i]))) >> 31;
            }
            if (carry == 1)
            { c.value.Add(1u); c.size++; }

            c.sign = swapped ? b.sign : a.sign;
            //c.Norm();
            return c;
        }

        /// <summary>
        /// Операция на разные знаки
        /// </summary>
        /// <param name="a">первое число</param>
        /// <param name="b">второе число</param>
        /// <returns></returns>
        static BigInt DiffSignOp(BigInt a, BigInt b)
        {
            bool swaped = false;
            if (a < b) { Swap(ref a, ref b); swaped = true; }
            uint borrow = 0;// флаг переноса
            BigInt c = new BigInt();
            for (int j = 0; j < a.size - 1; j++)
                c.value.Add(0u);
            c.size = c.value.Count;
            int i;
            for (i = 0; i < b.size; i++)
            {
                c.value[i] = a.value[i] - b.value[i] - borrow;
                borrow = ((~a.value[i] & b.value[i]) | ((~a.value[i] | b.value[i]) & c.value[i])) >> 31;
            }
            for (; i < a.size; i++)
            {
                c.value[i] = a.value[i] - borrow;
                borrow = ((~a.value[i] & 0u) | ((~a.value[i] | 0u) & c.value[i])) >> 31;
            }
            if (swaped)
                c.sign = !b.sign;
            else
                c.sign = a.sign;
            c.Norm();
            return c;
        }

        public static BigInt operator +(BigInt a,BigInt b)
        {
            return a.sign == b.sign ? BigInt.SignOp(a, b) : BigInt.DiffSignOp(a, b);
        }

        public static BigInt operator -(BigInt a,BigInt b)
        {
            return a.sign != b.sign ? BigInt.SignOp(a, b) : BigInt.DiffSignOp(a, b);
        }


        #endregion

        #region Умножение

        //УБРАТЬ PUBLIC
        public static uint MulN1(BigInt u, uint v, out BigInt z, ulong s = 0ul)
        {
            z = new BigInt();
            z.Resize(u.size);
            for (int i = 0; i < u.size; i++)
            {
                s += (ulong)u.value[i] * v;
                z.value[i] = (uint)s;
                s >>= 32;
            }
            return (uint)s;
        }

        void Shift(int a)
        {
            for (int i = 0; i < a; i++)
            {
                value.Insert(0,0u);
                size++;
            } 
        }

        public static BigInt MulTry(BigInt u,BigInt v)
        {
            BigInt z = new BigInt(0u); BigInt buf;
            for (int i = 0; i < v.size; i++)
            {
                uint s =BigInt.MulN1(u, v.value[i], out buf, 0);
                if (s != 0) { buf.size++; buf.value.Add(s); }
                buf.Shift(i);
                z = BigInt.SignOp(z, buf);
            }
            return z;
        }

        public static uint Mul_MN(BigInt u, BigInt v, out BigInt z)
        {
            uint s = BigInt.MulN1(u, v.value[0], out z);
            BigInt buf;
            for (int i = 1; i < v.size; i++)
            {

                s = BigInt.MulN1(u, v.value[i], out buf,z.value[i]);
                buf.Shift(i);
                z = BigInt.SignOp(z, buf);
            }
            return s;
        }


        public static BigInt Mul(BigInt u, BigInt v)
        {
            u = u.Clone() as BigInt;
            v = v.Clone() as BigInt;
            if (u < v)
                Swap(ref u, ref v);
            return MulTry(u, v);
        }


        #endregion

        #region Деление
        public static long ModulusOnShort(BigInt a, uint b)
        {
            if (a.size == 1 && a.value[0] == 0)
                return -1;
            long carry = 0;
            for (int i = (int)a.size - 1; i >= 0; --i)
            {
                long cur = a.value[i] + carry * (uint.MaxValue + 1l);
                a.value[i] = (uint)(cur / b);
                carry = (long)(cur % b);
            }
            //
            while (a.size > 1 && a.value[a.value.Count - 1] == 0)
            { a.value.RemoveAt(a.value.Count - 1); a.size--; }
            return carry;
        }


        public static BigInt DivN_1(BigInt u,uint v,out uint r)
        {
            uint[] ans = new uint[u.value.Count];
            r = 0;
            int j = u.value.Count - 1;
            while(j>=0)
            {
                ulong cur = (ulong)(r) * BASEul + u.value[j];
                ans[j] = (uint)(cur / v);
                r = (uint)(cur % v);
                j--;
            }
            BigInt res = new BigInt(); res.size = ans.Length;
            res.value = ans.ToList();
            return res;
        }

        public static BigInt DivUnSign(BigInt u,BigInt v,out BigInt r)
        {
            //Init
            int n = v.value.Count;
            int m = u.value.Count;
            uint[] tempArray = new uint[m + 1];
            tempArray[m] = 1;
            BigInt q = new BigInt(); q.value = tempArray.ToList();
            //Нормализация
            uint d = (uint)(BASEul / v.value[n - 1] + 1);
            u = BigInt.Mul(u, new BigInt(d));
            v = BigInt.Mul(v, new BigInt(d));
            if(u.value.Count==n+m)
            {
                u.value.Add(0u);
            }
            int j = m;
            while(j>=0)
            {
                ulong cur = (ulong)(u.value[j + n]) * (ulong)(BASEul) + u.value[j + n - 1];
                uint tempq = (uint)(cur / v.value[n - 1]);
                uint tempr = (uint)(cur % v.value[n - 1]);
                do
                {
                    if (tempq == BASEul || (ulong)tempq * (ulong)v.value[n - 2] > BASEul * (ulong)tempr + u.value[j + n - 2])
                    {
                        tempq--;
                        tempr += v.value[n - 1];
                    }
                    else
                    {
                        break;
                    }
                }
                while (tempr < BASEul);
                BigInt u2 = new BigInt();u2.value = u.value.GetRange(j, n + 1);
                u2 = u2 - BigInt.Mul(v, new BigInt(tempq));
                //

                //
                for(int h = j; h < j + n; h++)
                {
                    if(h-j>=u2.value.Count)
                    {
                        u.value[h] = 0;
                    }
                    else
                    {
                        u.value[h] = u2.value[h - j];
                    }
                }
                j--;
                q.Norm();
                r = new BigInt();
                r.value = u.value.GetRange(0, n);
                r = BigInt.DivN_1(r, d, out uint _R);
                return q;

            }

        }
        #endregion

        #region Сравнение итд

        public int CompareTo(BigInt other)
        {
            if (this.size == other.size)
            {
                for (int i = size - 1; i > -1; i--)
                {
                    if (this.value[i] != other.value[i])
                    {
                        if (this.value[i] > other.value[i]) return 1;
                        return -1;
                    }

                }
                return 0;

            }
            else if (this.size > other.size) return 1;
            return -1;
        }

        public static bool operator <(BigInt a, BigInt b)
        {
            return a.CompareTo(b) == -1 ? true : false;
        }

        public static bool operator >(BigInt a, BigInt b)
        {
            return a.CompareTo(b) == 1 ? true : false;
        }

        public static bool operator >=(BigInt a, BigInt b)
        {
            return a.CompareTo(b) != -1 ? true : false;
        }

        public static bool operator <=(BigInt a, BigInt b)
        {
            return a.CompareTo(b) != 1 ? true : false;
        }

        public static bool operator ==(BigInt a, BigInt b)
        {
            return a.CompareTo(b) == 0 ? true : false;
        }

        public static bool operator !=(BigInt a, BigInt b)
        {
            return a.CompareTo(b) != 0 ? true : false;
        }

        #endregion

        #region Перевод в другие типы
        public override string ToString()
        {
            //return DebugString();
            //this.Norm();
            BigInt buf = this.Clone() as BigInt;
            StringBuilder sb = new StringBuilder(string.Empty);
            long mod = BigInt.ModulusOnShort(buf, 10);
            do
            {

                sb.Append(mod);
                mod = BigInt.ModulusOnShort(buf, 10);
            }
            while (mod != -1);
            StringBuilder Out = new StringBuilder();
            if(this.sign) Out.Append('-');
            for (int i = sb.Length - 1; i > -1; i--)
                Out.Append(sb[i]);
            if (Out.ToString() != "1-")
                return Out.ToString();
            else
                return "0";
        }

        public string DebugString()
        {
            string res = size + "; ";
            for (int i = size - 1; i > -1; i--)
            {
                res += value[i];
                if (i != 0)
                    res += "\t";
            }
            return res;

        }

        public static BigInt Parse(string s)
        {
            BigInt res = new BigInt();
            bool neg = false;
            if (s[0]=='-')
            { neg = true; s = s.Substring(1); }       
            int n = s.Length;
            int razr = n / 9;
            if (n % 9 != 0)
                razr++;
            string num;
            BigInt x = new BigInt();  BigInt buf = new BigInt(); BigInt Base = new BigInt();
            x.Set(1); res.Set(0); buf.Set(0); Base.Set(1000000000u);
            for (int i = 1; i <= razr; i++)
            {
                n = s.Length - (i * 9);
                if (n < 0) { num = s.Substring(0, 9 + n); }
                else
                    num = s.Substring(Math.Max(n, 0), 9);
                buf.Set(uint.Parse(num));
                buf = BigInt.Mul(buf, x);
                res = BigInt.SignOp(res, buf);
                x = BigInt.Mul(x, Base);

            }
            res.sign = neg;
            res.Norm();
            return res;
        }

        public void Set(int num)
        {
            value[0] = (uint)Abs(num);
          
        }

        public void Set(long num)
        {

        }

        public void Set(ulong num)
        {

        }



        public void Set(uint num)
        {
            size = 1;
            value[0] = num;
        }

        public void Set(string num)
        {

        }




        #endregion

        #region Другое
        public static void Swap<T>(ref T a, ref T b)
        {
            T buf = a;
            a = b;
            b = buf;
        }

        public object Clone()
        {
            BigInt A = new BigInt();
            A.sign = this.sign;
            A.size = this.size;
            A.value.Clear();
            foreach (uint a in this.value)
                A.value.Add(a);
            return A as object;
        }

        private void Resize(int Newsize)
        {
            this.size = Newsize;
            for (int i = 1; i < Newsize; i++)
            {
                this.value.Add(0u);
            }
        }

        private void Norm()
        {
            if (value.Count == 1)
                return;
            while(value[value.Count - 1] == 0)
            {
                value.RemoveAt(value.Count - 1);
                size--;
            }
          
        }
        #endregion

       
    }
}
