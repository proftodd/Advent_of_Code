using System;
using System.IO;

namespace Day_14
{
    class Program
    {
        static void Main(string[] args)
        {
            DockComputer computer;
            var program = File.ReadLines(args[0]);

            computer = new DockComputer();
            computer.Run(program);
            Console.WriteLine($"Sum of values in memory: [{computer.SumMemory()}]");

            computer = new DockComputer();
            computer.RunV2(program);
            Console.WriteLine($"Sum of values in memory: [{computer.SumMemory()}]");
        }
    }
}
