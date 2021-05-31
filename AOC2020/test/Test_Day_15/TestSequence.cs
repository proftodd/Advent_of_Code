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
            Sequence sequence;

            sequence = new Sequence(new[] { 0, 3, 6 });
            Assert.AreEqual(6, sequence.FindNthNumber(3));
            Assert.AreEqual(0, sequence.FindNthNumber(10));
            Assert.AreEqual(436, sequence.FindNthNumber(2020));
        }

        [TestCase(new int[] { 0, 3, 6 }, 2020, 436)]
        [TestCase(new int[] { 0, 3, 6 }, 30000000, 175594)]
        [TestCase(new int[] { 1, 3, 2 }, 2020, 1)]
        [TestCase(new int[] { 1, 3, 2 }, 30000000, 2578)]
        [TestCase(new int[] { 2, 1, 3 }, 2020, 10)]
        [TestCase(new int[] { 2, 1, 3 }, 30000000, 3544142)]
        [TestCase(new int[] { 1, 2, 3 }, 2020, 27)]
        [TestCase(new int[] { 1, 2, 3 }, 30000000, 261214)]
        [TestCase(new int[] { 2, 3, 1 }, 2020, 78)]
        [TestCase(new int[] { 2, 3, 1 }, 30000000, 6895259)]
        [TestCase(new int[] { 3, 2, 1 }, 2020, 438)]
        [TestCase(new int[] { 3, 2, 1 }, 30000000, 18)]
        [TestCase(new int[] { 3, 1, 2 }, 2020, 1836)]
        [TestCase(new int[] { 3, 1, 2 }, 30000000, 362)]
        public void It_gives_correct_responses_to_example_cases(int[] seed, int numSaid, int lastSaid)
        {
            var sequence = new Sequence(seed);
            Assert.AreEqual(lastSaid, sequence.FindNthNumber(numSaid));
        }
    }
}
