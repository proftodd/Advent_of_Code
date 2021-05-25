using System;
using System.Linq;
using NUnit.Framework;

namespace Day_16
{
    public class TestInterpreter
    {
        [Test]
        public void It_is_constructed_correctly()
        {
            var interpreter = new Interpreter("test.txt");
            Assert.NotNull(interpreter.Ticket);
            Assert.AreEqual(3, interpreter.Fields.Count);
            Assert.AreEqual(4, interpreter.NearbyTickets.Count);
        }

        [Test]
        public void It_finds_scan_errors_correctly()
        {
            var interpreter = new Interpreter("test.txt");
            var expectedErrors = new[]
            {
                new Tuple<Ticket, int, int>(new Ticket("40,4,50"), 1, 4),
                new Tuple<Ticket, int, int>(new Ticket("55,2,20"), 0, 55),
                new Tuple<Ticket, int, int>(new Ticket("38,6,12"), 2, 12)
            };
            Assert.IsTrue(interpreter.ScanErrors().Zip(expectedErrors).All(t => t.Item1.Item2 == t.Item2.Item2 && t.Item1.Item3 == t.Item2.Item3));
        }
    }
}
