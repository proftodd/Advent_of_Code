using System;

namespace Day_12
{
    public abstract class SeafaringVessel
    {
        public SeafaringVessel()
        {
            X = 0;
            Y = 0;
        }

        public int X { get; set; }

        public int Y { get; set; }

        public abstract void Steer(string instruction);

        protected static ValueTuple<char, int> ParseInstruction(string instruction)
        {
            return (instruction[0], int.Parse(instruction[1..]));
        }

        protected static int NormalizeHeading(int heading)
        {
            while (heading < 0)
            {
                heading += 360;
            }
            if (heading >= 360)
            {
                return heading % 360;
            }
            else
            {
                return heading;
            }
        }

        public int ManhattanDistance()
        {
            return Math.Abs(X) + Math.Abs(Y);
        }
    }
}
