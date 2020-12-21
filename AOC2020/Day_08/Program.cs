using System;
using System.Collections.Generic;
using System.IO;

namespace Day_08
{
    class Program
    {
        static void Main(string[] args)
        {
            var program = File.ReadAllLines(args[0]);
            var console = new Console(program);
            var addresses = new Dictionary<int, int>();
            while (!addresses.ContainsKey(console.Address))
            {
                addresses.Add(console.Address, console.Accumulator);
                console.Step();
            }
            System.Console.WriteLine($"console repeats address {console.Address} with accumulator {console.Accumulator}");
        }
    }
}
