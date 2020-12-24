using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode2020
{
    class Day3
    {
        class Day3Map
        {
            int width;
            int height;
            List<String> map;

            public Day3Map()
            {
                map = new List<String>();
                using (Stream stream = File.Open(@"day3", FileMode.Open))
                using (TextReader sr = new StreamReader(stream, Encoding.UTF8))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        map.Add(line);
                    }
                }

                width = map[0].Length;
                height = map.Count;
            }

            public int treesOnSlope(int across, int down)
            {
                int x = 0; int y = 0; int treeCount = 0;
                while (y < height)
                {
                    if (map[y][x] == '#') treeCount++;
                    y += down;
                    x = (x + across) % width;
                }
                return treeCount;
            }
        }

        public static int Part1(int across, int down)
        {
            var map = new Day3Map();
            return map.treesOnSlope(across, down);
        }

        public static System.UInt64 Part2()
        {
            var map = new Day3Map();
            return ((ulong)map.treesOnSlope(1, 1) * (ulong)map.treesOnSlope(3, 1) * (ulong)map.treesOnSlope(5, 1) * (ulong)map.treesOnSlope(7, 1) * (ulong)map.treesOnSlope(1, 2));
        }
    }
}
