using System;
using System.IO;
using System.Linq;

namespace Day_01
{
    public class Program
    {
        static void Main(string[] args)
        {
            int[] entries = File.ReadAllLines(args[0])
                .Select(s => Int32.Parse(s))
                .ToArray();
            var (e1, e2, product) = CorrectExpenseReport(entries);
            Console.WriteLine($"{e1} * {e2} = {product}");
        }

        public static ValueTuple<int, int, int> CorrectExpenseReport(int[] entries)
        {
            int i, j;
            for (i = 0; i < entries.Length - 1; ++i)
            {
                for (j = i + 1; j < entries.Length; ++j)
                {
                    if (entries[i] + entries[j] == 2020)
                    {
                        return (entries[i], entries[j], entries[i] * entries[j]);
                    }
                }
            }

            return (0, 0, 0);
        }
    }
}
