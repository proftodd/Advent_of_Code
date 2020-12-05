using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_04
{
    public class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines(args[0]);
            var valid = ParseRecords(input)
                .Select(l => new Passport(l))
                .Where(p => p.IsValid());
            Console.WriteLine($"Valid passport count: {valid.Count()}");
        }

        public static List<string> ParseRecords(string[] lines)
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
                        var s2 = string.IsNullOrWhiteSpace(l[^1]) ? s : l[^1] + " " + s;
                        return l2.Concat(new[] { s2 }).ToList<string>();
                    }
                }
            );
        }
    }

    public class Passport
    {
        public Passport(string record)
        {
            var fields = record.Split();
            foreach(var f in fields)
            {
                var pair = f.Split(":");
                switch(pair[0])
                {
                    case "byr":
                        BirthYear = pair[1];
                        break;
                    case "iyr":
                        IssueYear = pair[1];
                        break;
                    case "eyr":
                        ExpirationYear = pair[1];
                        break;
                    case "hgt":
                        Height = pair[1];
                        break;
                    case "hcl":
                        HairColor = pair[1];
                        break;
                    case "ecl":
                        EyeColor = pair[1];
                        break;
                    case "pid":
                        PassportID = pair[1];
                        break;
                    case "cid":
                        CountryID = pair[1];
                        break;
                    default:
                        Console.WriteLine($"Unrecognized field: {pair[0]}");
                        break;
                }
            }
        }

        public string BirthYear { get; }

        public string IssueYear { get; }

        public string ExpirationYear { get; }

        public string Height { get; }

        public string HairColor { get; }

        public string EyeColor { get; }

        public string PassportID { get; }

        public string CountryID { get; }

        public override string ToString()
        {
            return $"byr:{BirthYear} iyr:{IssueYear} eyr:{ExpirationYear} hgt:{Height} hcl:{HairColor} ecl:{EyeColor} pid:{PassportID}{(CountryID == null ? "" : " cid:" + CountryID)}";
        }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(BirthYear)
                && !string.IsNullOrWhiteSpace(IssueYear)
                && !string.IsNullOrWhiteSpace(ExpirationYear)
                && !string.IsNullOrWhiteSpace(Height)
                && !string.IsNullOrWhiteSpace(HairColor)
                && !string.IsNullOrWhiteSpace(EyeColor)
                && !string.IsNullOrWhiteSpace(PassportID);
        }
    }
}
