using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode2020
{
    class Day8
    {
        public static int Part1()
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
            while (!visited.Contains(fp))
            {
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
            return acc;
        }

        public static (bool, int) RunProgramToEnd(List<KeyValuePair<string, int>> program, HashSet<int> visited, int fp, int acc)
        {
            while (!visited.Contains(fp))
            {
                if (fp >= program.Count)
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

        public static int Part2()
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
            while (true)
            {
                if (program[fp].Key == "nop")
                {
                    var clone = new List<KeyValuePair<string, int>>(program);
                    clone[fp] = new KeyValuePair<string, int>("jmp", clone[fp].Value);
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
    }
}
