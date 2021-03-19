using System;
using System.IO;
using System.Linq;

namespace Day_11
{
    public class Program
    {
        static void Main(string[] args)
        {
            var floorPlan = ReadFloorPlan(args[0]);
            var nextFloorPlan = floorPlan;
            do
            {
                floorPlan = nextFloorPlan;
                nextFloorPlan = Iterate(floorPlan);
            }
            while (!AreSame(floorPlan, nextFloorPlan));

            Console.WriteLine($"When stable, there are {CountOccupiedChairs(nextFloorPlan)} filled seats");
        }

        public static bool AreSame(char[][] fp1, char[][] fp2)
        {
            for (int i = 0; i < fp1.Length; ++i)
            {
                for (int j = 0; j < fp1[i].Length; ++j)
                {
                    if (fp1[i][j] != fp2[i][j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static char[][] ReadFloorPlan(string fileName)
        {
            return File.ReadLines(fileName)
                .Select(l => l.ToArray<char>())
                .ToArray<char[]>();
        }

        public static int CountEmptyChairs(char[][] floorPlan)
        {
            return floorPlan.Select(r => r.Where(p => p == 'L').Count()).Sum();
        }

        public static int CountOccupiedChairs(char[][] floorPlan)
        {
            return floorPlan.Select(r => r.Where(p => p == '#').Count()).Sum();
        }

        public static int CountAdjacentFilledSeats(char[][] floorPlan, int row, int col)
        {
            var chairCount = 0;
            for (int i = row - 1; i <= row + 1; ++i)
            {
                if (i < 0 || i >= floorPlan.Length)
                {
                    continue;
                }
                for (int j = col - 1; j <= col + 1; ++j)
                {
                    if (j < 0 || j >= floorPlan[i].Length)
                    {
                        continue;
                    }
                    if (i == row && j == col)
                    {
                        continue;
                    }
                    if (floorPlan[i][j] == '#')
                    {
                        // Console.WriteLine($"Found a chair at ({i},{j})");
                        ++chairCount;
                    }
                    // else
                    // {
                    //     Console.WriteLine($"No chair at ({i},{j})");
                    // }
                }
            }
            return chairCount;
        }

        public static char[][] Iterate(char[][] floorPlan)
        {
            var ret = new char[floorPlan.Length][];
            int adjacentFilledSeatCount;
            for (int i = 0; i < floorPlan.Length; ++i)
            {
                ret[i] = new char[floorPlan[i].Length];
                for (int j = 0; j < floorPlan[i].Length; ++j)
                {
                    if (floorPlan[i][j] == '.')
                    {
                        ret[i][j] = '.';
                    } 
                    adjacentFilledSeatCount = CountAdjacentFilledSeats(floorPlan, i, j);
                    if (floorPlan[i][j] == 'L' && adjacentFilledSeatCount == 0)
                    {
                        ret[i][j] = '#';
                    }
                    else if (floorPlan[i][j] == '#' && adjacentFilledSeatCount >= 4)
                    {
                        ret[i][j] = 'L';
                    }
                    else
                    {
                        ret[i][j] = floorPlan[i][j];
                    }

                }
            }
            return ret;
        }
    }
}
