using System;
using System.Collections.Generic;
using System.IO;

namespace Day_01
{
    class Program
    {
        static void Main(string[] args)
        {
            var file = new StreamReader(args[0]);
            var (finalFrequency, freqChanges) = GetFinalFrequency(0, file);
            var firstRepeat = GetFirstRepeat(0, freqChanges);
            Console.WriteLine($"The final frequency is {finalFrequency}");
            Console.WriteLine($"The first repeated frequency is {firstRepeat}");
        }

        public static (int, int[]) GetFinalFrequency(int startingFrequency, StreamReader frequencyChanges)
        {
            var currentFrequency = startingFrequency;
            string line;
            var freqList = new List<int>();
            while ((line = frequencyChanges.ReadLine()) != null)
            {
                var theChange = Int32.Parse(line);
                var newFrequency = currentFrequency + theChange;
                Console.WriteLine($"Current frequency {currentFrequency}, change of {theChange}; resulting frequency {newFrequency}");
                currentFrequency = newFrequency;
                freqList.Add(theChange);
            }
            return (currentFrequency, freqList.ToArray());
        }

        public static int GetFirstRepeat(int startingFrequency, int[] freqChanges)
        {
            var observedFrequencies = new SortedSet<int>();
            var i = 0;
            var aFrequency = startingFrequency;
            while (!observedFrequencies.Contains(aFrequency))
            {
                observedFrequencies.Add(aFrequency);
                aFrequency = aFrequency + freqChanges[i];
                i = (i + 1) % freqChanges.Length;
            }
            return aFrequency;
        }
    }
}
