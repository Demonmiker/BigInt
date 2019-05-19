using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigIntLibrary;
using static System.Console;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
namespace BigIntConsoleTest
{
    class Program
    {
        static int[] index = new int[4];
        static void Main(string[] args)
        {
            
            WindowWidth = 200;

            Thread t0 = new Thread(FullTest);
            Thread t1 = new Thread(FullTest);
            Thread t2 = new Thread(FullTest); 
            Thread t3 = new Thread(FullTest);
            t0.Start(0); t1.Start(1);
            t2.Start(2); t3.Start(3);
            while (index[0]<100000 && index[1] < 100000 && index[2] < 100000 && index[3] < 100000)
            {
                Thread.Sleep(3000);
                Clear();
                WriteLine(index[0]);
                WriteLine(index[1]);
                WriteLine(index[2]);
                WriteLine(index[3]);
            }
            ReadKey();
           
        }


        public static void FullTest(object ind)
        {
            int inde = (int)ind;
            for (int i = 0; i < 100000; i++)
            {
                index[inde] = i;
                //Clear();
                //WriteLine(i);
                bool Error = false;
                string s1 = GenerateNum(rnd.Next(1, 800), true);
                string s2 = GenerateNum(rnd.Next(1, 800), true);
                BigInt a1 = BigInt.Parse(s1);
                BigInt b1 = BigInt.Parse(s2);
                BigInteger a2 = BigInteger.Parse(s1);
                BigInteger b2 = BigInteger.Parse(s2);
                BigInt plus1 = a1 + b1;
                BigInteger plus2 = a2 + b2;
                BigInt minus1 = a1 - b1;
                BigInteger minus2 = a2 - b2;
                BigInt mul1 = a1 * b1;
                BigInteger mul2 = a2 * b2;
                BigInt div1 = a1 / b1;
                BigInteger div2 = a2 / b2;
                if (plus1.ToString() != plus2.ToString())
                    Error = true;
                if (minus1.ToString() != minus2.ToString())
                    Error = true;
                if (mul1.ToString() != mul2.ToString())
                    Error = true;
                if (div1.ToString() != div2.ToString())
                    Error = true;
                if(Error)
                {
                    i = -1;
                    break;
                }
            }
        }

        public static void HandTest()
        {
            char oper = ReadLine()[0];
            string s1 = ReadLine();
            string s2 = ReadLine();
            BigInt a = BigInt.Parse(s1);
            BigInt b = BigInt.Parse(s2);
            BigInteger a2= BigInteger.Parse(s1);
            BigInteger b2 = BigInteger.Parse(s2);
            BigInt c = new BigInt();
            BigInteger c2 = new BigInteger();
            switch(oper)
            {
                case '+':
                    c = a + b;
                    c2 = a2 + b2;
                    break;
                case '-':
                    c = a - b;
                    c2 = a2 - b2;
                    break;
                case '/':
                    c = a / b;
                    c2 = a2 / b2;
                    break;
                case '*':
                    c = a * b;
                    c2 = a2 * b2;
                    break;
                default:
                    break;
            }
            if (c.ToString() != c2.ToString())
            {
                Clear();
                WriteLine("c1=" + c);
                WriteLine("c2=" + c2);
                ReadKey();
            }


        }

        static string Bits(uint a)
        {
            string res = "";
            for (int i = 31; i >= 0; i--)
            {
                if ((i + 1) % 4 == 0)
                    res += " ";
                res += (a >> i) % 2;
            }
            return res;
        }

        public static void Divtest()
        {
            for (int i = 0; i < 1000000; i++)
            {
                Clear();
                WriteLine(i);
                bool Error = false;
                string s1 = GenerateNum(rnd.Next(300, 500), true);
                string s2 = GenerateNum(rnd.Next(2, 25), true);
                BigInt a1 = BigInt.Parse(s1);
                BigInt b1 = BigInt.Parse(s2);
                BigInteger a2 = BigInteger.Parse(s1);
                BigInteger b2 = BigInteger.Parse(s2);
                //
                BigInt an = a1.Clone() as BigInt;
                BigInt bn = b1.Clone() as BigInt;
                
                BigInteger c = BigInteger.Parse(an.ToString()) / BigInteger.Parse(bn.ToString());
                BigInt p1 = a1 / b1;
                //
                BigInteger p2 = a2 / b2;
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
                    WriteLine($"BigInt div = {p1}");
                    WriteLine($"   Int div = {p2}\n");
                    ReadKey();
                    LIMBSCOMPARE(p1, p2);
                    //BigInt.Div(a1, b1);
                    ReadKey();

                }

            }

            ReadKey();
        }

        public static void LIMBSCOMPARE(BigInt a,BigInteger bb)
        {
            BigInt b = BigInt.Parse(bb.ToString());
            Clear();
            //WriteLine();
            //WriteLine(a + "\n");
            //WriteLine(bb);
            //WriteLine(b);
            int i = 0;
            for (i = 0; i < a.size & i< b.size; i++)
            {
                WriteLine(a.value[i] + "\t" + b.value[i]);
            }
            while(i<a.size)
                WriteLine(a.value[i++]);
            while (i < b.size)
                WriteLine("\t" + b.value[i++]);
            WriteLine();
            WriteLine(a.size);
            WriteLine(b.size);
        }

       
            
        

      
      

       

    

        public static void MUL_TEST()
        {
           
            for (int i = 0; i < 10000000; i++)
            {
                Clear();
                WriteLine(i);
                bool Error = false;
                string s1 = GenerateNum(rnd.Next(120, 500),true);
                string s2 = GenerateNum(rnd.Next(120, 500), true);
                BigInt a1 = BigInt.Parse(s1);
                BigInt b1 = BigInt.Parse(s2);
                BigInteger a2 = BigInteger.Parse(s1);
                BigInteger b2 = BigInteger.Parse(s2);
                BigInt p1 = a1 * b1;
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
            s.Append(chars[rnd.Next(1, 10)]);
            for (int i = 0; i < Length-1; i++)
            {
                s.Append(chars[rnd.Next(0, 10)]);
            }
            return s.ToString();

        }

       

      

      
    }
}
