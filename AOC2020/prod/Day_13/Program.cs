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

            var importantBuses = buses
                .Select((b, i) => (b, i))
                .Where(t => t.Item1 != 0)
                .OrderByDescending(t => t.Item1)
                .ToArray();
            //Console.WriteLine($"Important buses: {string.Join('|', importantBuses)}");
            var (greatestPeriod, index) = importantBuses.First();
            Console.WriteLine($"The bus with the greatest period is {greatestPeriod} with index {index}");
            long theTimestamp = greatestPeriod;
            while (true)
            {
                int i;
                for (i = 1; i < importantBuses.Length; ++i)
                {
                    if ((theTimestamp - (index - importantBuses[i].Item2)) % importantBuses[i].Item1 != 0)
                    {
                        break;
                    }
                }
                if (i == importantBuses.Length)
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
