using System;
using System.Collections;
using System.IO;
using System.Linq;

namespace Day_03
{
    class Program
    {
        static void Main(string[] args)
        {
            var map = File.ReadAllLines(args[0]);
            long product = Enumerable.Range(1, args.Length - 1)
                .Select(i => args[i])
                .Select(s => {
                    var subArray = s.Split(",");
                    return (int.Parse(subArray[0]), int.Parse(subArray[1]));
                })
                .Select(t => CountTrees(map, t.Item1, t.Item2))
                .Aggregate((acc, val) => acc * val);
            Console.WriteLine($"The product of the routes is {product}");
        }

        public static long CountTrees(string[] map, int stepX, int stepY)
        {
            var height = map.Length;
            var width = map[0].Length;
            var x = 0;
            var y = 0;
            var treeCount = 0L;
            while (y < height - 1)
            {
                x = (x + stepX) % width;
                y = y + stepY;
                if (map[y][x] == '#')
                {
                    ++treeCount;
                }
            }
            return treeCount;
        }
    }
}
