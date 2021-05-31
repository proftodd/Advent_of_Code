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

            var scanner = interpreter.CreateScanner();
            var departureFields = interpreter.Fields.Where(f => f.Name.StartsWith("departure"));
            long product = departureFields
                .Select(df => scanner.Scan(interpreter.Ticket, df.Name))
                .Aggregate(1L, (agg, value) => agg * (long)value);
            Console.WriteLine($"The product of departure field values is {product}");
        }
    }
}
