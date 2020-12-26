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
                tile = new char[l][];
                for (int i = 0; i < l; i++)
                    tile[i] = new char[l];
            }

            public string getSide(int side) // 0 = N, 1 = E, 2 = S, 3 = W
            {
                char[] lr = new char[tile.GetLength(0)];
                switch (side)
                {
                    case 0:
                        return new string(tile[0]);
                        break;
                    case 1:
                        for (int i = 0; i < tile.GetLength(0); i++)
                            lr[i] = tile[i][tile.GetLength(0) - 1];
                        break;
                    case 2:
                        return new string(tile[tile.Length]);
                        break;
                    case 3:
                        for (int i = 0; i < tile.GetLength(0); i++)
                            lr[i] = tile[i][0];
                        break;
                }
                return new string(lr);
            }

            public List<string> sides() // this list is clockwise from top!
            {
                List<string> side = new List<string>();
                char[] left = new char[tile.GetLength(0)];
                char[] right = new char[tile.GetLength(0)];
                for (int i = 0; i < tile.GetLength(0); i++)
                {
                    left[i] = tile[i][0];
                    right[i] = tile[i][tile.GetLength(0)-1];
                }
                side.Add(new string(tile[0]));
                side.Add(new string(right));
                side.Add(new string(tile[tile.GetLength(0)-1]));
                side.Add(new string(left));
                return side;
            }

            public List<string> clockwiseSides()
            {
                List<String> sides = this.sides();
                sides[2] = new string(sides[2].Reverse().ToArray());
                sides[3] = new string(sides[3].Reverse().ToArray());
                return sides;
            }

            public List<string> anticlockwiseSides()
            {
                List<String> sides = this.sides();
                sides[0] = new string(sides[0].Reverse().ToArray());
                sides[1] = new string(sides[1].Reverse().ToArray());
                return sides;
            }

            public List<string> flipped_sides()
            {
                List<string> sides = this.sides();
                for(int i = 0; i < 4; i++)
                {
                    sides[i] = new string(sides[i].Reverse().ToArray());
                }
                return sides;
            }

            public List<string> all_sides()
            {
                var x = this.sides();
                x.AddRange(this.flipped_sides());
                return x;
            }

            public void flip()
            {
                char[][] t = new char[tile.Length][];
                for(int i = 0; i < tile.GetLength(0); i++)
                {
                    t[i] = new char[tile[0].Length];
                    for(int j =0; j < tile.GetLength(0); j++)
                    {
                        t[i][j] = tile[j][i];
                    }
                }
                tile = t;
            }

            public void rot_clockwise()
            {
                char[][] t = new char[tile.GetLength(0)][];
                for (int i = 0; i < tile.GetLength(0); i++)
                {
                    t[i] = new char[tile.GetLength(0)];
                    for (int j = 0; j < tile.GetLength(0); j++)
                    {
                        t[i][j] = tile[tile.GetLength(0) - j - 1][i];
                    }
                }
                tile = t;
            }

            public void rot_to(int startSide, int endSide)
            {
                while (startSide % 4 != endSide)
                {
                    rot_clockwise();
                    startSide++;
                }
            }

            public (int, int, int, bool) findTile(List<Tile> tiles) // (index, side, matched_side, to_flip) if the matched tile needs to be flipped, the bool will be true. 
            {
                for(int i = 0; i < tiles.Count; i++)
                    foreach (var (tSide, index) in this.clockwiseSides().Select((s, index) => (s, index)))
                    {
                        foreach (var (mSide, mIndex) in tiles[i].clockwiseSides().Select((s, index) => (s, 3-index)))
                            if (tSide == mSide) return (i, index, mIndex, true);
                        foreach (var (mSide, mIndex) in tiles[i].anticlockwiseSides().Select((s, index) => (s, index)))
                            if (tSide == mSide) return (i, index, mIndex, false);
                    }
                return (-1, -1, -1, false);
            }

            public override String ToString()
            {
                StringBuilder s = new StringBuilder();
                for (int i = 0; i < tile.Length; i++)
                    s.Append(tile[i]).AppendLine();
                return s.ToString();
            }
        }

        //static SortTiles(List<Tile> tiles, )


        public static long Part2()
        {
            List<Tile> tiles = new List<Tile>();
            Regex rx = new Regex("([0-9]+):");
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

            Tile[,] map = new Tile[12, 12];
            foreach (var (t,idx) in tiles.Select((tile,index) => (tile,index)))
            {
                int matched_sides = 0;
                foreach (Tile m in tiles)
                {
                    if (t.id == m.id) continue;
                    foreach (var tSide in t.sides())
                    {
                       foreach(var mSide in m.all_sides())
                        {
                            if (tSide == mSide) matched_sides++;
                        }
                    }
                }
                if(matched_sides == 2)
                {
                    map[0, 0] = t;
                    tiles.RemoveAt(idx);
                    goto CORNER_FOUND;
                }
            }
        CORNER_FOUND:
            { // getting the initial tile into the right rotation
                var (aIndex, aSide, aMatchedSide, aToFlip) = map[0, 0].findTile(tiles);
                var a = tiles[aIndex];
                tiles.RemoveAt(aIndex);
                var (bIndex, bSide, bMatchedSide, bToFlip) = map[0, 0].findTile(tiles);
                var b = tiles[bIndex];
                tiles.RemoveAt(bIndex);
                if (aToFlip) a.flip();
                if (bToFlip) b.flip();
                if (((aSide + 1) % 4) == bSide) // if a should go on [0,1]
                {
                    map[0, 0].rot_to(aSide, 1);
                    a.rot_to(aMatchedSide, 3);
                    map[0, 1] = a;
                    b.rot_to(bMatchedSide, 0);
                    map[1, 0] = b;
                }
                else
                {
                    map[0, 0].rot_to(bSide, 1);
                    b.rot_to(bMatchedSide, 3);
                    map[0, 1] = b;
                    a.rot_to(aMatchedSide, 0);
                    map[1, 0] = a;
                }



                Queue<(int, int)> openTiles = new Queue<(int, int)>();
                openTiles.Enqueue((1, 0));
                openTiles.Enqueue((0, 1));
                while (openTiles.Count > 0)
                {
                    var (tx, ty) = openTiles.Dequeue();
                    var (index, side, matchedSide, toFlip) = map[tx, ty].findTile(tiles);
                    while (index != -1)
                    {
                        var temp = tiles[index];
                        tiles.RemoveAt(index);
                        if (toFlip) temp.flip();
                        temp.rot_to(matchedSide, (side + 2) % 4);
                        var (nx, ny) = (-1, -1);
                        switch (side)
                        {
                            case 3: (nx, ny) = (tx, ty - 1); break;
                            case 1: (nx, ny) = (tx, ty + 1); break;
                            case 0: (nx, ny) = (tx - 1, ty); break;
                            case 2: (nx, ny) = (tx + 1, ty); break;
                        }
                        map[nx, ny] = temp;
                        openTiles.Enqueue((nx, ny));
                        (index, side, matchedSide, toFlip) = map[tx, ty].findTile(tiles);
                    }
                }
            }

            Tile fullMap = new Tile(0, 12*8);
            for(int i = 0; i < 12; i++)
                for(int j = 0; j < 12; j++)
                    for (int w = 1; w < 9; w++)
                        for (int z = 1; z < 9; z++)
                            fullMap.tile[i*8 + w-1][j*8 + z-1] = map[i,j].tile[w][z];

            List<(int, int)> kernel = new List<(int, int)> { (0,18),
                (1,0), (1,5), (1,6), (1,11), (1,12), (1,17), (1,18), (1,19),
                (2,1), (2,4), (2,7), (2,10), (2,13), (2,16)};

            fullMap.flip();
            fullMap.rot_clockwise();
            fullMap.rot_clockwise();
            int counter = 0;
            for(int i = 0; i < (12*8) - 20; i++)
            {
                for (int j = 0; j < (12 * 8) - 3; j++)
                {
                    bool found = true;
                    foreach (var (y, x) in kernel)
                    {
                        if (fullMap.tile[j + y][i + x] != '#')
                        {
                            found = false;
                            break;
                        }
                    }
                    if (found)
                    {
                        foreach (var (y, x) in kernel)
                        {
                            fullMap.tile[j + y][i + x] = 'O';
                        }
                    }
                }
            }


            for (int i = 0; i < (12 * 8); i++)
                for (int j = 0; j < (12 * 8); j++)
                    if (fullMap.tile[i][j] == '#') counter++;
            return counter;
        }
    }
}
