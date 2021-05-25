using System.Linq;
using NUnit.Framework;

namespace Day_16
{
    public class TestTicket
    {
        [Test]
        public void It_is_constructed_correctly()
        {
            var ticket = new Ticket("7,1,14");
            Assert.IsTrue(ticket.Fields.SequenceEqual(new[] { 7, 1, 14 }));
        }
    }
}
