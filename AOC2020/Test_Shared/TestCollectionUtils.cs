using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Shared
{
    public class TestCollectionUtils
    {
        [Test]
        public void It_collects_lines_of_text_into_records()
        {
            string[] input = {
                "ecl:gry pid:860033327 eyr:2020 hcl:#fffffd",
                "byr:1937 iyr:2017 cid:147 hgt:183cm",
                "",
                "iyr:2013 ecl:amb cid:350 eyr:2023 pid:028048884",
                "hcl:#cfa07d byr:1929",
                "",
                "hcl:#ae17e1 iyr:2013",
                "eyr:2024",
                "ecl:brn pid:760753108 byr:1931",
                "hgt:179cm",
                "",
                "hcl:#cfa07d eyr:2025 pid:166559648",
                "iyr:2011 ecl:brn hgt:59in"
            };

            var records = CollectionUtils.CollectRecords(input);

            Assert.AreEqual(4, records.Count());
            Assert.IsTrue(records.Contains("ecl:gry pid:860033327 eyr:2020 hcl:#fffffd byr:1937 iyr:2017 cid:147 hgt:183cm"));
            Assert.IsTrue(records.Contains("iyr:2013 ecl:amb cid:350 eyr:2023 pid:028048884 hcl:#cfa07d byr:1929"));
            Assert.IsTrue(records.Contains("hcl:#ae17e1 iyr:2013 eyr:2024 ecl:brn pid:760753108 byr:1931 hgt:179cm"));
            Assert.IsTrue(records.Contains("hcl:#cfa07d eyr:2025 pid:166559648 iyr:2011 ecl:brn hgt:59in"));
        }
    }
}