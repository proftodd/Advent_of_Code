using NUnit.Framework;

namespace Day_08
{
    public class TestAccumulate
    {
        [Test]
        public void It_increases_accumulator()
        {
            Instruction acc = new Accumulate();
            var accumulator = 0;
            var nextAddress = 1;
            var arg = 5;
            var result = acc.Execute(ref accumulator, ref nextAddress, arg);
            Assert.AreEqual(0, result);
            Assert.AreEqual(arg, accumulator);
            Assert.AreEqual(2, nextAddress);
        }
    }
}