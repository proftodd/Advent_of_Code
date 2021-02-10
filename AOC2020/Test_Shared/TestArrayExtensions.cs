using NUnit.Framework;

namespace Shared
{
    public class TestArrayExtensions
    {
        [Test]
        public void It_returns_false_for_arrays_of_different_lengths()
        {
            var me = new[] { 1, 2, 3 };
            var you = new[] { 1, 2 };
            Assert.IsFalse(me.ElementsAreEqual(you));
        }

        [Test]
        public void It_returns_false_for_arrays_with_different_elements()
        {
            var me = new[] { 1, 2, 3 };
            var you = new[] { 1, 2, 4 };
            Assert.IsFalse(me.ElementsAreEqual(you));
        }

        [Test]
        public void It_returns_true_if_arrays_have_same_elements()
        {
            var me = new[] { 1, 2, 3, 3, 4 };
            var you = new[] { 1, 2, 3, 3, 4 };
            Assert.IsTrue(me.ElementsAreEqual(you));
        }
    }
}