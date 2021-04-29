using System;

namespace Day_12
{
    public class Ferry : SeafaringVessel
    {
        public Ferry() : base()
        {
            Heading = 0;
        }

        private int heading;

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

        public override void Steer(string instruction)
        {
            var (command, argument) = ParseInstruction(instruction);

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
    }
}
