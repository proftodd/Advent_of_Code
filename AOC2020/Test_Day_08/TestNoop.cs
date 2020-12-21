using NUnit.Framework;

namespace Day_08
{
    public class TestNoop
    {
        [Test]
        public void It_returns_accumulator_unchanged()
        {
            Instruction noop = new Noop();
            var accumulator = 0;
            var nextAddress = 1;
            var arg = 5;
            var result = noop.Execute(ref accumulator, ref nextAddress, arg);
            Assert.AreEqual(0, result);
            Assert.AreEqual(0, accumulator);
            Assert.AreEqual(2, nextAddress);
        }
    }
}