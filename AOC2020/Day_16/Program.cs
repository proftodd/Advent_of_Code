using System;
using System.Linq;

namespace Day_16
{
    class Program
    {
        static void Main(string[] args)
        {
            var interpreter = new Interpreter(args[0]);
            var scanningErrorRate = interpreter.ScanErrors()
                .Select(t => t.Item3)
                .Sum();
            Console.WriteLine($"Observed scanningErrorRate = {scanningErrorRate}");
        }
    }
}
