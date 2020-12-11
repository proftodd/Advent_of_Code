using System;
using NUnit.Framework;

namespace Day_07
{
    public class TestBag
    {
        [Test]
        public void Bag_equality_comparison_works()
        {
            var b1 = new Bag("blue", "light");
            var b2 = new Bag("blue", "dark");
            var b3 = new Bag("red", "light");
            var b4 = new Bag("blue", "light");
            b1.AddCargo(b4, 1);
            Assert.AreEqual(b1, b4);
            Assert.IsTrue(b1.Equals(b4));
            Assert.IsFalse(b1.Equals(b2));
            Assert.IsFalse(b1.Equals(b3));
        }

        [Test]
        public void Bag_cannot_hold_same_bag_twice()
        {
            Bag container = new Bag("blue", "light");
            Bag cargo = new Bag("red", "dark");
            container.AddCargo(cargo, 1);
            Assert.Throws<ArgumentException>(() => container.AddCargo(cargo, 1));
        }

        [Test]
        public void Bag_reports_whether_another_bag_can_go_in_it()
        {
            Bag container = new Bag("blue", "light");
            Bag cargo = new Bag("red", "dark");
            container.AddCargo(cargo, 1);

            Assert.IsTrue(container.CanContain(new Bag("red", "dark")));
            Assert.IsFalse(container.CanContain(new Bag("red", "light")));
        }

        [Test]
        public void Bag_reports_whether_another_bag_can_contain_it()
        {
            Bag container = new Bag("blue", "light");
            Bag cargo = new Bag("red", "dark");
            cargo.SetContainedBy(container);

            Assert.IsTrue(cargo.CanBeIn(new Bag("blue", "light")));
            Assert.IsFalse(cargo.CanBeIn(new Bag("red", "light")));
        }
    }
}