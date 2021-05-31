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
                .Select(ls => ls.Select(s => new HashSet<char>(s)));
            var anyAnswered = groupAnswerSets.Select(ld => ld.Aggregate((d, d1) => {
                var ns = new HashSet<char>(d);
                ns.UnionWith(d1);
                return ns;
            }));
            var anyAnsweredCount = anyAnswered.Select(d => d.Count()).Sum();
            var allAnswered = groupAnswerSets.Select(ld => ld.Aggregate((d, d1) => {
                var ns = new HashSet<char>(d);
                ns.IntersectWith(d1);
                return ns;
            }));
            var allAnsweredCount = allAnswered.Select(d => d.Count()).Sum();
            Console.WriteLine($"Sum of answered question sets (any answered): {anyAnsweredCount}");
            Console.WriteLine($"Sum of answered question sets (all answered): {allAnsweredCount}");
        }
    }
}
