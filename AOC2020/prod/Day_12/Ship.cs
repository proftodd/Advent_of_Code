using System;

namespace Day_12
{
    public class Waypoint
    {
        public int RelX { get; set; }

        public int RelY { get; set; }
    }

    public class Ship : SeafaringVessel
    {
        private Waypoint waypoint;

        public Ship() : base()
        {
            waypoint = new Waypoint { RelX = 10, RelY = 1 };
        }

        public ValueTuple<int, int> Waypoint
        {
            get => (waypoint.RelX, waypoint.RelY);
        }

        public override void Steer(string instruction)
        {
            var (command, argument) = ParseInstruction(instruction);

            int tmp;
            switch (command)
            {
                case 'N':
                    waypoint.RelY += argument;
                    break;
                case 'S':
                    waypoint.RelY -= argument;
                    break;
                case 'E':
                    waypoint.RelX += argument;
                    break;
                case 'W':
                    waypoint.RelX -= argument;
                    break;
                case 'L':
                    argument = NormalizeHeading(argument);
                    switch (argument)
                    {
                        case 0:
                            break;
                        case 90:
                            tmp = waypoint.RelX;
                            waypoint.RelX = -waypoint.RelY;
                            waypoint.RelY = tmp;
                            break;
                        case 180:
                            waypoint.RelX = -waypoint.RelX;
                            waypoint.RelY = -waypoint.RelY;
                            break;
                        case 270:
                            this.Steer("R90");
                            break;
                        default:
                            throw new Exception($"Unrecognized rotation argument: {argument}");
                    }

                    break;
                case 'R':
                    argument = NormalizeHeading(argument);
                    switch (argument)
                    {
                        case 0:
                            break;
                        case 90:
                            tmp = waypoint.RelY;
                            waypoint.RelY = -waypoint.RelX;
                            waypoint.RelX = tmp;
                            break;
                        case 180:
                            waypoint.RelX = -waypoint.RelX;
                            waypoint.RelY = -waypoint.RelY;
                            break;
                        case 270:
                            this.Steer("L90");
                            break;
                        default:
                            throw new Exception($"Unrecognized rotation argument: {argument}");
                    }
                    break;
                case 'F':
                    X += argument * waypoint.RelX;
                    Y += argument * waypoint.RelY;
                    break;
                default:
                    throw new Exception($"Unrecognized instruction: {instruction}");
            }
        }

        public override string ToString()
        {
            return $"Ship@({X},{Y} with WP({Waypoint.Item1},{Waypoint.Item2})";
        }
    }
}
