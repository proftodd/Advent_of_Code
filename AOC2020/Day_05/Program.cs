using System;
using System.IO;
using System.Linq;

namespace Day_05
{
    public class Program
    {
        static void Main(string[] args)
        {
            var data = File.ReadAllLines(args[0]);
            var seatIDs = data
                .Select(s => GetSeatID(s))
                .ToArray();
            var maxSeatID = seatIDs.Max();

            Console.WriteLine($"Max Seat ID: {maxSeatID}");

            int mySeatID = -1;
            for (int i = 1; i < maxSeatID; ++i)
            {
                if (seatIDs.Contains(i - 1)
                && !seatIDs.Contains(i)
                &&  seatIDs.Contains(i + 1))
                {
                    mySeatID = i;
                    break;
                }
            }
            
            Console.WriteLine($"My seat ID is {mySeatID}");
        }

        public static int GetSeatID(string boardingPass)
        {
            var (row, seat) = GetSeatAssignment(boardingPass);
            return 8 * row + seat;
        }

        public static ValueTuple<int, int> GetSeatAssignment(string boardingPass)
        {
            var rowRange = (0, 127);
            var seatRange = (0, 7);
            for (int i = 0; i < 7; ++i)
            {
                rowRange = ReduceRange(rowRange, boardingPass[i]);
            }
            for (int i = 7; i < 10; ++i)
            {
                seatRange = ReduceRange(seatRange, boardingPass[i]);
            }
            return (rowRange.Item1, seatRange.Item1);
        }

        public static ValueTuple<int, int> ReduceRange(ValueTuple<int, int> startingRange, char direction)
        {
            var (min, max) = startingRange;
            var mid = (min + max) / 2;
            if (direction == 'F' || direction == 'L')
            {
                return (min, mid);
            }
            else
            {
                return (mid + 1, max);
            }
        }
    }
}
