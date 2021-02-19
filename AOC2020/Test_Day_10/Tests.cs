using System.IO;
using System.Linq;
using NUnit.Framework;

namespace Day_10
{
    public class Tests
    {
        [Test]
        public void It_counts_combinations_correctly_for_first_test_input()
        {
            var adapters = File.ReadLines("test1.txt")
                .Select(int.Parse)
                .OrderBy(a => a)
                .ToArray();
            var configs = Program.CountConfigurations(adapters);
            Assert.AreEqual(8L, Program.CountConfigurations(adapters));
        }

        [Test]
        public void It_counts_combinations_correctly_for_second_test_input()
        {
            var adapters = File.ReadLines("test2.txt")
                .Select(int.Parse)
                .OrderBy(a => a)
                .ToArray();
            Assert.AreEqual(19208, Program.CountConfigurations(adapters));
        }
    }
}