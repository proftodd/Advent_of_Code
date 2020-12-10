using System.Collections.Generic;
using NUnit.Framework;

namespace Day_06
{
    public class TestQuestionnaire
    {
        [Test]
        public void It_counts_answered_questions()
        {
            var input = "abcxabcyabcz";
            var expected = new Dictionary<char, int>(new[] {
                new KeyValuePair<char, int>('a', 3),
                new KeyValuePair<char, int>('b', 3),
                new KeyValuePair<char, int>('c', 3),
                new KeyValuePair<char, int>('x', 1),
                new KeyValuePair<char, int>('y', 1),
                new KeyValuePair<char, int>('z', 1)
            });
            Assert.AreEqual(expected, Program.CountAnsweredQuestions(input));
        }

        [Test]
        public void It_collects_groups_of_answer_sets_to_group_records()
        {
            string[] input = {
                "abc",
                "",
                "a",
                "b",
                "c",
                "",
                "ab",
                "ac",
                "",
                "a",
                "a",
                "a",
                "a",
                "",
                "b"
            };
            var expected = new List<string>(new[] {
                "abc",
                "abc",
                "abac",
                "aaaa",
                "b"
            });
            Assert.AreEqual(expected, Program.ParseGroupAnswers(input));
        }
    }
}