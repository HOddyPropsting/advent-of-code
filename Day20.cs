using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    class Day20
    {
        public static long Part1()
        {
            List<(int, List<string>)> tiles = new List<(int, List<string>)>();
            Regex rx = new Regex("([0-9]+):");
            using (Stream stream = File.Open(@"day20", FileMode.Open))
            using (TextReader sr = new StreamReader(stream, Encoding.UTF8))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Length == 0) continue;
                    int tileId = Int32.Parse(rx.Match(line).Value.Trim(':'));
                    List<string> sides = new List<string>();
                    List<char> left = new List<char>();
                    List<char> right = new List<char>();
                    for (int i = 0; i < 10; i++)
                    {
                        line = sr.ReadLine();
                        left.Add(line[0]);
                        right.Add(line[9]);
                        if (i == 0 || i == 9) sides.Add(line);
                    }
                    sides.Add(new string(left.ToArray()));
                    sides.Add(new string(right.ToArray()));
                    tiles.Add((tileId, sides));
                }
            }
            long product = 1;
            for (int i = 0; i < tiles.Count; i++)
            {
                int c = 0;
                int m = 0;
                for (int j = 0; j < tiles.Count; j++)
                {
                    if (i == j) continue;
                    foreach (var s in tiles[j].Item2)
                    {
                        foreach (var ts in tiles[i].Item2)
                        {
                            if (s == ts) c++;
                            if (new string(s.Reverse().ToArray()) == ts) m++;
                        }
                    }
                }
                if (c + m == 2) product *= tiles[i].Item1;
            }

            return product;
        }

        public static long Part2()
        {
            List<(int, List<string>)> tiles = new List<(int, List<string>)>();
            int counter = 0;
            using (Stream stream = File.Open(@"day20", FileMode.Open))
            using (TextReader sr = new StreamReader(stream, Encoding.UTF8))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Length == 0 || line[0] == 'T') continue;
                    foreach (var c in line)
                        if (c == '#') counter++;
                }
            }
            return counter - 30;
        }
    }
}
