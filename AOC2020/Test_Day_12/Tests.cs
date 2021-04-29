using System.IO;
using NUnit.Framework;

namespace Day_12
{
    public class Tests
    {
        [Test]
        public void It_responds_correctly_to_example_instructions()
        {
            var ferry = new Ferry();
            var instructions = File.ReadLines("test.txt");
            foreach (var instruction in instructions)
            {
                Program.UpdateFerry(ferry, instruction);
            }
            Assert.AreEqual(17, ferry.X);
            Assert.AreEqual(-8, ferry.Y);
            Assert.AreEqual(25, ferry.ManhattanDistance());
        }

        [Test]
        public void It_responds_correctly_to_U_turns()
        {
            var ferry = new Ferry();
            Program.UpdateFerry(ferry, "L180");
            Assert.AreEqual(180, ferry.Heading);
            Program.UpdateFerry(ferry, "L180");
            Assert.AreEqual(0, ferry.Heading);
            Program.UpdateFerry(ferry, "R180");
            Assert.AreEqual(180, ferry.Heading);
            Program.UpdateFerry(ferry, "R180");
            Assert.AreEqual(0, ferry.Heading);
        }
    }
}