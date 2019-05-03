using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace BigIntLibrary
{
    public class BigInt : IComparable<BigInt>
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
        public static BigInt SignOp(BigInt a,BigInt b,bool sign) // true если позитив
        {
            if (a.size < b.size) Swap(a, b);
            uint carry = 0;// флаг переноса
            BigInt c = new BigInt();
            
            for(int i=0;i<a.size-1;i++)  
                c.value.Add(0u);
            for (int i = 0; i < b.size ; i++)
            {
                c.value[i] = a.value[i] + b.value[i] + carry;
                carry = ((a.value[i] & b.value[i]) | ((a.value[i] | b.value[i]) & (~c.value[i]))) >> 31;
            }
            if (carry == 1)
            { c.value.Add(1u); c.size++; }
            return c;
        }

        //УБРАТЬ ПАБЛИК ПОЗЖЕ
        public static BigInt DiffSignOp(BigInt a,BigInt b)
        {
            return new BigInt();
        }
       

        

        #endregion

        #region Умножение

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
                    res += " , ";
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
        #endregion
    }
}
