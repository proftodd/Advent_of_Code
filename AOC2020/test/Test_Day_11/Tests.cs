using System.Linq;
using NUnit.Framework;

namespace Day_11
{
    public class Tests
    {
        [Test]
        public void It_reads_a_floor_plan()
        {
            var fp = Program.ReadFloorPlan("test1.txt");
            Assert.AreEqual(10, fp.Length);
            Assert.IsTrue(fp.All(l => l.Length == 10));
            Assert.AreEqual(71, Program.CountEmptyChairs(fp));
            Assert.AreEqual(0, Program.CountOccupiedChairs(fp));
        }

        [Test]
        public void It_counts_adjacent_filled_chairs()
        {
            var fp = Program.ReadFloorPlan("test3.txt");
            Assert.AreEqual(1, Program.CountAdjacentFilledSeats(fp, 5, 5));
            Assert.AreEqual(2, Program.CountAdjacentFilledSeats(fp, 5, 7));
            Assert.AreEqual(2, Program.CountAdjacentFilledSeats(fp, 0, 1));
            Assert.AreEqual(2, Program.CountAdjacentFilledSeats(fp, 6, 0));
            Assert.AreEqual(1, Program.CountAdjacentFilledSeats(fp, 0, 0));
            Assert.AreEqual(2, Program.CountAdjacentFilledSeats(fp, 0, 9));
            Assert.AreEqual(1, Program.CountAdjacentFilledSeats(fp, 9, 0));
            Assert.AreEqual(1, Program.CountAdjacentFilledSeats(fp, 9, 9));
        }

        [Test]
        public void It_iterates_correctly()
        {
            var floorPlan = Program.ReadFloorPlan("test1.txt");
            char[][] observedNextFloorPlan;
            char[][] expectedNextFloorPlan;
            for (int k = 2; k <= 6; ++k)
            {
                observedNextFloorPlan = Program.Iterate(floorPlan, Program.AdjacentSeatCriterion);
                expectedNextFloorPlan = Program.ReadFloorPlan($"test{k}.txt");
                Assert.IsTrue(Program.AreSame(expectedNextFloorPlan, observedNextFloorPlan));
                floorPlan = observedNextFloorPlan;
            }

            observedNextFloorPlan = Program.Iterate(floorPlan, Program.AdjacentSeatCriterion);
            Assert.IsTrue(Program.AreSame(floorPlan, observedNextFloorPlan));

            Assert.AreEqual(37, Program.CountOccupiedChairs(floorPlan));
        }

        [Test]
        public void It_counts_visible_seats_correctly()
        {
            var fpa = Program.ReadFloorPlan("testa.txt");
            Assert.AreEqual(8, Program.CountVisibleFilledSeats(fpa, 4, 3));
            var fpb = Program.ReadFloorPlan("testb.txt");
            Assert.AreEqual(0, Program.CountVisibleFilledSeats(fpb, 1, 1));
            var fpc = Program.ReadFloorPlan("testc.txt");
            Assert.AreEqual(0, Program.CountVisibleFilledSeats(fpc, 3, 3));
        }
    }
}