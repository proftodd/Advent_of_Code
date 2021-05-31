using System;
using System.Collections.Generic;
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

        [Test]
        public void It_counts_transitive_containers_correctly()
        {
            var cargo = new Bag("blue", "light");
            var container1 = new Bag("red", "dark");
            var container2 = new Bag("green", "forest");
            var container3 = new Bag("yellow", "sunny");
            cargo.SetContainedBy(container1);
            cargo.SetContainedBy(container2);
            container1.SetContainedBy(container3);
            ISet<Bag> expectedContainers = new HashSet<Bag>(new[] { container1, container2, container3 });
            var transitiveContainers = cargo.GetTransitiveContainers(cargo);
            transitiveContainers.ExceptWith(expectedContainers);
            Assert.AreEqual(0, transitiveContainers.Count);
        }

        [Test]
        public void Bag_reports_zero_contents_if_it_contains_no_other_bags()
        {
            Bag bag = new Bag("blue", "light");
            Assert.AreEqual(0, bag.GetTransitiveContentCount());
        }

        [Test]
        public void Bag_reports_correct_transitive_content_count()
        {
            var cargo = new Bag("blue", "light");
            var container1 = new Bag("red", "dark");
            var container2 = new Bag("green", "forest");
            var container3 = new Bag("yellow", "sunny");
            container1.AddCargo(cargo, 2);
            container2.AddCargo(cargo, 2);
            container3.AddCargo(container1, 2);
            Assert.AreEqual(6, container3.GetTransitiveContentCount());
        }
    }
}