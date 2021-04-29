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
                UpdateFerry(ferry, i);
            }
            Console.WriteLine($"Manhattan distance: {ferry.ManhattanDistance()}");
        }

        public static void UpdateFerry(Ferry ferry, string instruction)
        {
            char command = instruction[0];
            int argument = int.Parse(instruction[1..]);

            switch (command)
            {
                case 'N':
                    ferry.Y += argument;
                    break;
                case 'S':
                    ferry.Y -= argument;
                    break;
                case 'E':
                    ferry.X += argument;
                    break;
                case 'W':
                    ferry.Y -= argument;
                    break;
                case 'L':
                    //Console.WriteLine($"Currently facing {ferry.Heading}");
                    //Console.WriteLine($"Current state: {ferry.Heading}@({ferry.X},{ferry.Y})");
                    //Console.WriteLine($"Changing heading by {argument} to port");
                    ferry.Heading = ferry.Heading + argument;
                    //Console.WriteLine($"New state: {ferry.Heading}@({ferry.X},{ferry.Y})");
                    //Console.WriteLine();
                    break;
                case 'R':
                    //Console.WriteLine($"Currently facing {ferry.Heading}");
                    //Console.WriteLine($"Current state: {ferry.Heading}@({ferry.X},{ferry.Y})");
                    //Console.WriteLine($"Changing heading by {argument} to starboard");
                    ferry.Heading = ferry.Heading - argument;
                    //Console.WriteLine($"New state: {ferry.Heading}@({ferry.X},{ferry.Y})");
                    //Console.WriteLine();
                    break;
                case 'F':
                    if (ferry.Heading == 0)
                    {
                        //Console.WriteLine($"Currently facing {ferry.Heading}");
                        //Console.WriteLine($"Current state: {ferry.Heading}@({ferry.X},{ferry.Y})");
                        //Console.WriteLine($"Changing position by {argument}");
                        ferry.X += argument;
                        //Console.WriteLine($"New state: {ferry.Heading}@({ferry.X},{ferry.Y})");
                        //Console.WriteLine();
                        break;
                    }
                    else if (ferry.Heading == 90)
                    {
                        //Console.WriteLine($"Currently facing {ferry.Heading}");
                        //Console.WriteLine($"Current state: {ferry.Heading}@({ferry.X},{ferry.Y})");
                        //Console.WriteLine($"Changing position by {argument}");
                        ferry.Y += argument;
                        //Console.WriteLine($"New state: {ferry.Heading}@({ferry.X},{ferry.Y})");
                        //Console.WriteLine();
                        break;
                    }
                    else if (ferry.Heading == 180)
                    {
                        //Console.WriteLine($"Currently facing {ferry.Heading}");
                        //Console.WriteLine($"Current state: {ferry.Heading}@({ferry.X},{ferry.Y})");
                        //Console.WriteLine($"Changing position by {argument}");
                        ferry.X -= argument;
                        //Console.WriteLine($"New state: {ferry.Heading}@({ferry.X},{ferry.Y})");
                        //Console.WriteLine();
                        break;
                    }
                    else if (ferry.Heading == 270)
                    {
                        //Console.WriteLine($"Currently facing {ferry.Heading}");
                        //Console.WriteLine($"Current state: {ferry.Heading}@({ferry.X},{ferry.Y})");
                        //Console.WriteLine($"Changing position by {argument}");
                        ferry.Y -= argument;
                        //Console.WriteLine($"New state: {ferry.Heading}@({ferry.X},{ferry.Y})");
                        //Console.WriteLine();
                        break;
                    }
                    else
                    {
                        throw new Exception($"Unrecognized heading change: {ferry.Heading} + {argument}");
                    }
                default:
                    throw new Exception($"Unrecognized instruction: {instruction}");
            }
        }
    }

    public class Ferry
    {
        public Ferry()
        {
            X = 0;
            Y = 0;
            Heading = 0;
        }

        private int heading;

        public int X { get; set; }

        public int Y { get; set; }

        public int Heading
        {
            get { return heading; }
            set
            {
                if (value < 0)
                {
                    heading = value + 360;
                }
                else if (value >= 360)
                {
                    heading = value % 360;
                }
                else
                {
                    heading = value;
                }
            }
        }

        public int ManhattanDistance()
        {
            return Math.Abs(X) + Math.Abs(Y);
        }
    }
}
