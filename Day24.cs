using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2020
{
    class Day24
    {

        enum HexDir
        {
            E,
            SE,
            SW,
            W,
            NW,
            NE
        }

        static HexDir stringToDir(string s)
        {
            switch (s)
            {
                case "e": return HexDir.E;
                case "se": return HexDir.SE;
                case "sw": return HexDir.SW;
                case "w": return HexDir.W;
                case "nw": return HexDir.NW;
                case "ne": return HexDir.NE;
                default:
                    return HexDir.E;
            }
        }

        // laid out in cube coordinates: https://www.redblobgames.com/grids/hexagons/ hexes are laid out pointy side up.
        [DebuggerDisplay("{x}, {y}, {z}")]
        struct HexCoord
        {
            int x;
            int y;
            int z;
            public HexCoord(int x, int y, int z)
            {
                this.x = x;
                this.y = y;
                this.z = z;
            }

            public HexCoord Adjacent(HexDir dir)
            {
                switch (dir)
                {
                    case HexDir.W:  return new HexCoord(this.x - 1, this.y + 1, this.z);
                    case HexDir.E:  return new HexCoord(this.x + 1, this.y - 1, this.z);

                    case HexDir.SE: return new HexCoord(this.x, this.y - 1, this.z + 1);
                    case HexDir.NW: return new HexCoord(this.x, this.y + 1, this.z - 1);

                    case HexDir.SW: return new HexCoord(this.x - 1, this.y, this.z + 1);
                    case HexDir.NE: return new HexCoord(this.x + 1, this.y, this.z - 1); 
                }
                return this;
            }

            //    x+y+z      y          z        x
            //0000 0000 |0000 0000 |0000 0000| 0000 0000
            /*public override int GetHashCode()
            {
                unchecked
                {
                    return (Int32)(((byte)(Math.Abs(x) + Math.Abs(y) + Math.Abs(z))) << 24 | ((byte)y) << 16 | ((byte)z) << 8 | (byte)x );
                }
            }*/
        }

        static HexCoord FollowDirections(List<HexDir> directions, HexCoord origin = default(HexCoord))
        {
            HexCoord next = origin;
            foreach(var dir in directions)
            {
                next = next.Adjacent(dir);
            }
            return next;
        }

        static List<HexCoord> getNeighbours(HexCoord h)
        {
            List<HexCoord> l = new List<HexCoord>();
            l.Add(h.Adjacent(HexDir.E));
            l.Add(h.Adjacent(HexDir.W));
            l.Add(h.Adjacent(HexDir.NE));
            l.Add(h.Adjacent(HexDir.SW));
            l.Add(h.Adjacent(HexDir.NW));
            l.Add(h.Adjacent(HexDir.SE));
            return l;
        }

        public static int Part1()
        {
            //true = black, false = white
            Dictionary<HexCoord, bool> map = new Dictionary<HexCoord, bool>();
            using (Stream stream = File.Open(@"day24", FileMode.Open))
            using (TextReader sr = new StreamReader(stream, Encoding.UTF8))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    List<HexDir> directions = new List<HexDir>();
                    int idx = 0;
                    while (idx < line.Length)
                    {
                        int len = 0;
                        switch (line[idx])
                        {
                            case 's':
                            case 'n': len = 2; break;
                            case 'e':
                            case 'w': len = 1; break;
                        }
                        directions.Add(stringToDir(line.Substring(idx, len)));
                        idx += len;
                    }
                    HexCoord h = FollowDirections(directions);
                    if (map.ContainsKey(h))
                        map[h] = !map[h];
                    else map.Add(h, true);
                }
            }
            int counter = 0;
            foreach (var (key, value) in map)
            {
                if (value) counter++;
            }
            return counter;
        }

        public static int Part2()
        {
            //true = black, false = white
            Dictionary<HexCoord, bool> map = new Dictionary<HexCoord, bool>();
            using (Stream stream = File.Open(@"day24", FileMode.Open))
            using (TextReader sr = new StreamReader(stream, Encoding.UTF8))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    List<HexDir> directions = new List<HexDir>();
                    int idx = 0;
                    while (idx < line.Length)
                    {
                        int len = 0;
                        switch (line[idx])
                        {
                            case 's':
                            case 'n': len = 2; break;
                            case 'e':
                            case 'w': len = 1; break;
                        }
                        directions.Add(stringToDir(line.Substring(idx, len)));
                        idx += len;
                    }
                    HexCoord h = FollowDirections(directions);
                    if (map.ContainsKey(h))
                        map[h] = !map[h];
                    else map.Add(h, true);
                }
            }

            foreach(var (h,a) in map.ToList())
            {
                if (!a) map.Remove(h);
            }
            Console.WriteLine(map.Count);

            for (int _ = 0; _ < 100; _++)
            {
                Dictionary<HexCoord, int> neighboursCount = new Dictionary<HexCoord, int>();
                foreach (var (c, __) in map)
                {
                    var neighbours = getNeighbours(c);
                    foreach (var n in neighbours)
                    {
                        if (neighboursCount.ContainsKey(n))
                        {
                            neighboursCount[n] += 1;
                        }
                        else
                        {
                            neighboursCount.Add(n, 1);
                        }
                    }
                }
                Dictionary<HexCoord, bool> copy = new Dictionary<HexCoord, bool>();
                foreach (var (c, i) in neighboursCount)
                {
                    if (map.ContainsKey(c) && (i == 1 || i == 2))
                    {
                        copy.Add(c, true);
                    }
                    else if (!map.ContainsKey(c) && i == 2)
                    {
                        copy.Add(c, true);
                    }
                }
                map = copy;
                Console.WriteLine(map.Count);
            }
            return map.Count;
        }



    }
}
