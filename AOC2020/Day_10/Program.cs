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
            Console.WriteLine($"{differences[1]} 1 Jolt gaps * {differences[3]} 3 Jolt gaps = {differences[1] * differences[3]}");
            var configs = GenerateConfigurations(adapters);
            Console.WriteLine($"There are {configs.Values.Select(l => l.Count()).Sum()} ways to arrange the adapters");
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

        public static IDictionary<int, List<int[]>> GenerateConfigurations(int [] adapters)
        {
            var ret = new Dictionary<int, List<int[]>>();
            ret.Add(adapters.Length, new[] { adapters }.ToList());
            for (int i = adapters.Length; i > 0; --i)
            {
                if (ret[i].Count == 0)
                {
                    break;
                }
                var listOfOneShorterLists = new List<int[]>();
                ret.Add(i - 1, listOfOneShorterLists);
                foreach (var thisList in ret[i])
                {
                    // Console.WriteLine($"checking {string.Join(',', thisList.Select(a => a.ToString()))}");
                    for (int j = 0; j < thisList.Length - 1; ++j)
                    {
                        var prev = j == 0 ? 0 : thisList[j - 1];
                        var next = thisList[j + 1];
                        if (next - prev <= 3)
                        {
                            var shortenedList = thisList.Where((k, l) => l != j).ToArray();
                            // Console.WriteLine($"\tRemoving thisList[{j}] = {thisList[j]}, adding {string.Join(',', shortenedList)}");
                            if (!listOfOneShorterLists.Any(a => a.ElementsAreEqual(shortenedList)))
                            {
                                listOfOneShorterLists.Add(shortenedList);
                            }
                        }
                    }
                    // break;
                }
                Console.WriteLine($"There are {ret[i].Count()} combinations of length {i}.");
            }
            return ret;
        }
    }
}
