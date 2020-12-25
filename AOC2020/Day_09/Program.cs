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
                .Select(long.Parse)
                .ToArray();
            var preamble = new long[preambleLength];
            Array.Copy(data, 0, preamble, 0, preambleLength);

            Console.WriteLine($"The preamble has length {preambleLength}");
            // Console.WriteLine($"the first preamble is {string.Join(',', preamble)}");

            long key = 0L;
            long weakness = 0L;
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
                    key = data[i];
                    break;
                }
            }
            Console.WriteLine($"{key} cannot be formed as the sum of any two elements of {string.Join(',', preamble)}");

            for (int i = 2; i <= data.Length; ++i)
            {
                for (int j = 0; j <= data.Length - i; ++j)
                {
                    var thisRange = data[j .. (i + j)];
                    // Console.WriteLine(string.Join(',', thisRange));
                    if (thisRange.Sum() == key)
                    {
                        // Console.WriteLine($"{key} = {string.Join('+', thisRange)}");
                        Array.Sort(thisRange);
                        weakness = thisRange[0] + thisRange[^1];
                        goto FOUND;
                    }
                }
            }
            FOUND:
            Console.WriteLine($"The encryption weakness is {weakness}");

        }
    }
}
