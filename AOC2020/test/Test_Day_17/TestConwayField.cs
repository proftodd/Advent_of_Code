using System.IO;
using NUnit.Framework;

namespace Day_17
{
    public class TestConwayField
    {
        [Test]
        public void It_is_constructed_correctly()
        {
            var lines = File.ReadAllLines("test0.txt");
            var field = new ConwayField(lines);

            Assert.NotNull(field);
            Assert.AreEqual('.', field[(-1, -1, 0)]);
            Assert.AreEqual('#', field[(-1, 0, 0)]);
            Assert.AreEqual('#', field[(0, 1, 0)]);
            Assert.AreEqual('#', field[(1, -1, 0)]);
            Assert.AreEqual('#', field[(1, 0, 0)]);
            Assert.AreEqual('#', field[(1, 1, 0)]);
            Assert.AreEqual(-1, field.MinX);
            Assert.AreEqual(1, field.MaxX);
            Assert.AreEqual(-1, field.MinY);
            Assert.AreEqual(1, field.MaxY);
            Assert.AreEqual(-1, field.MinZ);
            Assert.AreEqual(1, field.MaxZ);
        }

        [Test]
        public void It_parses_iterations_correctly()
        {
            var lines = File.ReadAllLines("test3.txt");
            var field = new ConwayField(lines);

            Assert.AreEqual(-3, field.MinX);
            Assert.AreEqual(3, field.MaxX);
            Assert.AreEqual(-3, field.MinY);
            Assert.AreEqual(3, field.MaxY);
            Assert.AreEqual(-3, field.MinZ);
            Assert.AreEqual(3, field.MaxZ);
        }

        [Test]
        public void It_counts_population_correctly()
        {
            var lines = File.ReadAllLines("test0.txt");
            var field = new ConwayField(lines);

            Assert.AreEqual(5, field.Population);
        }

        [Test]
        public void It_counts_neighbors_correctly()
        {
            var lines = File.ReadAllLines("test3.txt");
            var field = new ConwayField(lines);

            Assert.AreEqual(7, field.NeighborCount(2, 2, 0));
            Assert.AreEqual(0, field.NeighborCount(100, 100, 100));
        }

        [Test]
        public void It_compares_fields_correctly()
        {
            var lines = File.ReadAllLines("test0.txt");
            var f1 = new ConwayField(lines);
            var f2 = new ConwayField(lines);
            lines[0] = ".##";
            var f3 = new ConwayField(lines);

            Assert.IsTrue(f1.Equals(f1));
            Assert.IsTrue(f1.Equals(f2));
            Assert.IsFalse(f1.Equals(f3));
        }

        [Test]
        public void It_iterates_correctly()
        {
            var field = new ConwayField(File.ReadAllLines("test0.txt"));
            ConwayField test;
            for (int i = 1; i <= 3; ++i)
            {
                test = new ConwayField(File.ReadAllLines($"test{i}.txt"));
                field = field.Iterate();
                Assert.IsTrue(test.Equals(field));
            }
        }
    }
}
