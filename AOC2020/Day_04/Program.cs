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
            var input = File.ReadAllLines(args[0]);
            var passports = ParseRecords(input)
                .Select(l => new Passport(l));
            var valid = passports.Where(p => p.IsValid());
            var strictValid = valid.Where(p => p.IsStrictValid());
            Console.WriteLine($"Valid passport count: {valid.Count()}");
            Console.WriteLine($"Strict valid count: {strictValid.Count()}");
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

        public bool IsStrictValid()
        {
            return ValidBirthYear(BirthYear)
                && ValidIssueYear(IssueYear)
                && ValidExpirationYear(ExpirationYear)
                && ValidHeight(Height)
                && ValidHairColor(HairColor)
                && ValidEyeColor(EyeColor)
                && ValidPassportID(PassportID);
        }

        public static bool ValidBirthYear(string byr)
        {
            if (byr == null)
            {
                return false;
            }

            int birthYear = int.Parse(byr);
            return birthYear >= 1920 && birthYear <= 2002;
        }

        public static bool ValidIssueYear(string iyr)
        {
            if (iyr == null)
            {
                return false;
            }

            int issueYear = int.Parse(iyr);
            return issueYear >= 2010 && issueYear <= 2020;
        }

        public static bool ValidExpirationYear(string eyr)
        {
            if (eyr == null)
            {
                return false;
            }

            int expirationYear = int.Parse(eyr);
            return expirationYear >= 2020 && expirationYear <= 2030;
        }

        public static bool ValidHeight(string hgt)
        {
            if (hgt == null)
            {
                return false;
            }

            var heightValidator = new Regex(@"^(\d+)(cm|in)$");
            var matches = heightValidator.Matches(hgt);
            if (matches.Count != 1)
            {
                return false;
            }

            var measure = int.Parse(matches[0].Groups[1].Value);
            var unit = matches[0].Groups[2].Value;
            if (unit == "cm")
            {
                return measure >= 150 && measure <= 193;
            }
            else if (unit == "in")
            {
                return measure >= 59 && measure <= 76;
            }
            else
            {
                return false;
            }
        }

        public static bool ValidHairColor(string hcl)
        {
            if (hcl == null)
            {
                return false;
            }

            var hairColorValidator = new Regex(@"^#[0-9a-f]{6}$");
            return hairColorValidator.Matches(hcl).Count == 1;
        }

        public static bool ValidEyeColor(string ecl)
        {
            return ecl == "amb"
                || ecl == "blu"
                || ecl == "brn"
                || ecl == "gry"
                || ecl == "grn"
                || ecl == "hzl"
                || ecl == "oth";
        }

        public static bool ValidPassportID(string pid)
        {
            if (pid == null)
            {
                return false;
            }

            var passportIdValidator = new Regex(@"^\d{9}$");
            return passportIdValidator.Matches(pid).Count == 1;
        }
    }
}
