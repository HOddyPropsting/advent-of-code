using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    class Day14
    {
        public static UInt64 Part1()
        {
            Dictionary<UInt64, UInt64> memory = new Dictionary<UInt64, UInt64>();
            UInt64 ones_mask = 0;
            UInt64 zeros_mask = 0;
            Regex numbersRx = new Regex(@"\[([0-9]+)\]\s=\s([0-9]+)");
            using (Stream stream = File.Open(@"day14", FileMode.Open))
            using (TextReader sr = new StreamReader(stream, Encoding.UTF8))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line[1] == 'a') // mask line
                    {
                        ones_mask = 0;
                        zeros_mask = 0;
                        int counter = 35;
                        foreach (char c in line.Split(' ')[2])
                        {
                            if (c == '1')
                            {
                                ones_mask |= (UInt64)1 << counter;
                            }
                            else if (c == '0')
                            {
                                zeros_mask |= (UInt64)1 << counter;
                            }
                            counter--;
                        }
                        zeros_mask = ~zeros_mask;
                    }
                    else
                    {
                        var matches = numbersRx.Match(line);
                        var address = UInt64.Parse(matches.Groups[1].Value);
                        var number = UInt64.Parse(matches.Groups[2].Value);
                        number |= ones_mask;
                        number &= zeros_mask;
                        memory[address] = number;
                    }
                }
            }
            UInt64 total = 0;
            foreach (var (key, value) in memory)
            {
                total += value;
            }
            return total;
        }

        public static BigInteger Part2()
        {
            Dictionary<UInt64, UInt64> memory = new Dictionary<UInt64, UInt64>();
            UInt64 ones_mask = 0;
            UInt64 floatingMask = 0;
            List<UInt64> floatingMasks = new List<UInt64>();
            Regex numbersRx = new Regex(@"\[([0-9]+)\]\s=\s([0-9]+)");
            using (Stream stream = File.Open(@"day14", FileMode.Open))
            using (TextReader sr = new StreamReader(stream, Encoding.UTF8))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line[1] == 'a') // mask line
                    {
                        ones_mask = 0;
                        floatingMask = 0;
                        floatingMasks.Clear();
                        floatingMasks.Add(0);
                        int counter = 35;
                        foreach (char c in line.Split(' ')[2])
                        {
                            if (c == '1')
                            {
                                ones_mask |= (UInt64)1 << counter;
                            }
                            else if (c == 'X')
                            {
                                foreach (var mask in floatingMasks.ToList())
                                {
                                    floatingMasks.Add(mask | ((UInt64)1 << counter));
                                }
                                floatingMask |= (UInt64)1 << counter;
                            }
                            counter--;
                        }
                        floatingMask = ~floatingMask;
                    }
                    else
                    {
                        var matches = numbersRx.Match(line);
                        var address = UInt64.Parse(matches.Groups[1].Value);
                        var number = UInt64.Parse(matches.Groups[2].Value);
                        address |= ones_mask;
                        address &= floatingMask; // set the mask bits for zero so each of the permutations can be applied.
                        foreach (var mask in floatingMasks)
                        {
                            memory[address | mask] = number;
                        }
                    }
                }
            }
            BigInteger total = 0;
            foreach (var (key, value) in memory)
            {
                total += value;
            }
            return total;
        }
    }
}
