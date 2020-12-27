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
            HandleJoltageDifference(adapters[0], differences);
            for (int i = 0; i < adapters.Length - 1; ++i)
            {
                var difference = adapters[i + 1] - adapters[i];
                HandleJoltageDifference(difference, differences);
            }
            HandleJoltageDifference(3, differences);
            Console.WriteLine($"{differences[1]} * {differences[3]} = {differences[1] * differences[3]}");
        }

        public static void HandleJoltageDifference(int difference, IDictionary<int, int> countedDifferences)
        {
            if (difference < 1 || difference > 3)
            {
                throw new Exception($"cannot bridge a {difference} Joltage difference");
            }
            if (countedDifferences.TryGetValue(difference, out var currentValue))
            {
                countedDifferences[difference] = currentValue + 1;
            }
            else
            {
                countedDifferences.Add(difference, 1);
            }
        }
    }
}
