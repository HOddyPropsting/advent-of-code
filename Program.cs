using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Day 1: {0}", Day1());
            Console.WriteLine("Day 1 Part 2: {0}", Day1Part2());
            Console.WriteLine("Day 2: {0}", Day2());
            Console.WriteLine("Day 2 Part 2: {0}", Day2Part2());
            Console.WriteLine("Day 3: {0}", Day3(3,1));
            Console.WriteLine("Day 3 Part 2: {0}", Day3Part2());
            Console.WriteLine("Day 4: {0}", Day4());
            Console.WriteLine("Day 4 Part 2: {0}", Day4Part2());
            Console.WriteLine("Day 5: {0}", Day5());
            Console.WriteLine("Day 5 Part 2: {0}", Day5Part2());
            Console.WriteLine("Day 6: {0}", Day6());
            Console.WriteLine("Day 6 Part 2: {0}", Day6Part2());
            Console.WriteLine("Day 7: {0}", Day7());
            Console.WriteLine("Day 7 Part 2: {0}", Day7Part2());
            Console.WriteLine("Day 8: {0}", Day8());
            Console.WriteLine("Day 8 Part 2: {0}", Day8Part2());
            Console.WriteLine("Day 9: {0}", Day9());
            Console.WriteLine("Day 9 Part 2: {0}", Day9Part2());
            Console.WriteLine("Day 10: {0}", Day10());
            Console.WriteLine("Day 10 Part 2: {0}", Day10Part2());
            Console.WriteLine("Day 11: {0}", Day11());
            Console.WriteLine("Day 11 Part 2: {0}", Day11Part2());
        }
        static int Day1()
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
            foreach (var i in input) {
                if (valuesToFind.Contains(i)) {
                    return i * (2020 - i);
                }
            }
            return 0;
        }

        static int Day1Part2()
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

            for(int i = 0; i < input.Count; i++)
            {
                for(int j = i+1; j < input.Count; j++)
                {
                    for (int k = j+1; k < input.Count; k++)
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

        class Day2Password
        {
            int min, max;
            string c;
            string password;

            public Day2Password(int min, int max, string c, string password)
            {
                this.min = min;
                this.max = max;
                this.c = c;
                this.password = password;
            }

            public bool isValid()
            {
                var number = password.Length - password.Replace(c, string.Empty).Length;
                return min <= number && number <= max;
            }

            public bool isValidPart2()
            {
                var minChar = password[min - 1];
                var maxChar = password[max - 1];
                if(minChar == c[0] && maxChar != c[0])
                {
                    return true;
                } else if (minChar != c[0] && maxChar == c[0])
                {
                    return true;
                }
                return false;
            }

            public Day2Password(string source)
            {
                Regex rx = new Regex(@"([0-9]*)-([0-9]*) ([a-z]): ([a-z]*)");
                MatchCollection matches = rx.Matches(source);
                min = Int32.Parse(matches[0].Groups[1].Value);
                max = Int32.Parse(matches[0].Groups[2].Value);
                c = matches[0].Groups[3].Value;
                password = matches[0].Groups[4].Value;
            }
        }

        static int Day2()
        {
            int _numberValid = 0;
            using (Stream stream = File.Open(@"day2", FileMode.Open))
            using (TextReader sr = new StreamReader(stream, Encoding.UTF8))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var text = new Day2Password(line);
                    if (text.isValid()) _numberValid++;
                }
            }
            return _numberValid;
        }

        static int Day2Part2()
        {
            int _numberValid = 0;
            using (Stream stream = File.Open(@"day2", FileMode.Open))
            using (TextReader sr = new StreamReader(stream, Encoding.UTF8))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var text = new Day2Password(line);
                    if (text.isValidPart2()) _numberValid++;
                }
            }
            return _numberValid;
        }

        class Day3Map
        {
            int width;
            int height;
            List<String> map;

            public Day3Map()
            {
                map = new List<String>();
                using (Stream stream = File.Open(@"day3", FileMode.Open))
                using (TextReader sr = new StreamReader(stream, Encoding.UTF8))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        map.Add(line);
                    }
                }

                width = map[0].Length;
                height = map.Count;
            }

            public int treesOnSlope(int across, int down)
            {
                int x = 0; int y = 0; int treeCount = 0;
                while(y < height)
                {
                    if (map[y][x] == '#') treeCount++;
                    y += down;
                    x = (x + across) % width;
                }
                return treeCount;
            }
        }

        public static int Day3(int across, int down)
        {
            var map = new Day3Map();
            return map.treesOnSlope(across, down);
        }

        public static System.UInt64 Day3Part2()
        {
            var map = new Day3Map();
            return ((ulong)map.treesOnSlope(1, 1) * (ulong)map.treesOnSlope(3, 1) * (ulong)map.treesOnSlope(5, 1) * (ulong)map.treesOnSlope(7, 1) * (ulong)map.treesOnSlope(1, 2));
        }

        public static int Day4()
        {
            int result = 0;
            using (Stream stream = File.Open(@"day4", FileMode.Open))
            using (TextReader sr = new StreamReader(stream, Encoding.UTF8))
            {
                string line;
                int numberOfFields = 0;
                int cidPresent = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    if(line.Length == 0)
                    {
                        if (numberOfFields - cidPresent == 7) result++;
                        numberOfFields = 0;
                        cidPresent = 0;
                    } else
                    {
                        foreach(var field in line.Split(@" "))
                        {
                            if (field.StartsWith("cid")) cidPresent++;
                            numberOfFields++;
                        }
                    }
                }
                if (numberOfFields - cidPresent == 7) result++;
            }
            return result;
        }
        public static int Day4Part2()
        {
            int result = 0;
            using (Stream stream = File.Open(@"day4", FileMode.Open))
            using (TextReader sr = new StreamReader(stream, Encoding.UTF8))
            {
                string line;
                int numberOfFields = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Length == 0)
                    {
                        if (numberOfFields == 7) result++;
                        numberOfFields = 0;
                    }
                    else
                    {
                        foreach (var field in line.Split(@" "))
                        {
                            /*
                             * 
    byr (Birth Year) - four digits; at least 1920 and at most 2002.
    iyr (Issue Year) - four digits; at least 2010 and at most 2020.
    eyr (Expiration Year) - four digits; at least 2020 and at most 2030.
    hgt (Height) - a number followed by either cm or in:
        If cm, the number must be at least 150 and at most 193.
        If in, the number must be at least 59 and at most 76.
    hcl (Hair Color) - a # followed by exactly six characters 0-9 or a-f.
    ecl (Eye Color) - exactly one of: amb blu brn gry grn hzl oth.
    pid (Passport ID) - a nine-digit number, including leading zeroes.
    cid (Country ID) - ignored, missing or not.

                             */
                            if (field.StartsWith("cid"))
                            {
                                //cidPresent++;
                                //numberOfFields++;
                            } else if (field.StartsWith("byr"))
                            {
                                var year = field.Split(":")[1];
                                if (year.Length == 4 && Int32.Parse(year) >= 1920 && Int32.Parse(year) <= 2002) numberOfFields++;
                            } else if (field.StartsWith("iyr"))
                            {
                                var year = field.Split(":")[1];
                                if (year.Length == 4 && Int32.Parse(year) >= 2010 && Int32.Parse(year) <= 2020) numberOfFields++;
                            } else if (field.StartsWith("eyr"))
                            {
                                var year = field.Split(":")[1];
                                if (year.Length == 4 && Int32.Parse(year) >= 2020 && Int32.Parse(year) <= 2030) numberOfFields++;
                            } else if (field.StartsWith("hgt"))
                            {
                                var height = field.Split(":")[1];
                                Regex rx = new Regex(@"[0-9]*in|cm");
                                if (!rx.IsMatch(height)) continue;
                                var value = Int32.Parse(height.Substring(0, height.Length - 2));
                                var type = height.Substring(height.Length - 2);
                                if (type == "in" && value >= 59 && value <= 76)
                                { 
                                    numberOfFields++; 
                                }  else if (type == "cm" && value >= 150 && value <= 193)
                                { 
                                    numberOfFields++;
                                }
                            } else if (field.StartsWith("hcl"))
                            {
                                var value = field.Split(":")[1];
                                Regex rx = new Regex(@"#[0-9a-f]{6}$");
                                if (rx.IsMatch(value)) numberOfFields++;
                            } else if (field.StartsWith("ecl"))
                            {
                                var value = field.Split(":")[1];
                                Regex rx = new Regex(@"amb|blu|brn|gry|grn|hzl|oth");
                                if (rx.IsMatch(value)) numberOfFields++;
                            } else if (field.StartsWith("pid"))
                            {
                                var value = field.Split(":")[1];
                                Regex rx = new Regex(@"^[0-9]{9}$");
                                if (rx.IsMatch(value)) numberOfFields++;
                            }
                        }
                    }
                }
                if (numberOfFields == 7) result++;
            }
            return result;
        }
    
        public static int Day5()
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

        public static int Day5Part2()
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
                var sum = ((maxID-minID) * (minID + maxID + 1)) / 2 + minID;
                foreach(var seat in seatNumbers)
                {
                    sum -= seat;
                }
                return sum;
            }
        }
        public static int Day6()
        {
            using (Stream stream = File.Open(@"day6", FileMode.Open))
            using (TextReader sr = new StreamReader(stream, Encoding.UTF8))
            {
                string line;
                StringBuilder group = new StringBuilder();
                int sum = 0;
                while ((line = sr.ReadLine()) != null)
                    {
                        if (line.Length == 0)
                        {
                            sum += group.ToString().Distinct().Count();
                            group.Clear();
                        } else
                        {
                            group.Append(line);
                        }
                    }
                sum += group.ToString().Distinct().Count();
                return sum;
            }
        }

        public static int Day6Part2()
        {
            using (Stream stream = File.Open(@"day6", FileMode.Open))
            using (TextReader sr = new StreamReader(stream, Encoding.UTF8))
            {
                string line;
                Dictionary<char, int> answers = new Dictionary<char, int>();
                int numberInGroup = 0;
                int sum = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Length == 0)
                    {
                        foreach(var (key, value) in answers)
                        {
                            if (value == numberInGroup) sum++;
                        }
                        numberInGroup = 0;
                        answers.Clear();
                    }
                    else
                    {
                        numberInGroup++;
                        foreach(char c in line)
                        {
                            if (answers.ContainsKey(c))
                            {
                                answers[c] = answers[c] + 1;
                            } else
                            {
                                answers.Add(c, 1);
                            }
                        }
                    }
                }
                foreach (var (key, value) in answers)
                {
                    if (value == numberInGroup) sum++;
                }
                numberInGroup = 0;
                answers.Clear();
                return sum;
            }
        }

        public static int Day7()
        {
            Dictionary<String, List<String>> graph = new Dictionary<string, List<string>>();
            Regex bagRx = new Regex(@"^([a-z]* [a-z]*)");
            Regex contentsRx = new Regex(@"([0-9]*) ([a-z]* [a-z]*) bag");
            using (Stream stream = File.Open(@"day7", FileMode.Open))
            using (TextReader sr = new StreamReader(stream, Encoding.UTF8))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    MatchCollection bagColour = bagRx.Matches(line);
                    MatchCollection bagContents = contentsRx.Matches(line);
                    
                    foreach(Match match in bagContents)
                    {
                        if(graph.ContainsKey(match.Groups[2].Value))
                        {
                            graph[match.Groups[2].Value].Add(bagColour[0].Value);
                        } else
                        {
                            List<String> contents = new List<string>();
                            contents.Add(bagColour[0].Value);
                            graph.Add(match.Groups[2].Value, contents);
                        }
                    }
                }
            }
            Stack<string> containers = new Stack<string>(graph["shiny gold"]);
            HashSet<string> tested = new HashSet<string>();
            while(containers.Count != 0)
            {
                string bag = containers.Pop();
                tested.Add(bag);
                foreach (var outerBag in graph.GetValueOrDefault(bag, new List<string>()))
                {
                    if(!tested.Contains(outerBag))
                    {
                        containers.Push(outerBag);
                    }
                }
            }
            return tested.Count;
        }
        public static int Day7Part2()
        {
            Dictionary<String, List<(String, int)>> graph = new Dictionary<string, List<(string, int)>>();
            int count = 0;
            Regex bagRx = new Regex(@"^([a-z]* [a-z]*)");
            Regex contentsRx = new Regex(@"([0-9]+) ([a-z]+ [a-z]+) bag");
            using (Stream stream = File.Open(@"day7", FileMode.Open))
            using (TextReader sr = new StreamReader(stream, Encoding.UTF8))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    MatchCollection bagColour = bagRx.Matches(line);
                    MatchCollection bagContents = contentsRx.Matches(line);
                    List<(String, int)> contents = new List<(string, int)>();
                    foreach (Match match in bagContents)
                    {
                        contents.Add((match.Groups[2].Value, Int32.Parse(match.Groups[1].Value)));
                    }
                    graph[bagColour[0].Value] = contents;
                }
            }
            Stack<(string, int)> containers = new Stack<(string, int)>(graph["shiny gold"]);
            count += containers.Peek().Item2;
            while (containers.Count != 0)
            {
                var (bag, number) = containers.Pop();
                foreach (var (innerBag, numberOfBags) in graph.GetValueOrDefault(bag, new List<(string, int)>()))
                {
                    count += number * numberOfBags;
                    containers.Push((innerBag, number * numberOfBags));
                }
            }
            return count;
        }

        public static int Day8()
        {
            int acc = 0; //accumulator
            int fp = 0;  //function pointer

            List<KeyValuePair<string, int>> program = new List<KeyValuePair<string, int>>(); //list of <instruction, instruction value>
            HashSet<int> visited = new HashSet<int>(); //we have to use a hashset as we need to know as soon as we have revisited the same instruction

            using (Stream stream = File.Open(@"day8", FileMode.Open))
            using (TextReader sr = new StreamReader(stream, Encoding.UTF8))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var parts = line.Split(" ");
                    program.Add(new KeyValuePair<string, int>(parts[0], Int32.Parse(parts[1])));
                }
            }
            while(!visited.Contains(fp))
            {
                visited.Add(fp);
                switch(program[fp].Key)
                {
                    case "nop":
                        fp++;
                        break;
                    case "acc":
                        acc += program[fp].Value;
                        fp++;
                        break;
                    case "jmp":
                        fp += program[fp].Value;
                        break;
                }
            }
            return acc;
        }

        public static (bool,int) RunProgramToEnd(List<KeyValuePair<string, int>> program, HashSet<int> visited, int fp, int acc)
        {
            while(!visited.Contains(fp))
            {
                if(fp >= program.Count)
                {
                    return (true, acc);
                }
                visited.Add(fp);
                switch (program[fp].Key)
                {
                    case "nop":
                        fp++;
                        break;
                    case "acc":
                        acc += program[fp].Value;
                        fp++;
                        break;
                    case "jmp":
                        fp += program[fp].Value;
                        break;
                }
            }
            return (false, acc);
        }

        public static int Day8Part2()
        {
            int acc = 0; //accumulator
            int fp = 0;  //function pointer
            int changed_pointer =-1;

            List<KeyValuePair<string, int>> program = new List<KeyValuePair<string, int>>(); //list of <instruction, instruction value>
            HashSet<int> visited = new HashSet<int>(); //we have to use a hashset as we need to know as soon as we have revisited the same instruction

            using (Stream stream = File.Open(@"day8", FileMode.Open))
            using (TextReader sr = new StreamReader(stream, Encoding.UTF8))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var parts = line.Split(" ");
                    program.Add(new KeyValuePair<string, int>(parts[0], Int32.Parse(parts[1])));
                }
            }
            while (true)
            {
                if(program[fp].Key == "nop")
                {
                    var clone = new List<KeyValuePair<string, int>>(program);
                    clone[fp] = new KeyValuePair<string, int>("jmp",clone[fp].Value);
                    var (ranToEnd, value) = RunProgramToEnd(clone, new HashSet<int>(visited), fp, acc);
                    if (ranToEnd) return value;
                }

                if (program[fp].Key == "jmp")
                {
                    var clone = new List<KeyValuePair<string, int>>(program);
                    clone[fp] = new KeyValuePair<string, int>("nop", clone[fp].Value);
                    var (ranToEnd, value) = RunProgramToEnd(clone, new HashSet<int>(visited), fp, acc);
                    if (ranToEnd) return value;
                }
                visited.Add(fp);

                switch (program[fp].Key)
                {
                    case "nop":
                        fp++;
                        break;
                    case "acc":
                        acc += program[fp].Value;
                        fp++;
                        break;
                    case "jmp":
                        fp += program[fp].Value;
                        break;
                }
            }
        }

        public static bool SumInList(IEnumerable<int> list, int value)
        {
            List<int> otherHalf = list.Select(x => value - x).ToList();
            foreach(var val in list)
            {
                if (otherHalf.Contains(val)) return true;
            }
            return false;
        }

        public static bool SumInListLinq(IEnumerable<int> list, int value)
        {
            return list.Select(x => value - x).Intersect(list).Count() > 0;
        }


        public static int Day9()
        {
            Queue<int> buffer = new Queue<int>(25);
            using (Stream stream = File.Open(@"day9", FileMode.Open))
            using (TextReader sr = new StreamReader(stream, Encoding.UTF8))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    int val = Int32.Parse(line);
                    if(buffer.Count == 25)
                    {
                        
                        var result = SumInList(buffer, val);
                        if (!result) return val;
                        buffer.Dequeue();
                        buffer.Enqueue(val);
                    } else
                    {
                        buffer.Enqueue(val);
                    }
                }
            }
            return 0;
        }

        public static Int64 Day9Part2()
        {
            Int64 searchValue = Day9();
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
                        while(buffer.Sum() > searchValue)
                        {
                            buffer.Dequeue();
                        }
                    }

                }
            }
            return 0;
        }


        public static int Day10()
        {
            List<int> adapters = new List<int>();
            adapters.Add(0); //socket is 0
            using (Stream stream = File.Open(@"day10", FileMode.Open))
            using (TextReader sr = new StreamReader(stream, Encoding.UTF8))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    adapters.Add(Int32.Parse(line));
                }
            }

            adapters.Sort();
            Dictionary<int,int> differences = new Dictionary<int, int>();
            differences.Add(1, 0);
            differences.Add(2, 0);
            differences.Add(3, 1); // built in is always 3 higher
            for (int i = 1; i < adapters.Count; i++)
            {
                differences[adapters[i] - adapters[i - 1]]++;
            }

            return differences[1] * differences[3];
        }

        public static BigInteger Day10Part2()
        {
            List<int> adapters = new List<int>();
            adapters.Add(0); //socket is 0
            using (Stream stream = File.Open(@"day10", FileMode.Open))
            using (TextReader sr = new StreamReader(stream, Encoding.UTF8))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    adapters.Add(Int32.Parse(line));
                }
            }

            adapters.Sort();
            adapters.Add(adapters.Last() + 3); //device is 3 greater than the greatest adapter
            List<int> differences = new List<int>();
            for (int i = 1; i < adapters.Count; i++)
            {
                differences.Add(adapters[i] - adapters[i - 1]);
            }
            List<int> streaks = new List<int>();
            int streakCount = 0;
            foreach(var diff in differences)
            {
                if(diff == 3)
                { 
                    if(streakCount > 1)
                    {
                        streaks.Add(streakCount);
                    }
                    streakCount = 0;
                } else
                {
                    streakCount++;
                }
            }
            BigInteger variants = 1;
            foreach(var streak in streaks)
            {
                switch (streak)
                {
                    case 2:
                        variants *= 2; //2 ways to re-write a 1 1 streak
                        break;
                    case 3:
                        variants *= 4; // 4 ways to re-write a 1 1 1 streak
                        break;
                    case 4:
                        variants *= 7; // 7 ways to re-write a 1 1 1 1 streak?
                        break;
                    default:
                        throw new Exception("error");
                }
            }
            return variants;
        }

        public static char applyKernel(char[,] map, int x, int y)
        {
            int count=0;
            if (map[x, y] == '.') return '.';

            for(int i = x-1; i<=x+1;i++)
            {
                if (i < 0 || i >= map.GetLength(0)) continue;
                for (int j = y - 1; j <= y + 1; j++)
                {
                    if (j < 0 || j >= map.GetLength(1)) continue;
                    if (map[i, j] == '#') count++;
                }
            }
            if(map[x,y] == '#')
            {
                return count >= 5 ? 'L' : '#';
            } else
            {
                return count == 0 ? '#' : 'L';
            }
        }

        public static char applyLoS(char[,] map, int x, int y)
        {
            int count = 0;
            if (map[x, y] == '.') return '.';
            for (var (i, j) = (x - 1, y - 1); i >= 0 && j >= 0; i--, j--)// - -
            {
                if (map[i, j] == 'L') break;
                if (map[i, j] == '#')
                {
                    count++; break;
                }
            }
            for (var (i, j) = (x, y - 1); j >= 0; j--)// 0 -
            {
                if (map[i, j] == 'L') break;
                if (map[i, j] == '#')
                {
                    count++; break;
                }
            }
            for (var (i, j) = (x + 1, y - 1); i < map.GetLength(0) && j >= 0; i++, j--)// + -
            {
                if (map[i, j] == 'L') break;
                if (map[i, j] == '#')
                {
                    count++; break;
                }
            }
            for (var (i, j) = (x + 1, y); i < map.GetLength(0); i++) //+ 0
            {
                if (map[i, j] == 'L') break;
                if (map[i, j] == '#')
                {
                    count++; break;
                }
            }
            for (var (i, j) = (x + 1, y + 1); i < map.GetLength(0) && j < map.GetLength(1); i++, j++)// + +
            {
                if (map[i, j] == 'L') break;
                if (map[i, j] == '#')
                {
                    count++; break;
                }
            }
            for (var (i, j) = (x, y + 1); j < map.GetLength(1); j++) //0 +
            {
                if (map[i, j] == 'L') break;
                if (map[i, j] == '#')
                {
                    count++; break;
                }
            }
            for (var (i, j) = (x - 1, y + 1); i >= 0 && j < map.GetLength(1); i--, j++)
            {
                if (map[i, j] == 'L') break;
                if (map[i, j] == '#')
                {
                    count++; break;
                }
            }
            for (var (i, j) = (x - 1, y); i >= 0 ; i--)
            {
                if (map[i, j] == 'L') break;
                if (map[i, j] == '#')
                {
                    count++; break;
                }
            }
            if (map[x, y] == '#')
            {
                return count >= 5 ? 'L' : '#';
            }
            else
            {
                return count == 0 ? '#' : 'L';
            }
        }

        public static int Day11()
        {
            List<int> adapters = new List<int>();
            adapters.Add(0); //socket is 0
            var width = 0;
            var height = 1;
            using (Stream stream = File.Open(@"day11", FileMode.Open))
            using (TextReader sr = new StreamReader(stream, Encoding.UTF8))
            {
                string line;
                line = sr.ReadLine();
                width = line.Length;
                while(sr.ReadLine() != null)
                {
                    height++;
                }
            }
            char[,] map = new char[width, height];
            char[,] backup = new char[width, height];
            using (Stream stream = File.Open(@"day11", FileMode.Open))
            using (TextReader sr = new StreamReader(stream, Encoding.UTF8))
            {
                string line;
                int i = 0;
                int j = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    foreach(var c in line)
                    {
                        map[i, j] = c;
                        i++;
                    }
                    i = 0;
                    j++;
                }
            }

            bool changed = true;
            while(changed)
            {
                changed = false;
                for (int i = 0; i < map.GetLength(0); i++)
                {
                    for (int j = 0; j < map.GetLength(1); j++)
                    {
                        backup[i, j] = applyKernel(map, i, j);
                    }
                }

                for (int i = 0; i < map.GetLength(0); i++)
                {
                    for (int j = 0; j < map.GetLength(1); j++)
                    {
                        if (map[i, j] != backup[i, j]) changed = true;
                        map[i, j] = backup[i, j];
                    }
                }
            }
            int counter = 0;
            foreach(var c in map)
            {
                if (c == '#') counter++;
            }
            return counter;
        }

        public static int Day11Part2()
        {
            List<int> adapters = new List<int>();
            adapters.Add(0); //socket is 0
            var width = 0;
            var height = 1;
            using (Stream stream = File.Open(@"day11", FileMode.Open))
            using (TextReader sr = new StreamReader(stream, Encoding.UTF8))
            {
                string line;
                line = sr.ReadLine();
                width = line.Length;
                while (sr.ReadLine() != null)
                {
                    height++;
                }
            }
            char[,] map = new char[width, height];
            char[,] backup = new char[width, height];
            using (Stream stream = File.Open(@"day11", FileMode.Open))
            using (TextReader sr = new StreamReader(stream, Encoding.UTF8))
            {
                string line;
                int i = 0;
                int j = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    foreach (var c in line)
                    {
                        map[i, j] = c;
                        i++;
                    }
                    i = 0;
                    j++;
                }
            }

            bool changed = true;
            while (changed)
            {
                changed = false;
                for (int i = 0; i < map.GetLength(0); i++)
                {
                    for (int j = 0; j < map.GetLength(1); j++)
                    {
                        backup[i, j] = applyLoS(map, i, j);
                    }
                }

                for (int i = 0; i < map.GetLength(0); i++)
                {
                    for (int j = 0; j < map.GetLength(1); j++)
                    {
                        if (map[i, j] != backup[i, j]) changed = true;
                        map[i, j] = backup[i, j];
                        //Console.Write(map[i, j]);
                    }
                   //Console.WriteLine();
                }
                //Console.WriteLine("=========================================================================");
            }
            int counter = 0;
            foreach (var c in map)
            {
                if (c == '#') counter++;
            }
            return counter;
        }

    }
}
