using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Day_04
{
    [TestFixture]
    public class Day_04Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void It_parses_a_guard_coming_on_shift()
        {
            var line = "[1518-11-01 00:00] Guard #10 begins shift";
            var message = Day_04.Program.ParseLine(line);

            var expectedDate = new DateTimeOffset(1518, 11, 1, 0, 0, 0, TimeSpan.Zero);
            Assert.IsInstanceOf<NewGuard>(message);
            Assert.AreEqual(expectedDate, message.Time);
            Assert.AreEqual("Guard #10 begins shift", message.Message);
            Assert.AreEqual(10, ((NewGuard)message).GuardId);
        }

        [Test]
        public void It_parses_a_guard_falling_asleep()
        {
            var line = "[1518-11-01 00:00] falls asleep";
            var message = Day_04.Program.ParseLine(line);

            var expectedDate = new DateTimeOffset(1518, 11, 1, 0, 0, 0, TimeSpan.Zero);
            Assert.IsInstanceOf<FallsAsleep>(message);
            Assert.AreEqual(expectedDate, message.Time);
            Assert.AreEqual("falls asleep", message.Message);
        }

        [Test]
        public void It_parses_a_guard_waking_up()
        {
            var line = "[1518-11-01 00:00] wakes up";
            var message = Day_04.Program.ParseLine(line);

            var expectedDate = new DateTimeOffset(1518, 11, 1, 0, 0, 0, TimeSpan.Zero);
            Assert.IsInstanceOf<WakesUp>(message);
            Assert.AreEqual(expectedDate, message.Time);
            Assert.AreEqual("wakes up", message.Message);
        }

        [Test]
        public void It_sums_nap_lengths()
        {
            var napList = new Nap[] {
                new Nap { ClosedStart = new DateTimeOffset(1518, 11, 1, 0, 5, 0, TimeSpan.Zero), OpenEnd = new DateTimeOffset(1518, 11, 1, 0, 25, 0, TimeSpan.Zero) },
                new Nap { ClosedStart = new DateTimeOffset(1518, 11, 1, 0, 30, 0, TimeSpan.Zero), OpenEnd = new DateTimeOffset(1518, 11, 1, 0, 55, 0, TimeSpan.Zero) },
                new Nap { ClosedStart = new DateTimeOffset(1518, 11, 3, 0, 24, 0, TimeSpan.Zero), OpenEnd = new DateTimeOffset(1518, 11, 3, 0, 29, 0, TimeSpan.Zero) }
            };

            var timeSlept = Day_04.Program.SumNapLengths(napList);

            Assert.AreEqual(50, timeSlept);
        }
    }
}