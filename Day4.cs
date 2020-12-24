using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    class Day4
    {
        public static int Part1()
        {
            int result = 0;
            using (Stream stream = File.Open(@"day4", FileMode.Open))
            using (TextReader sr = new StreamReader(stream, Encoding.UTF8))
            {
                string line;
                int numberOfFields = 0;
                int cidPresent = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Length == 0)
                    {
                        if (numberOfFields - cidPresent == 7) result++;
                        numberOfFields = 0;
                        cidPresent = 0;
                    }
                    else
                    {
                        foreach (var field in line.Split(@" "))
                        {
                            if (field.StartsWith("cid")) cidPresent++;
                            numberOfFields++;
                        }
                    }
                }
                if (numberOfFields - cidPresent == 7) result++;
            }
            return result;
        }
        public static int Part2()
        {
            int result = 0;
            using (Stream stream = File.Open(@"day4", FileMode.Open))
            using (TextReader sr = new StreamReader(stream, Encoding.UTF8))
            {
                string line;
                int numberOfFields = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Length == 0)
                    {
                        if (numberOfFields == 7) result++;
                        numberOfFields = 0;
                    }
                    else
                    {
                        foreach (var field in line.Split(@" "))
                        {
                            /*
                             * 
    byr (Birth Year) - four digits; at least 1920 and at most 2002.
    iyr (Issue Year) - four digits; at least 2010 and at most 2020.
    eyr (Expiration Year) - four digits; at least 2020 and at most 2030.
    hgt (Height) - a number followed by either cm or in:
        If cm, the number must be at least 150 and at most 193.
        If in, the number must be at least 59 and at most 76.
    hcl (Hair Color) - a # followed by exactly six characters 0-9 or a-f.
    ecl (Eye Color) - exactly one of: amb blu brn gry grn hzl oth.
    pid (Passport ID) - a nine-digit number, including leading zeroes.
    cid (Country ID) - ignored, missing or not.

                             */
                            if (field.StartsWith("cid"))
                            {
                                //cidPresent++;
                                //numberOfFields++;
                            }
                            else if (field.StartsWith("byr"))
                            {
                                var year = field.Split(":")[1];
                                if (year.Length == 4 && Int32.Parse(year) >= 1920 && Int32.Parse(year) <= 2002) numberOfFields++;
                            }
                            else if (field.StartsWith("iyr"))
                            {
                                var year = field.Split(":")[1];
                                if (year.Length == 4 && Int32.Parse(year) >= 2010 && Int32.Parse(year) <= 2020) numberOfFields++;
                            }
                            else if (field.StartsWith("eyr"))
                            {
                                var year = field.Split(":")[1];
                                if (year.Length == 4 && Int32.Parse(year) >= 2020 && Int32.Parse(year) <= 2030) numberOfFields++;
                            }
                            else if (field.StartsWith("hgt"))
                            {
                                var height = field.Split(":")[1];
                                Regex rx = new Regex(@"[0-9]*in|cm");
                                if (!rx.IsMatch(height)) continue;
                                var value = Int32.Parse(height.Substring(0, height.Length - 2));
                                var type = height.Substring(height.Length - 2);
                                if (type == "in" && value >= 59 && value <= 76)
                                {
                                    numberOfFields++;
                                }
                                else if (type == "cm" && value >= 150 && value <= 193)
                                {
                                    numberOfFields++;
                                }
                            }
                            else if (field.StartsWith("hcl"))
                            {
                                var value = field.Split(":")[1];
                                Regex rx = new Regex(@"#[0-9a-f]{6}$");
                                if (rx.IsMatch(value)) numberOfFields++;
                            }
                            else if (field.StartsWith("ecl"))
                            {
                                var value = field.Split(":")[1];
                                Regex rx = new Regex(@"amb|blu|brn|gry|grn|hzl|oth");
                                if (rx.IsMatch(value)) numberOfFields++;
                            }
                            else if (field.StartsWith("pid"))
                            {
                                var value = field.Split(":")[1];
                                Regex rx = new Regex(@"^[0-9]{9}$");
                                if (rx.IsMatch(value)) numberOfFields++;
                            }
                        }
                    }
                }
                if (numberOfFields == 7) result++;
            }
            return result;
        }
    }
}
