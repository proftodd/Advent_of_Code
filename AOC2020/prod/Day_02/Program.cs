﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day_02
{
    public class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines(args[0]);
            var matches = FindMatches(input, (policy, testString) => policy.IsMatch(testString));
            Console.WriteLine($"{matches.Length} passwords met the policy in force");
            var aMatches = FindMatches(input, (policy, testString) => policy.IsAlternateMatch(testString));
            Console.WriteLine($"{aMatches.Length} passwords met the alternate policy in force");
        }

        public static ValueTuple<Policy, string> ParseInputLine(string line)
        {
            var theRegex = new Regex(@"^(\d+)-(\d+) (\w): (\w+)$");
            var match = theRegex.Matches(line)[0];
            var lowerBound = Int32.Parse(match.Groups[1].Value);
            var upperBound = Int32.Parse(match.Groups[2].Value);
            var targetChar = match.Groups[3].Value;
            var theString = match.Groups[4].Value;
            var thePolicy = new Policy { letter = targetChar[0], min = lowerBound, max = upperBound };
            return (thePolicy, theString);
        }

        public static Dictionary<char, int> CountChars(string targetString)
        {
            var theDictionary = new Dictionary<char, int>();
            foreach (var aChar in targetString)
            {
                if (theDictionary.TryGetValue(aChar, out var count))
                {
                    theDictionary[aChar] = count + 1;
                }
                else
                {
                    theDictionary.Add(aChar, 1);
                }
            }
            return theDictionary;
        }

        public static string[] FindMatches(string[] input, Func<Policy, string, bool> test)
        {
            return input.Select(s => ParseInputLine(s))
                .Where(t => {
                    var (policy, testString) = t;
                    return test(policy, testString);
                })
                .Select(t => t.Item2)
                .ToArray();
        }
    }

    public struct Policy
    {
        public Char letter;
        public int min;
        public int max;

        public override string ToString()
        {
            return $"{min}-{max} {letter}";
        }
    }

    public static class PolicyExtensions
    {
        public static bool IsMatch(this Policy policy, string testString)
        {
            var characterCount = Program.CountChars(testString);
            return characterCount.ContainsKey(policy.letter)
                && characterCount[policy.letter] >= policy.min
                && characterCount[policy.letter] <= policy.max;
        }

        public static bool IsAlternateMatch(this Policy policy, string testString)
        {
            int matchCount = 0;
            if (testString.Length >= policy.min && testString[policy.min - 1] == policy.letter)
            {
                ++matchCount;
            }
            if (testString.Length >= policy.max && testString[policy.max - 1] == policy.letter)
            {
                ++matchCount;
            }
            return matchCount == 1;
        }
    }
}
