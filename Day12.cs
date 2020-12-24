using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode2020
{
    class Day12
    {
        public static char DirectionFromAngle(int angle)
        {
            switch (angle)
            {
                case 90: return 'E';
                case 180: return 'S';
                case 270: return 'W';
                case 0: return 'N';
                default: throw new Exception("unknown angle");
            }
        }
        public static int Part1()
        {
            Dictionary<char, (int, int)> direction = new Dictionary<char, (int, int)>();
            direction.Add('N', (0, 1));
            direction.Add('S', (0, -1));
            direction.Add('E', (1, 0));
            direction.Add('W', (-1, 0));
            var (x, y, angle) = (0, 0, 90); //north is 0, rotating clockwise

            using (Stream stream = File.Open(@"day12", FileMode.Open))
            using (TextReader sr = new StreamReader(stream, Encoding.UTF8))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var (command, amount) = (line[0], Int32.Parse(line.Substring(1)));
                    switch (command)
                    {
                        case 'N':
                        case 'S':
                        case 'E':
                        case 'W':
                            var (xAmount, yAmount) = direction[command];
                            x += xAmount * amount;
                            y += yAmount * amount;
                            break;
                        case 'F':
                            (xAmount, yAmount) = direction[DirectionFromAngle(angle)];
                            x += xAmount * amount;
                            y += yAmount * amount;
                            break;
                        case 'B':
                            (xAmount, yAmount) = direction[DirectionFromAngle(angle)];
                            x -= xAmount * amount;
                            y -= yAmount * amount;
                            break;
                        case 'L':
                            angle = (angle - amount % 360 + 360) % 360;
                            break;
                        case 'R':
                            angle = (angle + amount % 360 + 360) % 360;
                            break;
                    }
                }
            }

            return Math.Abs(x) + Math.Abs(y);
        }

        public static int Part2()
        {
            Dictionary<char, (int, int)> direction = new Dictionary<char, (int, int)>();
            direction.Add('N', (0, 1));
            direction.Add('S', (0, -1));
            direction.Add('E', (1, 0));
            direction.Add('W', (-1, 0));
            var (x, y, wx, wy) = (0, 0, 10, 1);

            using (Stream stream = File.Open(@"day12", FileMode.Open))
            using (TextReader sr = new StreamReader(stream, Encoding.UTF8))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var (command, amount) = (line[0], Int32.Parse(line.Substring(1)));
                    switch (command)
                    {
                        case 'N':
                        case 'S':
                        case 'E':
                        case 'W':
                            var (xAmount, yAmount) = direction[command];
                            wx += xAmount * amount;
                            wy += yAmount * amount;
                            break;
                        case 'F':
                            (xAmount, yAmount) = (wx, wy);
                            x += xAmount * amount;
                            y += yAmount * amount;
                            break;
                        case 'B':
                            (xAmount, yAmount) = (-wx, -wy);
                            x -= xAmount * amount;
                            y -= yAmount * amount;
                            break;
                        case 'L':
                            switch (amount)
                            {
                                case 90: (wx, wy) = (-wy, wx); break;
                                case 180: (wx, wy) = (-wx, -wy); break;
                                case 270: (wx, wy) = (wy, -wx); break;
                            }
                            break;
                        case 'R':
                            switch (amount)
                            {
                                case 90: (wx, wy) = (wy, -wx); break;
                                case 180: (wx, wy) = (-wx, -wy); break;
                                case 270: (wx, wy) = (-wy, wx); break;
                            }
                            break;
                    }
                }
            }

            return Math.Abs(x) + Math.Abs(y);
        }
    }
}
