using System;
using System.Linq;
using NUnit.Framework;

namespace Day_05
{
    [TestFixture]
    public class Day_05Tests
    {
        // private static Random random = new Random();
        private static Random random;

        [Test]
        public void It_matches_correctly()
        {
            Assert.IsTrue(Program.UnitsCanReact('A', 'a'));
        }

        [Test]
        public void It_does_not_match_different_chars()
        {
            Assert.IsFalse(Program.UnitsCanReact('A', 'b'));
        }

        [Test]
        public void It_does_not_match_if_case_is_same()
        {
            Assert.IsFalse(Program.UnitsCanReact('A', 'A'));
        }

        [Test]
        public void It_reduces_correctly_01()
        {
            Assert.AreEqual("", Program.ReduceCompletely("aA"));
        }

        [Test]
        public void It_reduces_correctly_02()
        {
            Assert.AreEqual("", Program.ReduceCompletely("abBA"));
        }

        [Test]
        public void It_reduces_correctly_03()
        {
            Assert.AreEqual("abAB", Program.ReduceCompletely("abAB"));
        }

        [Test]
        public void It_reduces_correctly_04()
        {
            Assert.AreEqual("aabAAB", Program.ReduceCompletely("aabAAB"));
        }

        [Test]
        public void It_reduces_correctly_05()
        {
            Assert.AreEqual("dabCBAcaDA", Program.ReduceCompletely("dabAcCaCBAcCcaDA"));
        }

        [Test]
        public void It_reduces_repeated_shrinks_correctly()
        {
            Assert.AreEqual("", Program.ReduceCompletely("AAAABbaaaa"));
        }

        [Test]
        public void It_correctly_reduces_randomly_generated_strings()
        {
            random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            var passed = true;
            for (int i = 0; i < 1_000_000 && passed; ++i)
            {
                var aString = new string(Enumerable.Repeat(chars, 50_000).Select(s => s[random.Next(s.Length)]).ToArray());
                var shrunken = Program.ReduceCompletely(aString);
                if (Enumerable.Range(0, shrunken.Length - 2).Any(i => Program.UnitsCanReact(shrunken[i], shrunken[i + 1])))
                {
                    Console.WriteLine($"full length: {aString}");
                    Console.WriteLine($"shrunken:    {shrunken}");
                    passed = false;
                }
                if (i % 1_000 == 0)
                {
                    Console.WriteLine($"Tested {i + 1} random strings");
                }
            }
            Assert.IsTrue(passed);
        }
    }
}