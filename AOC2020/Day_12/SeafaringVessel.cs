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

        protected ValueTuple<char, int> ParseInstruction(string instruction)
        {
            return (instruction[0], int.Parse(instruction[1..]));
        }

        public int ManhattanDistance()
        {
            return Math.Abs(X) + Math.Abs(Y);
        }
    }
}
