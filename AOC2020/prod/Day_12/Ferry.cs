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
                heading = NormalizeHeading(value);
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
                    Heading += argument;
                    break;
                case 'R':
                    Heading -= argument;
                    break;
                case 'F':
                    if (Heading == 0)
                    {
                        X += argument;
                        break;
                    }
                    else if (Heading == 90)
                    {
                        Y += argument;
                        break;
                    }
                    else if (Heading == 180)
                    {
                        X -= argument;
                        break;
                    }
                    else if (Heading == 270)
                    {
                        Y -= argument;
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

        public override string ToString()
        {
            return $"Ferry Bearing {Heading}@({X},{Y})";
        }
    }
}
