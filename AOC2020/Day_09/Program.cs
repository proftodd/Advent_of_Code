using System;
using System.IO;
using System.Linq;

namespace Day_09
{
    class Program
    {
        static void Main(string[] args)
        {
            var preambleLength = int.Parse(args[0]);
            var data = File.ReadLines(args[1])
                .Select(Int64.Parse)
                .ToArray();
            var preamble = new long[preambleLength];
            Array.Copy(data, 0, preamble, 0, preambleLength);

            Console.WriteLine($"The preamble has length {preambleLength}");
            Console.WriteLine($"the first preamble is {string.Join(',', preamble)}");

            for (int i = preambleLength; i < data.Length;)
            {
                var cp =
                    from p1 in preamble
                    from p2 in preamble
                    select (p1, p2);
                if (cp.Any(t => t.Item1 + t.Item2 == data[i]))
                {
                    // Console.WriteLine($"{data[i]} = {t.Item1} + {t.Item2}");
                    ++i;
                    Array.Copy(data, i - preambleLength, preamble, 0, preambleLength);
                    continue;
                }
                else
                {
                    Console.WriteLine($"{data[i]} cannot be formed as the sum of any two elements of {string.Join(',', preamble)}");
                    break;
                }
            }
        }
    }
}
