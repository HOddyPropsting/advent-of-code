using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2020
{
    class Day22
    {
        public static int Part1()
        {
            Queue<int> p1 = new Queue<int>();
            Queue<int> p2 = new Queue<int>();
            using (Stream stream = File.Open(@"day22", FileMode.Open))
            using (TextReader sr = new StreamReader(stream, Encoding.UTF8))
            {
                string line;
                while ((line = sr.ReadLine()) != "")
                {
                    if (line.Contains(":")) continue;
                    p1.Enqueue(Int32.Parse(line));
                }
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Contains(":")) continue;
                    p2.Enqueue(Int32.Parse(line));
                }
            }

            while(p1.Count != 0 && p2.Count != 0)
            {
                var c1 = p1.Dequeue();
                var c2 = p2.Dequeue();
                if(c1 < c2)
                {
                    p2.Enqueue(c2);
                    p2.Enqueue(c1);
                } else
                {
                    p1.Enqueue(c1);
                    p1.Enqueue(c2);
                }
            }

            if(p1.Count != 0)
            {
                return p1.ToList().Reverse<int>().Select((x, index) => x * (index + 1)).Sum();
            } else
            {
                return p2.ToList().Reverse<int>().Select((x, index) => x * (index + 1)).Sum();
            }
        }

        public static int Part2()
        {
            Queue<int> p1 = new Queue<int>();
            Queue<int> p2 = new Queue<int>();
            using (Stream stream = File.Open(@"day22", FileMode.Open))
            using (TextReader sr = new StreamReader(stream, Encoding.UTF8))
            {
                string line;
                while ((line = sr.ReadLine()) != "")
                {
                    if (line.Contains(":")) continue;
                    p1.Enqueue(Int32.Parse(line));
                }
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Contains(":")) continue;
                    p2.Enqueue(Int32.Parse(line));
                }
            }

            var p1Win = game(p1, p2);

            if (p2.Count == 0)
            {
                return p1.ToList().Reverse<int>().Select((x, index) => x * (index + 1)).Sum();
            }
            else
            {
                return p2.ToList().Reverse<int>().Select((x, index) => x * (index + 1)).Sum();
            }
        }

        public static int GetSequenceHashCode(List<int> sequence) //hot straight off stack overflow
        {
            const int seed = 487;
            const int modifier = 31;

            unchecked
            {
                return sequence.Aggregate(seed, (current, item) =>
                    (current * modifier) + item.GetHashCode());
            }
        }

        public static bool game(Queue<int> p1, Queue<int> p2)
        {
            HashSet<string> played = new HashSet<string>();
            int c1;
            int c2;
            while ((p1.Count != 0 && p2.Count != 0))
            {
                bool p1Win = true;
                StringBuilder sb = new StringBuilder();
                foreach(var p in p1)
                {
                    sb.Append(p);
                }
                sb.Append("|");
                foreach (var p in p2)
                {
                    sb.Append(p);
                }
                var current = sb.ToString();
                c1 = p1.Dequeue();
                c2 = p2.Dequeue();
                if (played.Contains(current))
                { 
                    goto end;
                }
                played.Add(current);
                if(p1.Count >= c1 && p2.Count >= c2)
                {
                    p1Win = game(new Queue<int>(p1.ToList().Take(c1).ToList()), new Queue<int>(p2.ToList().Take(c2)));
                } else
                {
                    p1Win = c1 > c2;
                }
                end:
                if (p1Win)
                {
                    p1.Enqueue(c1);
                    p1.Enqueue(c2);
                }
                else
                {
                    p2.Enqueue(c2);
                    p2.Enqueue(c1);
                }
            }
            if (p1.Count == 0) return false;
            return true;
        }
    }
}
