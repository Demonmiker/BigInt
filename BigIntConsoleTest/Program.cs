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
            Console.WindowWidth = 180;
            BigInt A = new BigInt();
            BigInt B = new BigInt();
            A.Set(uint.MaxValue);
            B.Set(1);
            A = BigInt.SignOp(A,B);
            
            for (int i=0;i<1000000;i++)
            {
                A = BigInt.SignOp(A, A);
                WriteLine(A.DebugString());
                ReadKey();
            }
            
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
    }
}
