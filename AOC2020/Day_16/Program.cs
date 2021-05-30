using System;
using System.Linq;

namespace Day_16
{
    class Program
    {
        static void Main(string[] args)
        {
            var interpreter = new Interpreter(args[0]);
            var badTickets = interpreter.ScanErrors();
            var scanningErrorRate = badTickets
                .Select(t => t.Item3)
                .Sum();
            Console.WriteLine($"Observed scanningErrorRate = {scanningErrorRate}");
        }
    }
}
