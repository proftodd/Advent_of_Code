using System;
using System.Text.RegularExpressions;

namespace Day_04
{
    public abstract class IMessage
    {
        protected IMessage(DateTimeOffset time, string message)
        {
            Time = time;
            Message = message;
        }

        public string Message { get; }

        public DateTimeOffset Time { get; }
    }

    public class NewGuard : IMessage
    {
        private static Regex rx = new Regex(@"^Guard #(\d+) begins shift$");

        public NewGuard(DateTimeOffset time, string message) : base(time, message)
        {
            MatchCollection matches = rx.Matches(message);
            Match match = matches[0];
            GuardId = Int32.Parse(match.Groups[1].Value);
        }

        public int GuardId { get; }
    }

    public class FallsAsleep : IMessage
    {
        public FallsAsleep(DateTimeOffset time, string message) : base(time, message)
        {
        }
    }

    public class WakesUp : IMessage
    {
        public WakesUp(DateTimeOffset time, string message) : base(time, message)
        {
        }
    }
}