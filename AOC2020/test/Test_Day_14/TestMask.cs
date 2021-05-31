using System.Collections.Generic;
using System.Linq;
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

        [Test]
        public void It_generates_addresses_correctly()
        {
            Mask mask;
            IEnumerable<long> addresses;

            mask = new Mask("000000000000000000000000000000X1001X");
            addresses = mask.GenerateAddresses(42);
            Assert.IsTrue(new long[] { 26, 27, 58, 59 }.All(a => addresses.Any(ad => a == ad)));

            mask = new Mask("00000000000000000000000000000000X0XX");
            addresses = mask.GenerateAddresses(26);
            Assert.IsTrue(new long[] { 16, 17, 18, 19, 24, 25, 26, 27 }.All(a => addresses.Any(ad => a == ad)));
        }
    }
}