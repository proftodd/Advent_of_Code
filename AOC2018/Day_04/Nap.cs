using System;

namespace Day_04
{
    public class Nap
    {
        public DateTimeOffset ClosedStart { get; set; } = DateTimeOffset.MinValue;

        public DateTimeOffset OpenEnd { get; set; } = DateTimeOffset.MaxValue;
    }
}