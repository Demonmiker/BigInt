using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigIntLibrary;
using static System.Console;
using System.Numerics;

namespace BigIntConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            
            WindowWidth = 105;
            MUL_TEST();
        }

        public static void MULN1Test()
        {
            for (int i = 0; i < 1000; i++)
            {
                Clear();
                //WriteLine(i);
                //bool Error = false;
                string s1 = GenerateNum(rnd.Next(11, 27), false);

                BigInt a1 = BigInt.Parse(s1);
                BigInteger a2 = BigInteger.Parse(s1);
                uint b = (uint)rnd.Next(int.MinValue, int.MaxValue);
                BigInt c1;
                //
                uint s = BigInt.MulN1(a1, b, out c1);
                WriteLine(s);
                WriteLine(c1);
                //
                c1.size++;
                c1.value.Add(s);
                WriteLine(c1);
                //
                WriteLine(a2 * b);
                ReadKey();
            }
            
            
          
        }

        public static void MUL_TEST_2()
        {
            BigInteger a = BigInteger.Pow(10, 77);
            BigInteger b = BigInteger.Pow(2, 63);
            BigInt a1 = BigInt.Parse(a.ToString());
            BigInt b1 = BigInt.Parse(b.ToString());
            BigInt c1 = BigInt.Mul(a1, b1);
            BigInteger c = a * b;
            WriteLine(c1);
            WriteLine(c);
            ReadKey();
        }

        public static void MUL_TEST()
        {
           
            for (int i = 0; i < 10000000; i++)
            {
                Clear();
                WriteLine(i);
                bool Error = false;
                string s1 = GenerateNum(rnd.Next(120, 500),false);
                string s2 = GenerateNum(rnd.Next(120, 500),false);
                BigInt a1 = BigInt.Parse(s1);
                BigInt b1 = BigInt.Parse(s2);
                BigInteger a2 = BigInteger.Parse(s1);
                BigInteger b2 = BigInteger.Parse(s2);
                BigInt p1 = BigInt.MulTry(a1, b1);
                BigInteger p2 = a2 * b2;
                if (p1.ToString() != p2.ToString())
                    Error = true;
                if (Error)
                {
                    WriteLine($"a = {s1}");
                    WriteLine($"b = {s2}\n");
                    WriteLine($"BigInt a = {a1}");
                    WriteLine($"BigInt b = {b1}\n");
                    WriteLine($"   Int a = {a2}");
                    WriteLine($"   Int b = {b2}\n");
                    WriteLine($"BigInt mul = {p1}");
                    WriteLine($"   Int mul = {p2}\n");
                    ReadKey();
                    BigInt p;
                    
                    ReadKey();
                    
                }
              
            }
            
            ReadKey();
           
        }

        public static void IO_AND_PLUS_MINUS_TEST()
        {
            
            for (int i = 0; i < 5000000; i++)
            {
                Clear();
                WriteLine(i);
                bool Error = false;
                string s1 = GenerateNum(rnd.Next(15, 800));
                string s2 = GenerateNum(rnd.Next(15, 800));
                BigInt a1= BigInt.Parse(s1);
                BigInt b1 = BigInt.Parse(s2);
                BigInteger a2 = BigInteger.Parse(s1);
                BigInteger b2 = BigInteger.Parse(s2);
                if (a1.ToString() != a2.ToString())
                    Error = true;
                if (b1.ToString() != b2.ToString())
                    Error = true;
                BigInt p1 = a1 + b1;
                BigInteger p2 = a2 + b2;
                if (p1.ToString() != p2.ToString())
                    Error = true;
                bool l1 = a1 < b1;
                bool l2 = a2 < b2;
                if (p1.ToString() != p2.ToString())
                    Error = true;
                BigInt m1 = a1 - b1;
                BigInteger m2 = a2 - b2;
                if (m1.ToString() != m2.ToString())
                    Error = true;
                if(Error)
                {
                    WriteLine($"a = {s1}");
                    WriteLine($"b = {s2}\n");
                    WriteLine($"BigInt a = {a1}");
                    WriteLine($"BigInt b = {b1}\n");
                    WriteLine($"   Int a = {a2}");
                    WriteLine($"   Int b = {b2}\n");
                    WriteLine($"BigInt add = {p1}");
                    WriteLine($"   Int add = {p2}\n");
                    WriteLine($"BigInt < = {l1}");
                    WriteLine($"   Int < = {l2}\n");
                    WriteLine($"BigInt sub = {m1}");
                    WriteLine($"   Int sub = {m2}\n");
                    ReadKey();
                    BigInt p = a1 + b1;
                    BigInt m = a1 - b1;
                    ReadKey();
                }
            }
        }

        static void MulFix()
        {
            BigInt a = new BigInt();
            a = BigInt.Parse("4294967296");
            WriteLine(a.DebugString());
            a = BigInt.Mul(a, a);
            WriteLine(a);
            ReadKey();
        }

        static void Test()
        {
            for (int i = 0; i < 10000; i++)
            {

                WriteLine(i);
                string s = ReadLine();
                BigInteger b = BigInteger.Parse(s);
                string n = b.ToString();
                BigInt v = BigInt.Parse(s);
                string m = v.ToString();


                string q = ReadLine();
                BigInteger d = BigInteger.Parse(q);
                string f = d.ToString();
                BigInt g = BigInt.Parse(q);
                string h = g.ToString();

                BigInt O = new BigInt();
                O = BigInt.Mul(v, g);

                BigInteger P = new BigInteger();
                P = b * d;

                if (O.ToString() != P.ToString())
                {
                    WriteLine(O);
                    WriteLine(P);
                    ReadKey();

                }

            }
            ReadKey();

        }

        static Random rnd = new Random();
        static string chars = "0123456789"; 
        static string GenerateNum(int Length, bool WithNeg=true)
        {
            StringBuilder s = new StringBuilder(Length - 1);
            if(WithNeg)
                if (rnd.Next(0, 2) == 1)
                    s.Append('-');
            for (int i = 0; i < Length; i++)
            {
                s.Append(chars[rnd.Next(0, 10)]);
            }
            return s.ToString();

        }

       

      

      
    }
}
