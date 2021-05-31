using System.IO;
using NUnit.Framework;

namespace Day_12
{
    public class TestShip
    {
        [Test]
        public void It_responds_correctly_to_example_instructions()
        {
            var ship = new Ship();
            var instructions = File.ReadLines("test.txt");
            foreach (var instruction in instructions)
            {
                ship.Steer(instruction);
            }

            Assert.AreEqual(214, ship.X);
            Assert.AreEqual(-72, ship.Y);
            Assert.AreEqual(286, ship.ManhattanDistance());
        }

        [Test]
        public void Waypoint_responds_correctly_to_rotations()
        {
            var ship = new Ship();
            Assert.AreEqual((10, 1), ship.Waypoint);
            ship.Steer("R90");
            Assert.AreEqual((1, -10), ship.Waypoint);
            ship.Steer("R90");
            Assert.AreEqual((-10, -1), ship.Waypoint);
            ship.Steer("R90");
            Assert.AreEqual((-1, 10), ship.Waypoint);
            ship.Steer("R90");
            Assert.AreEqual((10, 1), ship.Waypoint);
            ship.Steer("L90");
            Assert.AreEqual((-1, 10), ship.Waypoint);
            ship.Steer("L90");
            Assert.AreEqual((-10, -1), ship.Waypoint);
            ship.Steer("L90");
            Assert.AreEqual((1, -10), ship.Waypoint);
            ship.Steer("L90");
            Assert.AreEqual((10, 1), ship.Waypoint);
        }
    }
}
