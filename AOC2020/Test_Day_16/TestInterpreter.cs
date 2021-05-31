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

        [Test]
        public void It_creates_correct_scanner()
        {
            var interpreter = new Interpreter("test2.txt");
            var scanner = interpreter.CreateScanner();
            var ticket = new Ticket("11,12,13");
            Assert.AreEqual(12, scanner.Scan(ticket, "class"));
            Assert.AreEqual(11, scanner.Scan(ticket, "row"));
            Assert.AreEqual(13, scanner.Scan(ticket, "seat"));
        }
    }
}
