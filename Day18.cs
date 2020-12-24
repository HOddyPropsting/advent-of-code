using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2020
{
    class Day18
    {
        public static (long, int) calculate(List<string> input)
        {
            var currentOp = "+";
            long total = 0;
            for (int i = 0; i < input.Count; i++)
            {
                if (input[i] == "+") currentOp = "+";
                else if (input[i] == "*") currentOp = "*";
                else if (input[i] == "(")
                {
                    var (temp, index) = calculate(input.GetRange(i + 1, input.Count - i - 1));
                    switch (currentOp)
                    {
                        case "+": total += temp; break;
                        case "*": total *= temp; break;
                    }
                    i += index + 1;
                }
                else if (input[i] == ")")
                {
                    return (total, i);
                }
                else
                {
                    switch (currentOp)
                    {
                        case "+": total += Int64.Parse(input[i]); break;
                        case "*": total *= Int64.Parse(input[i]); break;
                    }
                }
            }
            return (total, 0);
        }

        public static long Part1()
        {
            long total = 0;
            using (Stream stream = File.Open(@"day18", FileMode.Open))
            using (TextReader sr = new StreamReader(stream, Encoding.UTF8))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    total += calculate(line.Replace("(", "( ").Replace(")", " )").Split(' ').ToList()).Item1;
                }
            }
            return total;
        }

        public static long Part2()
        {
            long total = 0;
            using (Stream stream = File.Open(@"day18", FileMode.Open))
            using (TextReader sr = new StreamReader(stream, Encoding.UTF8))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    List<string> splitLine = line.Replace("(", "( ").Replace(")", " )").Split(' ').ToList();
                    for (int i = 0; i < splitLine.Count; i++)
                    {
                        if (splitLine[i] == "+")
                        {
                            if (splitLine[i + 1] == "(")
                            {
                                var depth = 1;
                                var j = i + 2;
                                while (depth != 0)
                                {
                                    if (splitLine[j] == ")") depth--;
                                    if (splitLine[j] == "(") depth++;
                                    j++;
                                }
                                splitLine.Insert(j, ")");
                            }
                            else
                            {
                                splitLine.Insert(i + 2, ")");
                            }
                            if (splitLine[i - 1] == ")")
                            {
                                var depth = 1;
                                var j = i - 2;
                                while (depth != 0)
                                {
                                    if (splitLine[j] == ")") depth++;
                                    if (splitLine[j] == "(") depth--;
                                    j--;
                                }
                                splitLine.Insert(j + 1, "(");
                            }
                            else splitLine.Insert(i - 1, "(");
                            i++;
                        }
                    }
                    total += calculate(splitLine).Item1;
                }
            }
            return total;
        }
    }
}
