using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode2020
{
    class Day20
    {
        public static Part1()
        {
            List<char[,]> tiles = new List<char[,]>();
            using (Stream stream = File.Open(@"day20", FileMode.Open))
            using (TextReader sr = new StreamReader(stream, Encoding.UTF8))
            {
                string line;
                while ((line = sr.ReadLine()) != "")
                {}
            }
        }
    }
}
