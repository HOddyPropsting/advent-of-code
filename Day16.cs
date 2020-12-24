using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2020
{
    class Day16
    {
        public static int Part1()
        {
            List<(int, int)> rules = new List<(int, int)>();
            int sum = 0;
            using (Stream stream = File.Open(@"day16", FileMode.Open))
            using (TextReader sr = new StreamReader(stream, Encoding.UTF8))
            {
                string line;
                while ((line = sr.ReadLine()) != "")
                {
                    foreach (var pair in line.Split(':')[1].Split(' '))
                    {
                        if (pair.Length <= 2) continue;
                        var splitPair = pair.Split('-');
                        rules.Add((Int32.Parse(splitPair[0]), Int32.Parse(splitPair[1])));
                    }
                }
                sr.ReadLine();
                sr.ReadLine();
                sr.ReadLine();
                sr.ReadLine();
                while ((line = sr.ReadLine()) != null)
                {
                    foreach (var number in line.Split(','))
                    {
                        bool valid = false;
                        var temp = Int32.Parse(number);
                        foreach (var (min, max) in rules)
                        {
                            if (temp >= min && temp <= max)
                            {
                                valid = true;
                                break;
                            }
                        }
                        if (!valid) sum += temp;
                    }
                }
            }
            return sum;
        }
        public static Int64 Part2()
        {
            List<(string, int, int, int, int)> rules = new List<(string, int, int, int, int)>();
            List<int> YourTicket = new List<int>();
            Dictionary<int, List<string>> ruleMap = new Dictionary<int, List<string>>(); //position to rule map
            using (Stream stream = File.Open(@"day16", FileMode.Open))
            using (TextReader sr = new StreamReader(stream, Encoding.UTF8))
            {
                string line;
                while ((line = sr.ReadLine()) != "")
                {
                    var split = line.Split(':');
                    var splitRanges = split[1].Split(' ');
                    var firstRange = splitRanges[1].Split('-');
                    var secondRange = splitRanges[3].Split('-');
                    rules.Add((split[0], Int32.Parse(firstRange[0]), Int32.Parse(firstRange[1]), Int32.Parse(secondRange[0]), Int32.Parse(secondRange[1])));
                }
                for (int i = 0; i < rules.Count; i++)
                {
                    ruleMap[i] = new List<string>();
                }
                for (int i = 0; i < rules.Count; i++)
                {
                    for (int j = 0; j < rules.Count; j++)
                    {
                        ruleMap[i].Add(rules[j].Item1);
                    }
                }
                sr.ReadLine();
                YourTicket.AddRange(sr.ReadLine().Split(',').Select(x => Int32.Parse(x)));
                sr.ReadLine();
                sr.ReadLine();
                while ((line = sr.ReadLine()) != null)
                {
                    foreach (var (number, index) in line.Split(',').Select((item, index) => (item, index)))
                    {
                        bool valid = false;
                        var temp = Int32.Parse(number);
                        List<string> failedRules = new List<string>();
                        foreach (var (rule, min1, max1, min2, max2) in rules)
                        {
                            if ((temp >= min1 && temp <= max1) || (temp >= min2 && temp <= max2))
                            {
                                valid = true;
                            }
                            else
                            {
                                failedRules.Add(rule);
                            }
                        }
                        if (!valid) goto NEXTLINE; // skip processing this line as the ticket is invalid
                        ruleMap[index] = ruleMap[index].Except(failedRules).ToList();
                    }
                // we know the line is now valid
                NEXTLINE:;
                }
            }
            // The map only has one unique entires, we can then follow the chain to get the map with only that entry.
            List<int> singleRules = new List<int>();
            while (ruleMap.Sum(x => x.Value.Count) > 20)
            {
                var rule = ruleMap.Where(x => x.Value.Count == 1 && !singleRules.Contains(x.Key)).First().Key;
                singleRules.Add(rule);
                foreach (var (key, _) in ruleMap.ToList())
                {
                    if (key == rule) continue;
                    ruleMap[key] = ruleMap[key].Except(ruleMap[rule]).ToList();
                }
            }
            Int64 sum = 1;
            foreach (var rule in ruleMap.Select(x => new KeyValuePair<int, string>(x.Key, x.Value[0])).Where(x => x.Value.Contains("departure")))
            {
                sum *= YourTicket[rule.Key];
            }
            return sum;
        }
    }
}
