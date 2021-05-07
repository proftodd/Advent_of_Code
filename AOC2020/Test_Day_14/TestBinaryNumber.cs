using NUnit.Framework;

namespace Day_14
{
    public class TestBinaryNumber
    {
        [Test]
        public void It_roundtrips_correctly()
        {
            BinaryNumber bn;

            bn = new BinaryNumber(11L);
            Assert.AreEqual(11L, bn.ToLong());

            bn = new BinaryNumber(73L);
            Assert.AreEqual(73L, bn.ToLong());

            bn = new BinaryNumber(101L);
            Assert.AreEqual(101L, bn.ToLong());

            bn = new BinaryNumber(64L);
            Assert.AreEqual(64L, bn.ToLong());
        }
    }
}
