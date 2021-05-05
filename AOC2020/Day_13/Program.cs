using System;
using System.IO;
using System.Linq;

namespace Day_13
{
    class Program
    {
        public static void Main(string[] args)
        {
            var data = File.ReadAllLines(args[0]);
            var departureTime = int.Parse(data[0]);
            var buses = data[1].Split(',').Select(b => int.TryParse(b, out var bt) ? bt : 0).ToArray();
            Console.WriteLine($"Departure time: {departureTime}");
            Console.WriteLine($"Ferries: {string.Join('|', buses)}");
            var (theBus, timeDifference) = buses
                .Where(f => f != 0)
                .Select(x => (x, SmallestMultipleGreaterThan(x, departureTime)))
                .Select(t => (t.Item1, t.Item2 - departureTime))
                .Aggregate((0, int.MaxValue), (curSmallest, t) => curSmallest.Item2 > t.Item2 ? t : curSmallest);
            Console.WriteLine($"bus({theBus}) * time difference({timeDifference}) = {theBus * timeDifference}");

            var (greatestPeriod, index) = buses
                .Select((b, i) => (b, i))
                .Where(t => t.Item1 != 0)
                .Aggregate((int.MinValue, -1), (curLargest, t) => curLargest.Item1 < t.Item1 ? t : curLargest);
            Console.WriteLine($"The bus with the greatest period is {greatestPeriod} with index {index}");
            long theTimestamp = greatestPeriod;
            while (true)
            {
                int i;
                for (i = 0; i < buses.Length; ++i)
                {
                    if (buses[i] == 0)
                    {
                        continue;
                    }
                    if ((theTimestamp - (index - i)) % buses[i] != 0)
                    {
                        break;
                    }
                }
                if (i == buses.Length)
                {
                    break;
                }
                else
                {
                    theTimestamp += greatestPeriod;
                }
            }
            Console.WriteLine($"The earliest timestamp that meets the contest requirement is {theTimestamp - index}");
        }

        public static int SmallestMultipleGreaterThan(int factor, int target)
        {
            int theMultiple = factor;
            while (theMultiple < target)
            {
                theMultiple += factor;
            }
            return theMultiple;
        }
    }
}
