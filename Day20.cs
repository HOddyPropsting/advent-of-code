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
                if (c + m == 2)
                {
                    Console.WriteLine(tiles[i].Item1);
                    product *= tiles[i].Item1;
                }
            }

            return product;
        }

        class Tile
        {
            public int id;
            public char[][] tile;

            public Tile(int id, int l)
            {
                this.id = id;
                tile = new char[l-1][];
                for (int i = 0; i < l; i++)
                    tile[i] = new char[l-1];
            }

            public List<char[]> sides()
            {
                List<char[]> side = new List<char[]>();
                side.Add(tile[0]);
                side.Add(tile[tile.GetLength(0)]);
                return side;
            }

            public char[][] flip()
            {
                char[][] t = new char[tile.GetLength(0)][];
                for(int i = 0; i < tile.GetLength(0); i++)
                {
                    t[i] = new char[tile.GetLength(1)];
                    for(int j =0; j < tile.GetLength(1); j++)
                    {
                        t[i][j] = tile[j][i];
                    }
                }
                return t;
            }

            public char[][] rot()
            {
                char[][] t = new char[tile.GetLength(0)][];
                for (int i = 0; i < tile.GetLength(0); i++)
                {
                    t[i] = new char[tile.GetLength(1)];
                    for (int j = 0; j < tile.GetLength(1); j++)
                    {
                        t[i][j] = tile[tile.GetLength(1) - j - 1][i];
                    }
                }
                return t;
            }
        }

        public static long Part2()
        {
            List<Tile> tiles = new List<Tile>();
            Regex rx = new Regex("([0-9]+):");
            int counter = 0;
            using (Stream stream = File.Open(@"day20", FileMode.Open))
            using (TextReader sr = new StreamReader(stream, Encoding.UTF8))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Length == 0) continue;
                    int tileId = Int32.Parse(rx.Match(line).Value.Trim(':'));
                    Tile t = new Tile(tileId, 10);
                    for (int i = 0; i < 10; i++)
                    {
                        line = sr.ReadLine();
                        t.tile[i] = line.ToArray();
                    }
                    tiles.Add(t);
                }
            }




            return counter - 30;
        }
    }
}
