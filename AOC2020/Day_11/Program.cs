using System;
using System.IO;
using System.Linq;

namespace Day_11
{
    public class Program
    {
        static void Main(string[] args)
        {
            var initialFloorPlan = ReadFloorPlan(args[0]);
            char[][] floorPlan, nextFloorPlan;

            nextFloorPlan = initialFloorPlan;
            do
            {
                floorPlan = nextFloorPlan;
                nextFloorPlan = Iterate(floorPlan, AdjacentSeatCriterion);
            }
            while (!AreSame(floorPlan, nextFloorPlan));
            Console.WriteLine($"Adjacent seats: When stable, there are {CountOccupiedChairs(nextFloorPlan)} filled seats");

            nextFloorPlan = initialFloorPlan;
            do
            {
                floorPlan = nextFloorPlan;
                nextFloorPlan = Iterate(floorPlan, VisibleSeatCriterion);
            }
            while (!AreSame(floorPlan, nextFloorPlan));
            Console.WriteLine($"Visible seats: When stable, there are {CountOccupiedChairs(nextFloorPlan)} filled seats");
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

        public static char[][] Iterate(char[][] floorPlan, Func<char[][], int, int, char> seatCriterion)
        {
            var ret = new char[floorPlan.Length][];
            for (int i = 0; i < floorPlan.Length; ++i)
            {
                ret[i] = new char[floorPlan[i].Length];
                for (int j = 0; j < floorPlan[i].Length; ++j)
                {
                    ret[i][j] = seatCriterion(floorPlan, i, j);
                }
            }
            return ret;
        }

        public static char AdjacentSeatCriterion(char[][] floorPlan, int row, int col)
        {
            if (floorPlan[row][col] == '.')
            {
                return '.';
            }
            int filledSeatCount = CountAdjacentFilledSeats(floorPlan, row, col);
            if (floorPlan[row][col] == 'L' && filledSeatCount == 0)
            {
                return '#';
            }
            else if (floorPlan[row][col] == '#' && filledSeatCount >= 4)
            {
                return 'L';
            }
            else
            {
                return floorPlan[row][col];
            }
        }

        public static char VisibleSeatCriterion(char[][] floorPlan, int row, int col)
        {
            if (floorPlan[row][col] == '.')
            {
                return '.';
            }
            int filledSeatCount = CountVisibleFilledSeats(floorPlan, row, col);
            if (floorPlan[row][col] == 'L' && filledSeatCount == 0)
            {
                return '#';
            }
            else if (floorPlan[row][col] == '#' && filledSeatCount >= 5)
            {
                return 'L';
            }
            else
            {
                return floorPlan[row][col];
            }
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

        public static int CountVisibleFilledSeats(char[][] floorPlan, int row, int col)
        {
            var directions = new ValueTuple<int, int>[] {
                (0, 1),  (1, 1),   (1, 0),  (1, -1),
                (0, -1), (-1, -1), (-1, 0), (-1, 1)
            };
            return directions.Select(d => CountVisibleSeatsInDirection(floorPlan, row + d.Item1, col + d.Item2, d)).Sum();
        }

        private static int CountVisibleSeatsInDirection(char[][] floorPlan, int row, int col, ValueTuple<int, int> direction)
        {
            if (row < 0 || col < 0 || row >= floorPlan.Length || col >= floorPlan[row].Length)
            {
                return 0;
            }
            else if (floorPlan[row][col] == '#')
            {
                return 1;
            }
            else if (floorPlan[row][col] == 'L') {
                return 0;
            }
            else
            {
                return CountVisibleSeatsInDirection(floorPlan, row + direction.Item1, col + direction.Item2, direction);
            }
        }
    }
}
