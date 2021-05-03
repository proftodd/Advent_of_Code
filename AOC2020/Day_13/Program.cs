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
            var buses = data[1].Split(',');
            Console.WriteLine($"Departure time: {departureTime}");
            Console.WriteLine($"Ferries: {string.Join('|', buses)}");
            var (theBus, timeDifference) = buses
                .Where(f => f != "x")
                .Select(int.Parse)
                .Select(x => (x, SmallestMultipleGreaterThan(x, departureTime)))
                .Select(t => (t.Item1, t.Item2 - departureTime))
                .Aggregate((0, int.MaxValue), (curSmallest, t) => curSmallest.Item2 > t.Item2 ? t : curSmallest);
            Console.WriteLine($"bus({theBus}) * time difference({timeDifference}) = {theBus * timeDifference}");
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
