using NUnit.Framework;

namespace Day_15
{
    public class TestSequence
    {
        [Test]
        public void It_correctly_initializes_sequence()
        {
            var sequence = new Sequence(new[] { 0, 3, 6 });
            Assert.AreEqual(3, sequence.NumberSaid);
            Assert.AreEqual(6, sequence.LastSaid);
        }

        [Test]
        public void It_generates_next_value()
        {
            var sequence = new Sequence(new[] { 0, 3, 6 });

            Assert.AreEqual(0, sequence.NextValue());
            Assert.AreEqual(4, sequence.NumberSaid);
            Assert.AreEqual(0, sequence.LastSaid);

            Assert.AreEqual(3, sequence.NextValue());
            Assert.AreEqual(5, sequence.NumberSaid);
            Assert.AreEqual(3, sequence.LastSaid);

            Assert.AreEqual(3, sequence.NextValue());
            Assert.AreEqual(6, sequence.NumberSaid);
            Assert.AreEqual(3, sequence.LastSaid);

            Assert.AreEqual(1, sequence.NextValue());
            Assert.AreEqual(7, sequence.NumberSaid);
            Assert.AreEqual(1, sequence.LastSaid);

            Assert.AreEqual(0, sequence.NextValue());
            Assert.AreEqual(8, sequence.NumberSaid);
            Assert.AreEqual(0, sequence.LastSaid);

            Assert.AreEqual(4, sequence.NextValue());
            Assert.AreEqual(9, sequence.NumberSaid);
            Assert.AreEqual(4, sequence.LastSaid);

            Assert.AreEqual(0, sequence.NextValue());
            Assert.AreEqual(10, sequence.NumberSaid);
            Assert.AreEqual(0, sequence.LastSaid);
        }

        [Test]
        public void It_runs_correctly()
        {
            int[] seed;

            seed = new[] { 0, 3, 6 };
            Assert.AreEqual(6, new Sequence(seed).FindNthNumber(3));
            Assert.AreEqual(0, new Sequence(seed).FindNthNumber(10));
            Assert.AreEqual(436, new Sequence(seed).FindNthNumber(2020));

            Assert.AreEqual(1, new Sequence(new[] { 1, 3, 2 }).FindNthNumber(2020));
            Assert.AreEqual(10, new Sequence(new[] { 2, 1, 3 }).FindNthNumber(2020));
            Assert.AreEqual(27, new Sequence(new[] { 1, 2, 3 }).FindNthNumber(2020));
            Assert.AreEqual(78, new Sequence(new[] { 2, 3, 1 }).FindNthNumber(2020));
            Assert.AreEqual(438, new Sequence(new[] { 3, 2, 1 }).FindNthNumber(2020));
            Assert.AreEqual(1836, new Sequence(new[] { 3, 1, 2 }).FindNthNumber(2020));
        }
    }
}
