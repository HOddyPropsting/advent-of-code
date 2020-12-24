using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    class Day2Password
    {
        int min, max;
        string c;
        string password;

        public Day2Password(int min, int max, string c, string password)
        {
            this.min = min;
            this.max = max;
            this.c = c;
            this.password = password;
        }

        public bool isValid()
        {
            var number = password.Length - password.Replace(c, string.Empty).Length;
            return min <= number && number <= max;
        }

        public bool isValidPart2()
        {
            var minChar = password[min - 1];
            var maxChar = password[max - 1];
            if (minChar == c[0] && maxChar != c[0])
            {
                return true;
            }
            else if (minChar != c[0] && maxChar == c[0])
            {
                return true;
            }
            return false;
        }

        public Day2Password(string source)
        {
            Regex rx = new Regex(@"([0-9]*)-([0-9]*) ([a-z]): ([a-z]*)");
            MatchCollection matches = rx.Matches(source);
            min = Int32.Parse(matches[0].Groups[1].Value);
            max = Int32.Parse(matches[0].Groups[2].Value);
            c = matches[0].Groups[3].Value;
            password = matches[0].Groups[4].Value;
        }
    }

    public class Day2
    {
        public static int Part1()
        {
            int _numberValid = 0;
            using (Stream stream = File.Open(@"day2", FileMode.Open))
            using (TextReader sr = new StreamReader(stream, Encoding.UTF8))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var text = new Day2Password(line);
                    if (text.isValid()) _numberValid++;
                }
            }
            return _numberValid;
        }

        public static int Part2()
        {
            int _numberValid = 0;
            using (Stream stream = File.Open(@"day2", FileMode.Open))
            using (TextReader sr = new StreamReader(stream, Encoding.UTF8))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var text = new Day2Password(line);
                    if (text.isValidPart2()) _numberValid++;
                }
            }
            return _numberValid;
        }
    }


}
