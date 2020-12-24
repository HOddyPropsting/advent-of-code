using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;

namespace AdventOfCode2020
{
    class Day10
    {
        public static int Part1()
        {
            List<int> adapters = new List<int>();
            adapters.Add(0); //socket is 0
            using (Stream stream = File.Open(@"day10", FileMode.Open))
            using (TextReader sr = new StreamReader(stream, Encoding.UTF8))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    adapters.Add(Int32.Parse(line));
                }
            }

            adapters.Sort();
            Dictionary<int, int> differences = new Dictionary<int, int>();
            differences.Add(1, 0);
            differences.Add(2, 0);
            differences.Add(3, 1); // built in is always 3 higher
            for (int i = 1; i < adapters.Count; i++)
            {
                differences[adapters[i] - adapters[i - 1]]++;
            }

            return differences[1] * differences[3];
        }

        public static BigInteger Part2()
        {
            List<int> adapters = new List<int>();
            adapters.Add(0); //socket is 0
            using (Stream stream = File.Open(@"day10", FileMode.Open))
            using (TextReader sr = new StreamReader(stream, Encoding.UTF8))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    adapters.Add(Int32.Parse(line));
                }
            }

            adapters.Sort();
            adapters.Add(adapters.Last() + 3); //device is 3 greater than the greatest adapter
            List<int> differences = new List<int>();
            for (int i = 1; i < adapters.Count; i++)
            {
                differences.Add(adapters[i] - adapters[i - 1]);
            }
            List<int> streaks = new List<int>();
            int streakCount = 0;
            foreach (var diff in differences)
            {
                if (diff == 3)
                {
                    if (streakCount > 1)
                    {
                        streaks.Add(streakCount);
                    }
                    streakCount = 0;
                }
                else
                {
                    streakCount++;
                }
            }
            BigInteger variants = 1;
            foreach (var streak in streaks)
            {
                switch (streak)
                {
                    case 2:
                        variants *= 2; //2 ways to re-write a 1 1 streak
                        break;
                    case 3:
                        variants *= 4; // 4 ways to re-write a 1 1 1 streak
                        break;
                    case 4:
                        variants *= 7; // 7 ways to re-write a 1 1 1 1 streak?
                        break;
                    default:
                        throw new Exception("error");
                }
            }
            return variants;
        }
    }
}
