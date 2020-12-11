using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Shared;

namespace Day_06
{
    public class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines(args[0]);
            var groupAnswerSets = CollectionUtils.CollectRecords(input)
                .Select(l => l.Split())
                .Select(ls => ls.Select(s => CountAnsweredQuestions(s)));
            var anyAnswered = groupAnswerSets.Select(ld => ld.Aggregate((d, d1) => {
                var nd = new Dictionary<char, int>(d);
                foreach (var c in d1.Keys)
                {
                    nd.TryAdd(c, d1[c]);
                }
                return nd;
            }));
            var anyAnsweredCount = anyAnswered.Select(d => d.Keys.Count()).Sum();
            Console.WriteLine($"Sum of answered question sets: {anyAnsweredCount}");
        }

        public static List<string> ParseGroupAnswers(string[] lines)
        {
            return lines.Aggregate(
                new List<string>(new[] { "" }),
                (l, s) => {
                    if (string.IsNullOrWhiteSpace(s))
                    {
                        return l.Concat(new[] { "" }).ToList<string>();
                    }
                    else
                    {
                        var l2 = l.GetRange(0, l.Count() - 1);
                        var s2 = string.IsNullOrWhiteSpace(l[^1]) ? s : l[^1] + "" + s;
                        return l2.Concat(new[] { s2 }).ToList<string>();
                    }
                }
            );
        }

        public static Dictionary<char, int> CountAnsweredQuestions(string input)
        {
            var ret = input
                .GroupBy(
                    c => c,
                    c => c,
                    (c, l) => (c, l.Count())
                )
                .ToDictionary(t => t.Item1, t => t.Item2);
            return ret;
        }
    }
}
