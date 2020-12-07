using NUnit.Framework;

namespace Day_05
{
    public class TestRangeReducer
    {
        [Test]
        public void It_reduces_ranges_correctly()
        {
            var input = (0, 127);
            Assert.AreEqual((0, 63), Program.ReduceRange(input, 'F'));
            Assert.AreEqual((0, 63), Program.ReduceRange(input, 'L'));
            Assert.AreEqual((64, 127), Program.ReduceRange(input, 'B'));
            Assert.AreEqual((64, 127), Program.ReduceRange(input, 'R'));
        }

        [Test]
        public void It_scans_boarding_passes()
        {
            Assert.AreEqual((44, 5), Program.GetSeatAssignment("FBFBBFFRLR"));
            Assert.AreEqual((70, 7), Program.GetSeatAssignment("BFFFBBFRRR"));
            Assert.AreEqual((14, 7), Program.GetSeatAssignment("FFFBBBFRRR"));
            Assert.AreEqual((102, 4), Program.GetSeatAssignment("BBFFBBFRLL"));
        }

        [Test]
        public void It_calculates_seat_ids()
        {
            Assert.AreEqual(357, Program.GetSeatID("FBFBBFFRLR"));
            Assert.AreEqual(567, Program.GetSeatID("BFFFBBFRRR"));
            Assert.AreEqual(119, Program.GetSeatID("FFFBBBFRRR"));
            Assert.AreEqual(820, Program.GetSeatID("BBFFBBFRLL"));
        }
    }
}