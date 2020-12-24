using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode2020
{
    class Day5
    {
        public static int Part1()
        {
            using (Stream stream = File.Open(@"day5", FileMode.Open))
            using (TextReader sr = new StreamReader(stream, Encoding.UTF8))
            {
                string line;
                int maxID = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    int row = 0;
                    int column = 0;
                    for (int i = 0; i < 7; i++)
                    {
                        if (line[i] == 'B') row |= 1 << 6 - i;
                    }
                    for (int i = 0; i < 3; i++)
                    {
                        if (line[i + 7] == 'R') column |= 1 << 2 - i;
                    }
                    maxID = Math.Max(maxID, row * 8 + column);
                }
                return maxID;
            }
        }

        public static int Part2()
        {
            using (Stream stream = File.Open(@"day5", FileMode.Open))
            using (TextReader sr = new StreamReader(stream, Encoding.UTF8))
            {
                string line;
                List<int> seatNumbers = new List<int>();
                int maxID = 0;
                int minID = Int32.MaxValue;
                while ((line = sr.ReadLine()) != null)
                {
                    int row = 0;
                    int column = 0;
                    for (int i = 0; i < 7; i++)
                    {
                        if (line[i] == 'B') row |= 1 << 6 - i;
                    }
                    for (int i = 0; i < 3; i++)
                    {
                        if (line[i + 7] == 'R') column |= 1 << 2 - i;
                    }
                    seatNumbers.Add(row * 8 + column);
                    maxID = Math.Max(maxID, row * 8 + column);
                    minID = Math.Min(minID, row * 8 + column);
                }
                var sum = ((maxID - minID) * (minID + maxID + 1)) / 2 + minID;
                foreach (var seat in seatNumbers)
                {
                    sum -= seat;
                }
                return sum;
            }
        }
    }
}
