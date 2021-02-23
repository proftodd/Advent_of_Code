using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Shared;

namespace Day_10
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var adapterList = File.ReadLines(args[0])
                .Select(int.Parse)
                .OrderBy(a => a)
                .ToList();
            adapterList.Add(adapterList.Last() + 3);
            adapterList.Insert(0, 0);
            var adapters = adapterList.ToArray();
            var differences = new Dictionary<int, int>();
            HandleJoltageDifference(adapters[1], differences);
            for (int i = 1; i < adapters.Length - 2; ++i)
            {
                var difference = adapters[i + 1] - adapters[i];
                HandleJoltageDifference(difference, differences);
            }
            HandleJoltageDifference(3, differences);
            Console.WriteLine($"{differences[1]} 1 Jolt gaps * {differences[3]} 3 Jolt gaps = {differences[1] * differences[3]}");
            Console.WriteLine($"There are {CountConfigurations(adapters)} ways to arrange the adapters");
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

        public static Dictionary<long, long> resultSet = new Dictionary<long, long>();

        // Hat tip to Djolence Tipic for the algorithm to use
        // https://github.com/DjolenceTipic/Advent-of-Code/blob/main/Advent-of-Code-2020/day-10/Program.cs
        public static long CountConfigurations(int[] adapters)
        {
            if (resultSet.ContainsKey(adapters.Length))
            {
                return resultSet[adapters.Length];
            }

            if (adapters.Length == 1)
            {
                return 1;
            }

            long total = 0;
            for (int i = 1; i < adapters.Length; ++i)
            {
                if (adapters[i] - adapters[0] <= 3)
                {
                    total += CountConfigurations(adapters[i..]);
                }
                else
                {
                    break;
                }
            }

            resultSet.Add(adapters.Length, total);

            return total;
        }
    }
}
