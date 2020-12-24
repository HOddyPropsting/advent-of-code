using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2020
{
    class Day9
    {
        public static bool SumInList(IEnumerable<int> list, int value)
        {
            List<int> otherHalf = list.Select(x => value - x).ToList();
            foreach (var val in list)
            {
                if (otherHalf.Contains(val)) return true;
            }
            return false;
        }

        public static bool SumInListLinq(IEnumerable<int> list, int value)
        {
            return list.Select(x => value - x).Intersect(list).Count() > 0;
        }


        public static int Part1()
        {
            Queue<int> buffer = new Queue<int>(25);
            using (Stream stream = File.Open(@"day9", FileMode.Open))
            using (TextReader sr = new StreamReader(stream, Encoding.UTF8))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    int val = Int32.Parse(line);
                    if (buffer.Count == 25)
                    {

                        var result = SumInList(buffer, val);
                        if (!result) return val;
                        buffer.Dequeue();
                        buffer.Enqueue(val);
                    }
                    else
                    {
                        buffer.Enqueue(val);
                    }
                }
            }
            return 0;
        }

        public static Int64 Part2()
        {
            Int64 searchValue = Part1();
            Queue<Int64> buffer = new Queue<Int64>(25);
            using (Stream stream = File.Open(@"day9", FileMode.Open))
            using (TextReader sr = new StreamReader(stream, Encoding.UTF8))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var val = Int64.Parse(line);
                    buffer.Enqueue(val);
                    var sum = buffer.Sum();
                    if (sum == searchValue)
                    {
                        return buffer.Min() + buffer.Max();
                    }
                    else if (sum > searchValue)
                    {
                        while (buffer.Sum() > searchValue)
                        {
                            buffer.Dequeue();
                        }
                    }

                }
            }
            return 0;
        }
    }
}
