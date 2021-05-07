using System;
using System.IO;

namespace Day_14
{
    class Program
    {
        static void Main(string[] args)
        {
            var computer = new DockComputer();
            var program = File.ReadLines(args[0]);
            computer.Run(program);
            Console.WriteLine($"Sum of values in memory: [{computer.SumMemory()}]");
        }
    }
}
