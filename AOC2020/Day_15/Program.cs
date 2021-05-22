using System;
using System.Linq;

namespace Day_15
{
    public class Program
    {
        static void Main(string[] args)
        {
            var sequence = new Sequence(args.Select(int.Parse).ToArray());
            Console.WriteLine($"The 2020th number is {sequence.FindNthNumber(2020)}");
        }
    }
}
