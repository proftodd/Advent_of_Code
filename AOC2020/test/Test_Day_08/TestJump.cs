using NUnit.Framework;

namespace Day_08
{
    public class TestJump
    {
        [Test]
        public void It_jumps_to_correct_address()
        {
            Instruction jump = new Jump();
            var accumulator = 0;
            var nextAddress = 1;
            var arg = 5;
            var result = jump.Execute(ref accumulator, ref nextAddress, arg);
            Assert.AreEqual(0, result);
            Assert.AreEqual(0, accumulator);
            Assert.AreEqual(6, nextAddress);
        }
    }
}