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
            var (b1, b2, b3, bonus) = FindBonus(entries);
            Console.WriteLine($"{e1} * {e2} = {product}");
            Console.WriteLine($"{b1} * {b2} * {b3} = {bonus}");
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

        public static ValueTuple<int, int, int, int> FindBonus(int[] entries)
        {
            int i, j, k;
            for (i = 0; i < entries.Length - 2; ++i)
            {
                for (j = i + 1; j < entries.Length - 1; ++j)
                {
                    for (k = j + 1; k < entries.Length; ++k)
                    {
                        if (entries[i] + entries[j] + entries[k] == 2020)
                        {
                            return (entries[i], entries[j], entries[k], entries[i] * entries[j] * entries[k]);
                        }
                    }
                }
            }

            return (0, 0, 0, 0);
        }
    }
}
