using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Day_04
{
    public class TestPassportVerify
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

            var records = Program.ParseRecords(input);

            Assert.AreEqual(4, records.Count());
            Assert.IsTrue(records.Contains("ecl:gry pid:860033327 eyr:2020 hcl:#fffffd byr:1937 iyr:2017 cid:147 hgt:183cm"));
            Assert.IsTrue(records.Contains("iyr:2013 ecl:amb cid:350 eyr:2023 pid:028048884 hcl:#cfa07d byr:1929"));
            Assert.IsTrue(records.Contains("hcl:#ae17e1 iyr:2013 eyr:2024 ecl:brn pid:760753108 byr:1931 hgt:179cm"));
            Assert.IsTrue(records.Contains("hcl:#cfa07d eyr:2025 pid:166559648 iyr:2011 ecl:brn hgt:59in"));
        }
    }

    public class TestPassport
    {
        [Test]
        public void It_constructs_valid_passport_record()
        {
            var p = new Passport("ecl:gry pid:860033327 eyr:2020 hcl:#fffffd byr:1937 iyr:2017 cid:147 hgt:183cm");
            Assert.AreEqual("byr:1937 iyr:2017 eyr:2020 hgt:183cm hcl:#fffffd ecl:gry pid:860033327 cid:147", p.ToString());
            Assert.IsTrue(p.IsValid());
        }

        [Test]
        public void It_constructs_valid_passport_record_with_no_cid()
        {
            var p = new Passport("hcl:#ae17e1 iyr:2013 eyr:2024 ecl:brn pid:760753108 byr:1931 hgt:179cm");
            Assert.AreEqual("byr:1931 iyr:2013 eyr:2024 hgt:179cm hcl:#ae17e1 ecl:brn pid:760753108", p.ToString());
            Assert.IsTrue(p.IsValid());
        }

        [Test]
        public void It_constructs_invalid_passport_record()
        {
            var p = new Passport("iyr:2013 ecl:amb cid:350 eyr:2023 pid:028048884 hcl:#cfa07d byr:1929");
            Assert.AreEqual("byr:1929 iyr:2013 eyr:2023 hgt: hcl:#cfa07d ecl:amb pid:028048884 cid:350", p.ToString());
            Assert.IsFalse(p.IsValid());
        }

        [Test]
        public void It_validates_birth_year()
        {
            Assert.IsTrue(Passport.ValidBirthYear("2002"));
            Assert.IsFalse(Passport.ValidBirthYear("2003"));
        }

        [Test]
        public void It_validates_height()
        {
            Assert.IsTrue(Passport.ValidHeight("60in"));
            Assert.IsTrue(Passport.ValidHeight("190cm"));
            Assert.IsFalse(Passport.ValidHeight("190in"));
            Assert.IsFalse(Passport.ValidHeight("190"));
        }

        [Test]
        public void It_validates_hair_color()
        {
            Assert.IsTrue(Passport.ValidHairColor("#123abc"));
            Assert.IsFalse(Passport.ValidHairColor("#123abz"));
            Assert.IsFalse(Passport.ValidHairColor("123abc"));
        }

        [Test]
        public void It_validates_eye_color()
        {
            Assert.IsTrue(Passport.ValidEyeColor("brn"));
            Assert.IsFalse(Passport.ValidEyeColor("wat"));
        }

        [Test]
        public void It_validates_passport_id()
        {
            Assert.IsTrue(Passport.ValidPassportID("000000001"));
            Assert.IsFalse(Passport.ValidPassportID("0123456789"));
        }

        [Test]
        public void It_does_strict_validation()
        {
            Assert.IsFalse(new Passport("eyr:1972 cid:100 hcl:#18171d ecl:amb hgt:170 pid:186cm iyr:2018 byr:1926").IsStrictValid());
            Assert.IsFalse(new Passport("iyr:2019 hcl:#602927 eyr:1967 hgt:170cm ecl:grn pid:012533040 byr:1946").IsStrictValid());
            Assert.IsFalse(new Passport("hcl:dab227 iyr:2012 ecl:brn hgt:182cm pid:021572410 eyr:2020 byr:1992 cid:277").IsStrictValid());
            Assert.IsFalse(new Passport("hgt:59cm ecl:zzz eyr:2038 hcl:74454a iyr:2023 pid:3556412378 byr:2007").IsStrictValid());
            Assert.IsTrue(new Passport("pid:087499704 hgt:74in ecl:grn iyr:2012 eyr:2030 byr:1980 hcl:#623a2f").IsStrictValid());
            Assert.IsTrue(new Passport("eyr:2029 ecl:blu cid:129 byr:1989 iyr:2014 pid:896056539 hcl:#a97842 hgt:165cm").IsStrictValid());
            Assert.IsTrue(new Passport("hcl:#888785 hgt:164cm byr:2001 iyr:2015 cid:88 pid:545766238 ecl:hzl eyr:2022").IsStrictValid());
            Assert.IsTrue(new Passport("iyr:2010 hgt:158cm hcl:#b6652a ecl:blu byr:1944 eyr:2021 pid:093154719").IsStrictValid());
        }
    }
}