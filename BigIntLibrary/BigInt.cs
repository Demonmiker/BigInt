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
        int size;

        bool sign;

        // base = uint.MaxValue + 1;

        List<uint> value = new List<uint>(); // значения (лимбы)
        private static int baza = 9;

        //int memory; можно создать свойство указывающее а value.Lenght
        #endregion

        #region Конструкторы
        public BigInt()
        {
            size = 1;

            value.Add(0u);

        }
        #endregion

        #region Добавление и Вычитание

        //УБРАТЬ ПАБЛИК ПОЗЖЕ
        /// <summary>
        /// операция на одинаковые знвки
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="sign"></param>
        /// <returns></returns>
        public static BigInt SignOp(BigInt a, BigInt b) // true если позитив
        {
            //a = a.Clone() as BigInt;
            //b = b.Clone() as BigInt;
            if (a.size < b.size) Swap(ref a, ref b);
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
            return c;
        }

        //УБРАТЬ ПАБЛИК ПОЗЖЕ
        /// <summary>
        /// Операция на разные знаки
        /// </summary>
        /// <param name="a">первое число</param>
        /// <param name="b">второе число</param>
        /// <returns></returns>
        public static BigInt DiffSignOp(BigInt a, BigInt b)
        {
            //a = a.Clone() as BigInt;
            // b = b.Clone() as BigInt;
            if (a.size < b.size) Swap(ref a, ref b);
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
            return c;
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
                this.value.Insert(0, 0u);
                this.size++;
            }

        }

        public static uint Mul_MN(BigInt u, BigInt v, out BigInt z)
        {
            uint s = BigInt.MulN1(u, v.value[0], out z, 0);
            BigInt buf;
            for (int i = 1; i < v.size; i++)
            {
                s = BigInt.MulN1(u, v.value[i], out buf, s);
                buf.Shift(i);
                z = BigInt.SignOp(z, buf);
            }
            return s;
        }

        public static BigInt Mul(BigInt u, BigInt v)
        {
            u = u.Clone() as BigInt;
            v = v.Clone() as BigInt;
            if (v.size > u.size)
                Swap(ref u, ref v);
            uint carry = BigInt.Mul_MN(u, v, out BigInt z);
            if (carry != 0) { z.value.Add(carry); z.size++; }
            return z;
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
            for (int i = sb.Length - 1; i > -1; i--)
                Out.Append(sb[i]);
            return Out.ToString();
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

        public void Set(int num)
        {
            value[0] = (uint)Abs(num);
            size = num < 0 ? -1 : 1;
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
        #endregion

        public static BigInt Parse(string s)
        {
            int n = s.Length;
            int razr = n / 9 + 1; string num;
            BigInt x = new BigInt(); BigInt res = new BigInt(); BigInt buf = new BigInt(); BigInt Base = new BigInt();
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
            return res;
        }
    }
}
