using System;
using System.Linq;
using NUnit.Framework;

namespace Day_16
{
    public class TestField
    {
        [Test]
        public void It_is_constructed_properly()
        {
            var field = new Field("class: 1-3 or 5-7");
            Assert.AreEqual("class", field.Name);
            Assert.IsTrue(field.Validator.SequenceEqual(new[] {
                new Range(1, 3),
                new Range(5, 7)
            }));
        }

        [Test]
        public void It_validates_tickets_correctly()
        {
            var field = new Field("class: 1-3 or 5-7");
            Ticket t;

            t = new Ticket("7,3,5");
            Assert.IsTrue(new Tuple<Ticket, int, int>[] { (Tuple<Ticket, int, int>)null, (Tuple<Ticket, int, int>)null, (Tuple<Ticket, int, int>)null }.SequenceEqual(field.Validate(t)));

            t = new Ticket("7,3,47");
            Assert.IsTrue(new Tuple<Ticket, int, int>[] { (Tuple<Ticket, int, int>)null, (Tuple<Ticket, int, int>)null, new Tuple<Ticket, int, int>(t, 2, 47) }.SequenceEqual(field.Validate(t)));
        }
    }
}
