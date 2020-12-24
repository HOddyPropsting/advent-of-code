using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020
{
    class Day15
    {
        public static int Part1()
        {
            List<int> numbers = new List<int> { 2, 0, 1, 7, 4, 14, 18 };
            Dictionary<int, int> seen = new Dictionary<int, int>();
            for (int i = 0; i < numbers.Count; i++)
            {
                seen[numbers[i]] = i + 1;
            }
            numbers.Add(0);
            int turn = numbers.Count;
            while (turn < 2020)
            {
                if (seen.ContainsKey(numbers[turn - 1]))
                {
                    numbers.Add(turn - seen[numbers[turn - 1]]);
                    seen[numbers[turn - 1]] = turn;
                }
                else
                {
                    seen[numbers[turn - 1]] = turn;
                    numbers.Add(0);
                }
                turn++;
            }
            return numbers.Last();
        }
        public static int Part2()
        {
            List<int> numbers = new List<int> { 2, 0, 1, 7, 4, 14, 18 };
            Dictionary<int, int> seen = new Dictionary<int, int>();
            for (int i = 0; i < numbers.Count; i++)
            {
                seen[numbers[i]] = i + 1;
            }
            numbers.Add(0);
            int turn = numbers.Count;
            while (turn < 30000000)
            {
                if (seen.ContainsKey(numbers[turn - 1]))
                {
                    numbers.Add(turn - seen[numbers[turn - 1]]);
                    seen[numbers[turn - 1]] = turn;
                }
                else
                {
                    seen[numbers[turn - 1]] = turn;
                    numbers.Add(0);
                }
                turn++;
            }
            return numbers.Last();
        }

    }
}
