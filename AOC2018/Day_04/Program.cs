using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day_04
{
    public class Program
    {
        static void Main(string[] args)
        {
            var file = new StreamReader(args[0]);
            var lineList = new List<string>();
            string line;
            while ((line = file.ReadLine()) != null)
            {
                lineList.Add(line);
            }

            var sortedRecordList = lineList.Select(ParseLine).OrderBy(r => r.Time);

            Dictionary<int, List<Nap>> guardRecords = new Dictionary<int, List<Nap>>();
            int currentGuard = 0;
            foreach (var m in sortedRecordList)
            {
                if (typeof(NewGuard).IsInstanceOfType(m))
                {
                    var newGuard = (NewGuard) m;
                    currentGuard = newGuard.GuardId;
                    if (!guardRecords.ContainsKey(currentGuard))
                    {
                        guardRecords.Add(currentGuard, new List<Nap>());
                    }
                }
                else if (typeof(FallsAsleep).IsInstanceOfType(m))
                {
                    var newNap = new Nap { ClosedStart = m.Time };
                    guardRecords[currentGuard].Add(newNap);
                }
                else
                {
                    guardRecords[currentGuard][^1].OpenEnd = m.Time;
                }
            }

            var (mostSleepyGuard, lengthSlept) = guardRecords.Keys.Aggregate((0, 0), (tuple, next) => {
                var thisNapSum = SumNapLengths(guardRecords[next]);
                return thisNapSum > tuple.Item2 ? (next, thisNapSum) : tuple;
            });

            Dictionary<int, int> napHistogram = new Dictionary<int, int>();
            foreach (var nap in guardRecords[mostSleepyGuard])
            {
                for (int m = nap.ClosedStart.Minute; m < nap.OpenEnd.Minute; ++m)
                {
                    if (napHistogram.TryGetValue(m, out var currentValue))
                    {
                        napHistogram[m] = (currentValue + 1);
                    }
                    else
                    {
                        napHistogram.Add(m, 1);
                    }
                }
            }

            var mostSleepyMinute = napHistogram.Keys.Aggregate((prev, next) => napHistogram[next] > napHistogram[prev] ? next : prev);

            Dictionary<int, Dictionary<int, int>> mostSleepyMinuteOfAllHistogram = new Dictionary<int, Dictionary<int, int>>();
            foreach (var guardId in guardRecords.Keys)
            {
                foreach (var nap in guardRecords[guardId])
                {
                    for (var m = nap.ClosedStart.Minute; m < nap.OpenEnd.Minute; ++m)
                    {
                        Dictionary<int, int> currentDictionary;
                        if (!mostSleepyMinuteOfAllHistogram.TryGetValue(m, out currentDictionary))
                        {
                            currentDictionary = new Dictionary<int, int>();
                            mostSleepyMinuteOfAllHistogram[m] = currentDictionary;
                        }
                        if (currentDictionary.TryGetValue(guardId, out var currentValue))
                        {
                            currentDictionary[guardId] = ++currentValue;
                        }
                        else
                        {
                            currentDictionary.Add(guardId, 1);
                        }
                    }
                }
            }
            var mostSleepyGuardPerMinute = mostSleepyMinuteOfAllHistogram.Keys.Select(m =>
            {
                var mostSleepyGuardThisMinute = mostSleepyMinuteOfAllHistogram[m].Keys.Aggregate((prev, next) => mostSleepyMinuteOfAllHistogram[m][next] > mostSleepyMinuteOfAllHistogram[m][prev] ? next : prev);
                return (m, mostSleepyGuardThisMinute);
            });
            var (mostSleepyMinuteOfAll, mostSleepyGuardOfAll) = mostSleepyGuardPerMinute.Aggregate((prevPair, nextPair) =>
                mostSleepyMinuteOfAllHistogram[nextPair.Item1][nextPair.Item2] > mostSleepyMinuteOfAllHistogram[prevPair.Item1][prevPair.Item2]
                ? nextPair
                : prevPair
            );

            Console.WriteLine($"The most sleepy guard is {mostSleepyGuard}, who slept for {lengthSlept} minutes");
            Console.WriteLine($"The most sleepy minute for {mostSleepyGuard} was {mostSleepyMinute} ({napHistogram[mostSleepyMinute]} times)");
            Console.WriteLine($"Value1 = {mostSleepyGuard * mostSleepyMinute}");
            Console.WriteLine($"The most-slept minute was {mostSleepyMinuteOfAll}");
            Console.WriteLine($"The most sleepy guard during that minute was {mostSleepyGuardOfAll}");
            Console.WriteLine($"Value2 = {mostSleepyMinuteOfAll * mostSleepyGuardOfAll}");
        }

        public static IMessage ParseLine(string line)
        {
            var rx = new Regex(@"^\[(\d{4})-(\d{2})-(\d{2}) (\d{2}):(\d{2})] ((Guard #\d+ begins shift$)|(falls asleep)|(wakes up))");
            MatchCollection matches = rx.Matches(line);
            Match match = matches[0];
            var year = Int32.Parse(match.Groups[1].Value);
            var month = Int32.Parse(match.Groups[2].Value);
            var day = Int32.Parse(match.Groups[3].Value);
            var hour = Int32.Parse(match.Groups[4].Value);
            var minute = Int32.Parse(match.Groups[5].Value);
            var message = match.Groups[6].Value;
            var dt = new DateTimeOffset(year, month, day, hour, minute, 0, TimeSpan.Zero);
            if (match.Groups[7].Success)
            {
                return new NewGuard(dt, message);
            }
            else if (match.Groups[8].Success)
            {
                return new FallsAsleep(dt, message);
            }
            else
            {
                return new WakesUp(dt, message);
            }
        }

        public static int SumNapLengths(IEnumerable<Nap> napList)
        {
            return napList.Select(n => n.OpenEnd.Minute - n.ClosedStart.Minute).Sum();
        }
    }
}
