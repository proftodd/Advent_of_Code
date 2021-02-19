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

        public static long CountConfigurations(int [] adapters)
        {
            List<int[]> currentConfigurationList = new List<int[]>(new[] { adapters });
            List<int[]> listOfOneShorterConfigurations;
            long configurationCount = 1;
            while (currentConfigurationList.LongCount() > 0L)
            {
                listOfOneShorterConfigurations = new List<int[]>();
                foreach (var thisList in currentConfigurationList)
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
                            if (!listOfOneShorterConfigurations.Any(a => a.ElementsAreEqual(shortenedList)))
                            {
                                listOfOneShorterConfigurations.Add(shortenedList);
                            }
                        }
                    }
                    // break;
                }
                if (listOfOneShorterConfigurations.LongCount() > 0L)
                {
                    Console.WriteLine($"There are {listOfOneShorterConfigurations.LongCount()} combinations of length {listOfOneShorterConfigurations[0].Length}.");
                }
                configurationCount += listOfOneShorterConfigurations.LongCount();
                currentConfigurationList = listOfOneShorterConfigurations;
            }
            return configurationCount;
        }
    }
}
