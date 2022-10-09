using System;
using System.Collections.Generic;
using System.IO;

namespace Day_02
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args[0] == "c")
            {
                CalculateChecksums(args[1]);
            }
            else if (args[0] == "m")
            {
                MatchBoxes(args[1]);
            }
        }

        public static void CalculateChecksums(string fileName)
        {
            var file = new StreamReader(fileName);
            string line;
            var twoLetterCount = 0;
            var threeLetterCount = 0;
            while ((line = file.ReadLine()) != null)
            {
                var hist = CountChars(line);
                if (hist.ContainsValue(2))
                {
                    ++twoLetterCount;
                }
                if (hist.ContainsValue(3))
                {
                    ++threeLetterCount;
                }
            }
            Console.WriteLine($"The checksum is {twoLetterCount * threeLetterCount}");
        }

        public static Dictionary<char, int> CountChars(string boxId)
        {
            var histogram = new Dictionary<Char, Int32>();
            foreach (var aChar in boxId)
            {
                if (histogram.ContainsKey(aChar))
                {
                    histogram[aChar]++;
                }
                else
                {
                    histogram[aChar] = 1;
                }
            }
            return histogram;
        }

        public static void MatchBoxes(string fileName)
        {
            var boxArray = File.ReadAllLines(fileName);
            var matches = new List<(string, string)>();
            for (int i = 0; i < boxArray.Length - 1; ++i)
            {
                for (int j = i + 1; j < boxArray.Length; ++j)
                {
                    if (IdsMatch(boxArray[i], boxArray[j]))
                    {
                        matches.Add((boxArray[i], boxArray[j]));
                    }
                }
            }
            foreach (var (id1, id2) in matches)
            {
                Console.WriteLine($"Match found:");
                Console.WriteLine($"\t{id1}");
                Console.WriteLine($"\t{id2}");
            }
        }

        public static bool IdsMatch(string id1, string id2)
        {
            var mismatchedChars = 0;
            for (int i = 0; i < id1.Length && i < id2.Length; ++i)
            {
                if (id1[i] != id2[i])
                {
                    ++mismatchedChars;
                }
                if (mismatchedChars > 1)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
