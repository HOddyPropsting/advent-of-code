using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode2020
{
    public class Day1
    {
        public static int Part1()
        {
            List<int> input = new List<int>();
            using (Stream stream = File.Open(@"day1", FileMode.Open))
            using (TextReader sr = new StreamReader(stream, Encoding.UTF8))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    input.Add(Convert.ToInt32(line));
                }
            }

            HashSet<int> valuesToFind = new HashSet<int>();
            for (int i = 0; i < input.Count; i++)
            {
                valuesToFind.Add(2020 - input[i]);
            }
            foreach (var i in input)
            {
                if (valuesToFind.Contains(i))
                {
                    return i * (2020 - i);
                }
            }
            return 0;
        }

        public static int Part2()
        {
            List<int> input = new List<int>();
            using (Stream stream = File.Open(@"day1", FileMode.Open))
            using (TextReader sr = new StreamReader(stream, Encoding.UTF8))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    input.Add(Convert.ToInt32(line));
                }
            }

            for (int i = 0; i < input.Count; i++)
            {
                for (int j = i + 1; j < input.Count; j++)
                {
                    for (int k = j + 1; k < input.Count; k++)
                    {
                        if (input[i] + input[j] + input[k] == 2020)
                        {
                            return input[i] * input[j] * input[k];
                        }
                    }
                }
            }
            return 0;
        }
    }
}