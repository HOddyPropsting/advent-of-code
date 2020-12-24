using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2020
{
    class Day6
    {
        public static int Part1()
        {
            using (Stream stream = File.Open(@"day6", FileMode.Open))
            using (TextReader sr = new StreamReader(stream, Encoding.UTF8))
            {
                string line;
                StringBuilder group = new StringBuilder();
                int sum = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Length == 0)
                    {
                        sum += group.ToString().Distinct().Count();
                        group.Clear();
                    }
                    else
                    {
                        group.Append(line);
                    }
                }
                sum += group.ToString().Distinct().Count();
                return sum;
            }
        }

        public static int Part2()
        {
            using (Stream stream = File.Open(@"day6", FileMode.Open))
            using (TextReader sr = new StreamReader(stream, Encoding.UTF8))
            {
                string line;
                Dictionary<char, int> answers = new Dictionary<char, int>();
                int numberInGroup = 0;
                int sum = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Length == 0)
                    {
                        foreach (var (key, value) in answers)
                        {
                            if (value == numberInGroup) sum++;
                        }
                        numberInGroup = 0;
                        answers.Clear();
                    }
                    else
                    {
                        numberInGroup++;
                        foreach (char c in line)
                        {
                            if (answers.ContainsKey(c))
                            {
                                answers[c] = answers[c] + 1;
                            }
                            else
                            {
                                answers.Add(c, 1);
                            }
                        }
                    }
                }
                foreach (var (key, value) in answers)
                {
                    if (value == numberInGroup) sum++;
                }
                numberInGroup = 0;
                answers.Clear();
                return sum;
            }
        }
    }
}
