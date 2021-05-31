using System;
using System.Linq;

namespace Day_15
{
    public class Program
    {
        static void Main(string[] args)
        {
            int lastSaid = int.Parse(args[0]);
            var sequence = new Sequence(args[1..].Select(int.Parse).ToArray());
            Console.WriteLine($"The {lastSaid}th number is {sequence.FindNthNumber(lastSaid)}");
        }
    }
}
