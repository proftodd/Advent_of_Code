using System;
using System.IO;

namespace Day_12
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var ferry = new Ferry();
            var instructions = File.ReadLines(args[0]);
            foreach (var i in instructions)
            {
                ferry.Steer(i);
            }
            Console.WriteLine($"Manhattan distance: {ferry.ManhattanDistance()}");
        }
    }
}
