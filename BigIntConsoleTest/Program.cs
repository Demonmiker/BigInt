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
            //BigInt a = new BigInt(); BigInt r = new BigInt();
            //a = BigInt.Parse("4294967296");
            //BigInt.MulN1(a, 1u, out r);
            //return;
            WindowWidth = 105;
            Test();
        }

        static void OutputTest()
        {
            BigInt A = new BigInt(); BigInt B = new BigInt();
            A.Set(uint.Parse(ReadLine()));
            B.Set(uint.Parse(ReadLine()));
            A = BigInt.SignOp(A, B);
            WriteLine(A);
            ReadKey();
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

        static string GenerateNum(int Length)
        {
            string chars = "0123456789"; int pos = 0;
            Random rnd = new Random();
            StringBuilder s = new StringBuilder(Length - 1);
            for (int i = 0; i < Length; i++)
            {
                pos = rnd.Next(0, 10);
                s.Append(chars[pos]);


            }
            return s.ToString();

        }

        static void Mod10Test()
        {
            BigInt A = new BigInt(); BigInt B = new BigInt();
            A.Set(uint.Parse(ReadLine()));
            B.Set(uint.Parse(ReadLine()));
            A = BigInt.SignOp(A, B);
            long mod = 0;
            while (mod != -1)
            {
                mod = BigInt.ModulusOnShort(A, 10);
                WriteLine(mod);
                ReadKey();
            }
            ReadKey();
        }

        static void MulTest1()
        {
            BigInt A = new BigInt(); BigInt B = new BigInt();
            A.Set(uint.Parse(ReadLine()));
            B.Set(uint.Parse(ReadLine()));
            A = BigInt.SignOp(A, B);
            B.Set(uint.Parse(ReadLine()));

            //
            BigInt D = new BigInt(); // ответ

            D = BigInt.Mul(A, B);
            WriteLine(D.DebugString());

            ReadKey();
        }

        static void test1()
        {
            Write("num:");
            BigInt A = new BigInt(), B = new BigInt();
            A.Set(uint.Parse(ReadLine()));
            WriteLine("A -- " + A.DebugString());
            uint b;
            do
            {
                bool plus = true;
                Write("op:");
                plus = ReadLine() == "+" ? true : false;
                Write("num2:");
                b = uint.Parse(ReadLine());
                B.Set(b);
                WriteLine("B -- " + B.DebugString());
                A = plus ? BigInt.SignOp(A, B) : BigInt.DiffSignOp(A, B);
                WriteLine("A -- " + A.DebugString());
            }
            while (b != 0)
            ;
        }

        static void test2()
        {
            Console.WindowWidth = 180;
            BigInt A = new BigInt();
            BigInt B = new BigInt();
            A.Set(uint.MaxValue);
            B.Set(1);
            A = BigInt.SignOp(A, B);

            for (int i = 0; i < 1000000; i++)
            {
                A = BigInt.SignOp(A, A);
                WriteLine(A.DebugString());
                ReadKey();
            }

        }
    }
}
