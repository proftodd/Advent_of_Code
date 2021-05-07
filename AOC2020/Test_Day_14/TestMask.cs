using NUnit.Framework;

namespace Day_14
{
    public class TestMask
    {
        [Test]
        public void It_masks_binary_numbers_correctly()
        {
            var mask = new Mask("XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X");

            Assert.AreEqual(73, mask.MaskNumber(11).ToLong());
            Assert.AreEqual(101, mask.MaskNumber(101).ToLong());
            Assert.AreEqual(64, mask.MaskNumber(0).ToLong());
        }
    }
}