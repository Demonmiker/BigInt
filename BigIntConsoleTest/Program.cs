using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigIntLibrary;
using static System.Console;

namespace BigIntConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Write("num:");
            BigInt A = new BigInt(), B = new BigInt();
            A.Set(uint.Parse(ReadLine()));
            WriteLine("A -- " + A.DebugString());
            uint b;
            do
            {
                Write("num2:");
                b = uint.Parse(ReadLine());
                B.Set(b);
                WriteLine("B -- " + B.DebugString());
                A = BigInt.SignOp(A, B, true);
                WriteLine("A -- " + A.DebugString());
            }
            while (b != 0)
            ;
        }
    }
}
