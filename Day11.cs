using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode2020
{
    class Day11
    {
        public static char applyKernel(char[,] map, int x, int y)
        {
            int count = 0;
            if (map[x, y] == '.') return '.';

            for (int i = x - 1; i <= x + 1; i++)
            {
                if (i < 0 || i >= map.GetLength(0)) continue;
                for (int j = y - 1; j <= y + 1; j++)
                {
                    if (j < 0 || j >= map.GetLength(1)) continue;
                    if (map[i, j] == '#') count++;
                }
            }
            if (map[x, y] == '#')
            {
                return count >= 5 ? 'L' : '#';
            }
            else
            {
                return count == 0 ? '#' : 'L';
            }
        }

        public static char applyLoS(char[,] map, int x, int y)
        {
            int count = 0;
            if (map[x, y] == '.') return '.';
            for (var (i, j) = (x - 1, y - 1); i >= 0 && j >= 0; i--, j--)// - -
            {
                if (map[i, j] == 'L') break;
                if (map[i, j] == '#')
                {
                    count++; break;
                }
            }
            for (var (i, j) = (x, y - 1); j >= 0; j--)// 0 -
            {
                if (map[i, j] == 'L') break;
                if (map[i, j] == '#')
                {
                    count++; break;
                }
            }
            for (var (i, j) = (x + 1, y - 1); i < map.GetLength(0) && j >= 0; i++, j--)// + -
            {
                if (map[i, j] == 'L') break;
                if (map[i, j] == '#')
                {
                    count++; break;
                }
            }
            for (var (i, j) = (x + 1, y); i < map.GetLength(0); i++) //+ 0
            {
                if (map[i, j] == 'L') break;
                if (map[i, j] == '#')
                {
                    count++; break;
                }
            }
            for (var (i, j) = (x + 1, y + 1); i < map.GetLength(0) && j < map.GetLength(1); i++, j++)// + +
            {
                if (map[i, j] == 'L') break;
                if (map[i, j] == '#')
                {
                    count++; break;
                }
            }
            for (var (i, j) = (x, y + 1); j < map.GetLength(1); j++) //0 +
            {
                if (map[i, j] == 'L') break;
                if (map[i, j] == '#')
                {
                    count++; break;
                }
            }
            for (var (i, j) = (x - 1, y + 1); i >= 0 && j < map.GetLength(1); i--, j++)
            {
                if (map[i, j] == 'L') break;
                if (map[i, j] == '#')
                {
                    count++; break;
                }
            }
            for (var (i, j) = (x - 1, y); i >= 0; i--)
            {
                if (map[i, j] == 'L') break;
                if (map[i, j] == '#')
                {
                    count++; break;
                }
            }
            if (map[x, y] == '#')
            {
                return count >= 5 ? 'L' : '#';
            }
            else
            {
                return count == 0 ? '#' : 'L';
            }
        }

        public static int Part1()
        {
            List<int> adapters = new List<int>();
            adapters.Add(0); //socket is 0
            var width = 0;
            var height = 1;
            using (Stream stream = File.Open(@"day11", FileMode.Open))
            using (TextReader sr = new StreamReader(stream, Encoding.UTF8))
            {
                string line;
                line = sr.ReadLine();
                width = line.Length;
                while (sr.ReadLine() != null)
                {
                    height++;
                }
            }
            char[,] map = new char[width, height];
            char[,] backup = new char[width, height];
            using (Stream stream = File.Open(@"day11", FileMode.Open))
            using (TextReader sr = new StreamReader(stream, Encoding.UTF8))
            {
                string line;
                int i = 0;
                int j = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    foreach (var c in line)
                    {
                        map[i, j] = c;
                        i++;
                    }
                    i = 0;
                    j++;
                }
            }

            bool changed = true;
            while (changed)
            {
                changed = false;
                for (int i = 0; i < map.GetLength(0); i++)
                {
                    for (int j = 0; j < map.GetLength(1); j++)
                    {
                        backup[i, j] = applyKernel(map, i, j);
                    }
                }

                for (int i = 0; i < map.GetLength(0); i++)
                {
                    for (int j = 0; j < map.GetLength(1); j++)
                    {
                        if (map[i, j] != backup[i, j]) changed = true;
                        map[i, j] = backup[i, j];
                    }
                }
            }
            int counter = 0;
            foreach (var c in map)
            {
                if (c == '#') counter++;
            }
            return counter;
        }

        public static int Part2()
        {
            List<int> adapters = new List<int>();
            adapters.Add(0); //socket is 0
            var width = 0;
            var height = 1;
            using (Stream stream = File.Open(@"day11", FileMode.Open))
            using (TextReader sr = new StreamReader(stream, Encoding.UTF8))
            {
                string line;
                line = sr.ReadLine();
                width = line.Length;
                while (sr.ReadLine() != null)
                {
                    height++;
                }
            }
            char[,] map = new char[width, height];
            char[,] backup = new char[width, height];
            using (Stream stream = File.Open(@"day11", FileMode.Open))
            using (TextReader sr = new StreamReader(stream, Encoding.UTF8))
            {
                string line;
                int i = 0;
                int j = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    foreach (var c in line)
                    {
                        map[i, j] = c;
                        i++;
                    }
                    i = 0;
                    j++;
                }
            }

            bool changed = true;
            while (changed)
            {
                changed = false;
                for (int i = 0; i < map.GetLength(0); i++)
                {
                    for (int j = 0; j < map.GetLength(1); j++)
                    {
                        backup[i, j] = applyLoS(map, i, j);
                    }
                }

                for (int i = 0; i < map.GetLength(0); i++)
                {
                    for (int j = 0; j < map.GetLength(1); j++)
                    {
                        if (map[i, j] != backup[i, j]) changed = true;
                        map[i, j] = backup[i, j];
                    }
                }
            }
            int counter = 0;
            foreach (var c in map)
            {
                if (c == '#') counter++;
            }
            return counter;
        }
    }
}
