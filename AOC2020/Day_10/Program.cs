using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_10
{
    class Program
    {
        static void Main(string[] args)
        {
            var adapters = File.ReadLines(args[0])
                .Select(int.Parse)
                .ToArray();
            Array.Sort(adapters);
            var differences = new Dictionary<int, int>();
            int currentValue;
            if (differences.TryGetValue(adapters[0], out currentValue))
            {
                differences[adapters[0]] = currentValue + 1;
            }
            else
            {
                differences.Add(adapters[0], 1);
            }
            for (int i = 0; i < adapters.Length - 1; ++i)
            {
                var difference = adapters[i + 1] - adapters[i];
                if (difference < 1 || difference > 3)
                {
                    throw new Exception($"cannot bridge a difference of {difference} between {adapters[i]} and {adapters[i + 1]}");
                }
                if (differences.TryGetValue(difference, out currentValue))
                {
                    differences[difference] = currentValue + 1;
                }
                else
                {
                    differences.Add(difference, 1);
                }
            }
            differences[3] = differences[3] + 1;
            Console.WriteLine($"{differences[1]} * {differences[3]} = {differences[1] * differences[3]}");
        }
    }
}
