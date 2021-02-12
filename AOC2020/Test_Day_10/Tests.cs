using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Shared;

namespace Day_10
{
    public class Tests
    {
        [Test]
        public void It_counts_combinations_correctly_for_first_test_input()
        {
            var adapters = File.ReadLines("../../../../../input/2020/Day_10/test1.txt")
                .Select(int.Parse)
                .OrderBy(a => a)
                .ToArray();
            var configs = Program.GenerateConfigurations(adapters);
            Assert.AreEqual(8, configs.Values.Select(l => l.Count()).Sum());
            var expectedCombinations = @"(0), 1, 4, 5, 6, 7, 10, 11, 12, 15, 16, 19, (22)
(0), 1, 4, 5, 6, 7, 10, 12, 15, 16, 19, (22)
(0), 1, 4, 5, 7, 10, 11, 12, 15, 16, 19, (22)
(0), 1, 4, 5, 7, 10, 12, 15, 16, 19, (22)
(0), 1, 4, 6, 7, 10, 11, 12, 15, 16, 19, (22)
(0), 1, 4, 6, 7, 10, 12, 15, 16, 19, (22)
(0), 1, 4, 7, 10, 11, 12, 15, 16, 19, (22)
(0), 1, 4, 7, 10, 12, 15, 16, 19, (22)"
            .Split('\n')
            .Select(l => l.Split(", "))
            .Select(l => l.Where(e => !e.Contains("(")))
            .Select(l => l.Select(int.Parse))
            .Select(l => l.ToArray())
            .ToArray();
            Assert.IsTrue(expectedCombinations.All(c => configs[c.Length].Any(cc => cc.ElementsAreEqual(c))));
        }

        [Test]
        public void It_counts_combinations_correctly_for_second_test_input()
        {
            var adapters = File.ReadLines("../../../../../input/2020/Day_10/test2.txt")
                .Select(int.Parse)
                .OrderBy(a => a)
                .ToArray();
            var configs = Program.GenerateConfigurations(adapters);
            Assert.AreEqual(19208, configs.Values.Select(l => l.Count()).Sum());
            var expectedCombinations = @"(0), 1, 2, 3, 4, 7, 8, 9, 10, 11, 14, 17, 18, 19, 20, 23, 24, 25, 28, 31, 32, 33, 34, 35, 38, 39, 42, 45, 46, 47, 48, 49, (52)
(0), 1, 2, 3, 4, 7, 8, 9, 10, 11, 14, 17, 18, 19, 20, 23, 24, 25, 28, 31, 32, 33, 34, 35, 38, 39, 42, 45, 46, 47, 49, (52)
(0), 1, 2, 3, 4, 7, 8, 9, 10, 11, 14, 17, 18, 19, 20, 23, 24, 25, 28, 31, 32, 33, 34, 35, 38, 39, 42, 45, 46, 48, 49, (52)
(0), 1, 2, 3, 4, 7, 8, 9, 10, 11, 14, 17, 18, 19, 20, 23, 24, 25, 28, 31, 32, 33, 34, 35, 38, 39, 42, 45, 46, 49, (52)
(0), 1, 2, 3, 4, 7, 8, 9, 10, 11, 14, 17, 18, 19, 20, 23, 24, 25, 28, 31, 32, 33, 34, 35, 38, 39, 42, 45, 47, 48, 49, (52)
(0), 3, 4, 7, 10, 11, 14, 17, 20, 23, 25, 28, 31, 34, 35, 38, 39, 42, 45, 46, 48, 49, (52)
(0), 3, 4, 7, 10, 11, 14, 17, 20, 23, 25, 28, 31, 34, 35, 38, 39, 42, 45, 46, 49, (52)
(0), 3, 4, 7, 10, 11, 14, 17, 20, 23, 25, 28, 31, 34, 35, 38, 39, 42, 45, 47, 48, 49, (52)
(0), 3, 4, 7, 10, 11, 14, 17, 20, 23, 25, 28, 31, 34, 35, 38, 39, 42, 45, 47, 49, (52)
(0), 3, 4, 7, 10, 11, 14, 17, 20, 23, 25, 28, 31, 34, 35, 38, 39, 42, 45, 48, 49, (52)"
            .Split('\n')
            .Select(l => l.Split(", "))
            .Select(l => l.Where(e => !e.Contains("(")))
            .Select(l => l.Select(int.Parse))
            .Select(l => l.ToArray())
            .ToArray();
            // var allPassed = true;
            // foreach (var comb in expectedCombinations)
            // {
            //     if (!configs.Any(c => c.ElementsAreEqual(comb)))
            //     {
            //         Console.WriteLine($"Not found: {string.Join(',', comb.Select(a => a.ToString()))}");
            //         allPassed = false;
            //     }
            // }
            // Assert.IsTrue(allPassed);
            Assert.IsTrue(expectedCombinations.All(c => configs[c.Length].Any(cc => cc.ElementsAreEqual(c))));
        }
    }
}