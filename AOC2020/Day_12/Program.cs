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

        public void Steer(string instruction)
        {
            char command = instruction[0];
            int argument = int.Parse(instruction[1..]);

            switch (command)
            {
                case 'N':
                    Y += argument;
                    break;
                case 'S':
                    Y -= argument;
                    break;
                case 'E':
                    X += argument;
                    break;
                case 'W':
                    Y -= argument;
                    break;
                case 'L':
                    //Console.WriteLine($"Currently facing {ferry.Heading}");
                    //Console.WriteLine($"Current state: {ferry.Heading}@({ferry.X},{ferry.Y})");
                    //Console.WriteLine($"Changing heading by {argument} to port");
                    Heading = Heading + argument;
                    //Console.WriteLine($"New state: {ferry.Heading}@({ferry.X},{ferry.Y})");
                    //Console.WriteLine();
                    break;
                case 'R':
                    //Console.WriteLine($"Currently facing {ferry.Heading}");
                    //Console.WriteLine($"Current state: {ferry.Heading}@({ferry.X},{ferry.Y})");
                    //Console.WriteLine($"Changing heading by {argument} to starboard");
                    Heading = Heading - argument;
                    //Console.WriteLine($"New state: {ferry.Heading}@({ferry.X},{ferry.Y})");
                    //Console.WriteLine();
                    break;
                case 'F':
                    if (Heading == 0)
                    {
                        //Console.WriteLine($"Currently facing {ferry.Heading}");
                        //Console.WriteLine($"Current state: {ferry.Heading}@({ferry.X},{ferry.Y})");
                        //Console.WriteLine($"Changing position by {argument}");
                        X += argument;
                        //Console.WriteLine($"New state: {ferry.Heading}@({ferry.X},{ferry.Y})");
                        //Console.WriteLine();
                        break;
                    }
                    else if (Heading == 90)
                    {
                        //Console.WriteLine($"Currently facing {ferry.Heading}");
                        //Console.WriteLine($"Current state: {ferry.Heading}@({ferry.X},{ferry.Y})");
                        //Console.WriteLine($"Changing position by {argument}");
                        Y += argument;
                        //Console.WriteLine($"New state: {ferry.Heading}@({ferry.X},{ferry.Y})");
                        //Console.WriteLine();
                        break;
                    }
                    else if (Heading == 180)
                    {
                        //Console.WriteLine($"Currently facing {ferry.Heading}");
                        //Console.WriteLine($"Current state: {ferry.Heading}@({ferry.X},{ferry.Y})");
                        //Console.WriteLine($"Changing position by {argument}");
                        X -= argument;
                        //Console.WriteLine($"New state: {ferry.Heading}@({ferry.X},{ferry.Y})");
                        //Console.WriteLine();
                        break;
                    }
                    else if (Heading == 270)
                    {
                        //Console.WriteLine($"Currently facing {ferry.Heading}");
                        //Console.WriteLine($"Current state: {ferry.Heading}@({ferry.X},{ferry.Y})");
                        //Console.WriteLine($"Changing position by {argument}");
                        Y -= argument;
                        //Console.WriteLine($"New state: {ferry.Heading}@({ferry.X},{ferry.Y})");
                        //Console.WriteLine();
                        break;
                    }
                    else
                    {
                        throw new Exception($"Unrecognized heading change: {Heading} + {argument}");
                    }
                default:
                    throw new Exception($"Unrecognized instruction: {instruction}");
            }
        }

        public int ManhattanDistance()
        {
            return Math.Abs(X) + Math.Abs(Y);
        }
    }
}
