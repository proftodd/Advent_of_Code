using System;
using System.IO;

namespace Day_12
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var instructions = File.ReadLines(args[0]);

            var ferry = new Ferry();
            //Console.WriteLine(ferry);
            foreach (var i in instructions)
            {
                ferry.Steer(i);
                //Console.WriteLine($"\t{ferry}");
            }
            Console.WriteLine($"Ferry Manhattan distance: {ferry.ManhattanDistance()}");

            var ship = new Ship();
            //Console.WriteLine(ship);
            foreach (var i in instructions)
            {
                ship.Steer(i);
                //Console.WriteLine($"\t{ship}");
            }
            Console.WriteLine($"Ship Manhattan distance: {ship.ManhattanDistance()}");
        }
    }
}
