using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    class Day7
    {
        public static int Part1()
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

                    foreach (Match match in bagContents)
                    {
                        if (graph.ContainsKey(match.Groups[2].Value))
                        {
                            graph[match.Groups[2].Value].Add(bagColour[0].Value);
                        }
                        else
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
            while (containers.Count != 0)
            {
                string bag = containers.Pop();
                tested.Add(bag);
                foreach (var outerBag in graph.GetValueOrDefault(bag, new List<string>()))
                {
                    if (!tested.Contains(outerBag))
                    {
                        containers.Push(outerBag);
                    }
                }
            }
            return tested.Count;
        }
        public static int Part2()
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
    }
}
