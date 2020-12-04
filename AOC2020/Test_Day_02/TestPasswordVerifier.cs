using System.Collections.Generic;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace Day_02
{
    public class TestPasswordVerifier
    {
        [Test]
        public void It_parses_input_line()
        {
            var input = "1-3 a: abcde";
            var expectedPolicy = new Policy { letter = 'a', min = 1, max = 3 };
            var expectedString = "abcde";
            var (observedPolicy, observedString) = Program.ParseInputLine(input);
            Assert.AreEqual(expectedPolicy, observedPolicy);
            Assert.AreEqual(expectedString, observedString);
        }

        [Test]
        public void It_counts_characters()
        {
            var targetString = "cbbadada";
            var expectedCount = new Dictionary<char, int>(
                new[] {
                    new KeyValuePair<char, int>('a', 3),
                    new KeyValuePair<char, int>('b', 2),
                    new KeyValuePair<char, int>('c', 1),
                    new KeyValuePair<char, int>('d', 2)
                }
            );
            Assert.AreEqual(expectedCount, Program.CountChars(targetString));
        }

        [Test]
        public void It_correctly_compares_strings_to_policies()
        {
            var thePolicy = new Policy { letter = 'a', min = 2, max = 3 };
            Assert.IsTrue(thePolicy.IsMatch("aa"));
            Assert.IsTrue(thePolicy.IsMatch("aaa"));
            Assert.IsTrue(thePolicy.IsMatch("aba"));
            Assert.IsFalse(thePolicy.IsMatch("ab"));
            Assert.IsFalse(thePolicy.IsMatch("aaaa"));
            Assert.IsFalse(thePolicy.IsMatch("aabaa"));
        }

        [Test]
        public void It_counts_correct_passwords()
        {
            string[] input = {
                "1-3 a: abcde",
                "1-3 b: cdefg",
                "2-9 c: ccccccccc"
            };
            var matches = Program.FindMatches(input);
            Assert.AreEqual(2, matches.Length);
        }
    }
}