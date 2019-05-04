using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace BigIntLibrary
{
    public class BigInt : IComparable<BigInt> , ICloneable
    {
        #region Структура
        int size;

        bool sign;

        // base = uint.MaxValue + 1;

        List<uint> value = new List<uint>(); // значения (лимбы)

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
        public static BigInt SignOp(BigInt a,BigInt b) // true если позитив
        {
            if (a.size < b.size) Swap(a, b);
            uint carry = 0;// флаг переноса
            BigInt c = new BigInt();
            
            for(int j=0;j<a.size-1;j++)  
                c.value.Add(0u);
            c.size = c.value.Count;
            int i = 0;
            for (i = 0; i < b.size ; i++)
            {
                c.value[i] = a.value[i] + b.value[i] + carry;
                carry = ((a.value[i] & b.value[i]) | ((a.value[i] | b.value[i]) & (~c.value[i]))) >> 31;
            }
            for (i = i; i < a.size; i++)
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
        public static BigInt DiffSignOp(BigInt a,BigInt b)
        {
            if (a.size < b.size) Swap(a, b);
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
            for (i = i; i < a.size; i++)
            {
                c.value[i] = a.value[i] - borrow;
                borrow = ((~a.value[i] & 0u) | ((~a.value[i] | 0u) & c.value[i])) >> 31;
            }
            return c;
        }
       

        

        #endregion

        #region Умножение

        //УБРАТЬ PUBLIC
        public static uint MulN1(BigInt u,uint v,out BigInt z)
        {
            z = new BigInt();
            ulong s = 0;
            for (int i = 0; i < u.size; i++)
            {
                s += (ulong)u.value[i] * v;
                u.value[i] = (uint)s;
                s >>= 32;
            }
            return (uint)s;
        }
        #endregion

        #region Деление

        #endregion

        #region Сравнение итд

        public int CompareTo(BigInt other)
        {
            if (this.size == other.size)
            {
                for(int i=size-1;i>-1;i--)
                {
                    if(this.value[i]!=other.value[i])
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

        public static bool operator < (BigInt a,BigInt b)
        {
            return a.CompareTo(b) == -1 ? true : false;
        }

        public static bool operator > (BigInt a, BigInt b)
        {
            return a.CompareTo(b) == 1 ? true : false;
        }

        public static bool operator >= (BigInt a, BigInt b)
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
            return base.ToString();
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
        public static void Swap<T>(T a,T b)
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
            foreach (uint a in this.value)
                A.value.Add(a);
            return A as object;
        }


        #endregion
    }
}
