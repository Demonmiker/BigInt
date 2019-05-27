using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;
using System.Numerics;


namespace BigIntLibrary
{
    /// <summary>
    /// класс реализующий длинную арифметику
    /// </summary>
    public class BigInt : IComparable<BigInt>, ICloneable
    {
        #region Структура

        int size;

        /// <summary>
        /// знак числа true если негативное
        /// </summary>
        public bool sign { get; protected set; } = false;

        /// <summary>
        /// система счисления
        /// </summary>
        public readonly static BigInt BASE;

        /// <summary>
        /// система счисления
        /// </summary>
        static ulong BASEul;

        /// <summary>
        /// структура где хранится числа
        /// </summary>
        List<uint> value = new List<uint>(); // значения (лимбы)

        
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

        public BigInt(int num)
        {
            size = 1;
            value[0] = ((uint)Math.Abs(num));
            sign = (num < 0);
        }

        public BigInt(long num)
        {

            ulong snum = (ulong)Math.Abs(num);
            sign = num < 0;
            size = 2;
            value[0] = (uint)(snum);
            value.Add((uint)(snum >> 32));
        }

        public BigInt(ulong num)
        {
            size = 2;
            value[0] = ((uint)num);
            value.Add((uint)(num >> 32));
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

        /// <summary>
        /// операция сложения
        /// </summary>
        /// <param name="a">слагаемое</param>
        /// <param name="b">слагаемое</param>
        /// <returns>сумма</returns>
        public static BigInt operator +(BigInt a,BigInt b)
        {
            return a.sign == b.sign ? BigInt.SignOp(a, b) : BigInt.DiffSignOp(a, b);
        }

        /// <summary>
        /// операция отрицания
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static BigInt operator -(BigInt a,BigInt b)
        {
            return a.sign != b.sign ? BigInt.SignOp(a, b) : BigInt.DiffSignOp(a, b);
        }


        #endregion

        #region Умножение

        /// <summary>
        /// операция умножения BigInt на uint (не используется)
        /// </summary>
        /// <param name="u"></param>
        /// <param name="v"></param>
        /// <param name="z"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        private static uint MulN1(BigInt u, uint v, out BigInt z, ulong s = 0ul)
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

        /// <summary>
        /// умножение на систему счисления
        /// </summary>
        /// <param name="a">количество операций</param>
        private void Shift(int a)
        {
            for (int i = 0; i < a; i++)
            {
                value.Insert(0,0u);
                size++;
            } 
        }

        /// <summary>
        /// умножение на систему счисления с выдачей нового BigInt
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        private BigInt GetShift(int a)
        {
            BigInt b = this.Clone() as BigInt;
            b.Shift(a);
            return b;
        }
      
        /// <summary>
        /// оерация умножения
        /// </summary>
        /// <param name="u">множитель</param>
        /// <param name="v">множитель</param>
        /// <returns>произведение</returns>
        private static BigInt MulTry(BigInt u,BigInt v)
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

        /// <summary>
        /// в теории более быстрая операция умножения (не работает и не используется)
        /// </summary>
        /// <param name="u"></param>
        /// <param name="v"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        private static uint Mul_MN(BigInt u, BigInt v, out BigInt z)
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

        /// <summary>
        /// умножение 
        /// </summary>
        /// <param name="u"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        static BigInt Mul(BigInt u, BigInt v)
        {
            u = u.Clone() as BigInt;
            v = v.Clone() as BigInt;
            if (u < v)
                Swap(ref u, ref v);
            return MulTry(u, v);
        }

        /// <summary>
        /// операция умножения
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static BigInt operator * (BigInt a,BigInt b)
        {
            BigInt res = BigInt.MulTry(a, b);
            res.sign = a.sign ^ b.sign;
            return res;
        }
        #endregion

        #region Деление
        /// <summary>
        /// операция для преведения к строке (остаток от 10)
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private static long ModulusOnShort(BigInt a, uint b)
        {
            if (a.size == 1 && a.value[0] == 0)
                return -1;
            long carry = 0;
            for (int i = (int)a.size - 1; i >= 0; --i)
            {
                long cur = a.value[i] + carry * (uint.MaxValue + 1L);
                a.value[i] = (uint)(cur / b);
                carry = (long)(cur % b);
            }
            //
            while (a.size > 1 && a.value[a.value.Count - 1] == 0)
            { a.value.RemoveAt(a.value.Count - 1); a.size--; }
            return carry;
        }

        /// <summary>
        /// деление BigInt на uint
        /// </summary>
        /// <param name="u"></param>
        /// <param name="v"></param>
        /// <param name="r">остаток</param>
        /// <returns></returns>
        private static BigInt DivN_1(BigInt u,uint v,out uint r)
        {
            u = u.Clone() as BigInt;
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

        /// <summary>
        /// деление 3 лимба на два лимба
        /// </summary>
        /// <param name="u"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        private static uint DIV_3_BY_2(BigInt u,BigInt d)
        { 
            
            //
            if (u.size != 3 || d.size != 2)
                throw new Exception("Не 3 и 2");
            if ((d.value[1] >> 31) != 1)
                throw new Exception("Не норма");
            //
            ulong U = glue(u.value[2], u.value[1]);
            ulong D = glue(d.value[1], d.value[0]);
            ulong Q = U / d.value[1];
            uint r0, r1, q;
            ulong R;
            if(u.value[2]==d.value[1])
            {
                uint s1 = d.value[0] - u.value[1];
                ulong S = ((ulong)s1 << 32) - u.value[0];
                if(S <= D)
                {
                    q = uint.MaxValue;
                    R = D - S;
                    r1 = (uint)(R >> 32);
                    r0 = (uint)(R);
                }
                else
                {
                    q = uint.MaxValue-1;
                    R = 2*D - S;
                    r1 = (uint)(R >> 32);
                    r0 = (uint)(R);
                }
                return q;
            }
            ulong DQ = Q * d.value[0];
            R = ((U - Q * d.value[1]) << 32) | u.value[0];
            if(R < DQ)
            {
                --Q; R += D;
                if(R >=D && R < DQ)
                {
                    --Q; R += D;
                }
            }
            R -= DQ;
            r1 = (uint)(R >> 32);
            r0 = (uint)(R);
            q = (uint)Q;
            return q;
        }

        /// <summary>
        /// операция деление
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static BigInt operator / (BigInt a, BigInt b)
        {
            if (b == new BigInt())
                throw new Exception("деление на ноль");
            if (a < b)
                return new BigInt();
            BigInt res;
            if((b.size)==1)
            {
                res = BigInt.DivN_1(a, b.value[0], out uint r);
                res.sign = a.sign ^ b.sign;
            }
            else
            {
                res = BigInt.Div(a, b);
                res.sign = a.sign ^ b.sign;
            }
            return res;
        }

        /// <summary>
        /// деление
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private static BigInt Div(BigInt a,BigInt b)
        {
            BigInt v = b.Clone() as BigInt;v.sign = false;
            BigInt u = a.Clone() as BigInt;u.sign = false;
            u.Normalize(v.Normalize());
            return DIV_M_BY_N(u, v, out BigInt r);

        }

        /// <summary>
        /// нормализация 
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private int Normalize(int num = -1)
        {
            if (num == -1)
            {
                num = 0;
                uint dn = this.value[size - 1]; uint bd2 = (uint)(BASEul / 2);
                while (dn < bd2) { num++; dn = dn << 1; }
            }
            if (num == 0)
                return 0;
            uint cur = 0; uint next = 0;
            for (int i = 0; i < size; i++)
            {
                next = value[i] >> 32 - num;
                value[i] = value[i] << num;
                value[i] = value[i] | cur;
                cur = next;


            }
            if (cur != 0)
            { this.value.Add(cur); size++; }
            return num;
        }

        /// <summary>
        /// операция деление для чисел с размером M и N M>N
        /// </summary>
        /// <param name="U"></param>
        /// <param name="D"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        private static BigInt DIV_M_BY_N(BigInt U,BigInt D,out BigInt r)
        {
           
            int m = U.size; int n = D.size;
            if (U < D) { r = D - U; return new BigInt(); }//
            int k = m - n;
            List<uint> q = new List<uint>();
            for (int i = 0; i < k + 1; i++) q.Add(0u);
            BigInt Q = new BigInt();
            Q.value = q; Q.size = k + 1;
            if (U >= D.GetShift(k))
            {
                q[k] = 1;
                U = U - D.GetShift(k);
            }
            else { q.RemoveAt(q.Count - 1); Q.size--; }
            while(k>0)
            {
                k = k - 1;
                if (glue(U.value[n + k], U.value[n + k - 1]) == glue(D.value[n - 1], D.value[n - 2]))
                    q[k] = uint.MaxValue;
                else
                {
                    // H / L
                    BigInt H = Bigglue(U.value[n + k], U.value[n + k - 1], U.value[n + k - 2]);
                    BigInt L = Bigglue(D.value[n - 1], D.value[n - 2]);
                    //H.Normalize(L.Normalize());
                     q[k] = DIV_3_BY_2(H,L);
                }
                // Проверка
                BigInteger uu = BigInteger.Parse(U.ToString());
                BigInteger qq = BigInteger.Parse(q[k].ToString());
                BigInteger dd = BigInteger.Parse(D.GetShift(k).ToString());
                uu = uu - (qq * dd);
                //

                U = U - BigInt.Mul(new BigInt(q[k]), D.GetShift(k));
                if (U.sign)
                {
                    U = U + D;
                    q[k] = q[k] - 1;
                }
            }
            r = U;
            return Q;
        }

        /// <summary>
        /// операция сборки числа из лимбов
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private static ulong glue(uint a,uint b)
        {
            return ((ulong)a << 32) | b;
        }
        /// <summary>
        /// операция сборки числа из лимбов
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private static BigInt Bigglue(uint a,uint b)
        {
            BigInt d = new BigInt();
            d.value = new List<uint>() { b, a };
            d.size = 2;
            return d;
        }
        /// <summary>
        /// операция сборки числа из лимбов
        /// </summary>
        /// <param name="c"></param>
        /// <param name="b"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        private static BigInt Bigglue (uint c,uint b,uint a)
        {
            BigInt d = new BigInt();
            d.value = new List<uint>() { a, b ,c};
            d.size = 3;
            return d;
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

        /// <summary>
        /// перевод в строку
        /// </summary>
        /// <returns>строчное представление</returns>
        public override string ToString()
        {
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

        /// <summary>
        /// строка для отладки
        /// </summary>
        /// <returns></returns>
        private string DebugString()
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

        /// <summary>
        /// получание числа из строки
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
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
            value.Clear();
            size = 1;
            value.Add(  (uint)Abs(num));
            sign = num < 0;
        }

        public void Set(long num)
        {
            value.Clear();
            ulong snum = (ulong)Math.Abs(num);
            sign = num < 0;
            size = 2;
            value.Add((uint)snum);
            value.Add((uint)(snum >> 32));
        }

        public void Set(ulong num)
        {
            value.Clear();
            size = 2;
            value.Add((uint)num);
            value.Add((uint)(num >> 32));
        }

        public void Set(uint num)
        {
            value.Clear();
            size = 1;
            value.Add(num);
        }

       
        #endregion

        #region Другое
        static void Swap<T>(ref T a, ref T b)
        {
            T buf = a;
            a = b;
            b = buf;
        }

        /// <summary>
        /// выдать клон объекта
        /// </summary>
        /// <returns></returns>
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
            if (value.Count <= 1)
                return;
            while(value[value.Count - 1] == 0 )
            {
                value.RemoveAt(value.Count - 1);
                size--;
                if (value.Count == 1)
                    return;
            }
          
        }
        #endregion

       
    }
}
