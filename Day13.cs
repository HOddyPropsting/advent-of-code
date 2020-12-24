using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    class Day13
    {
        public static int Part1()
        {
            int time = 0;
            int bestBus = Int32.MaxValue;
            Regex busRx = new Regex(@"([0-9]+)");
            using (Stream stream = File.Open(@"day13", FileMode.Open))
            using (TextReader sr = new StreamReader(stream, Encoding.UTF8))
            {
                string line;
                line = sr.ReadLine();
                time = Int32.Parse(line);
                while ((line = sr.ReadLine()) != null)
                {
                    MatchCollection busses = busRx.Matches(line);

                    foreach (Match busMatch in busses)
                    {
                        int bus = Int32.Parse(busMatch.Value);
                        if ((bus - (time % bus)) < (bestBus - (time % bestBus)))
                        {
                            bestBus = bus;
                        }
                    }
                }
                var busArrivalTime = ((time / bestBus) + 1) * bestBus;
                return (busArrivalTime - time) * bestBus;
            }
        }


        public static BigInteger Part2()
        {
            List<(int, int)> busses = new List<(int, int)>();
            using (Stream stream = File.Open(@"day13", FileMode.Open))
            using (TextReader sr = new StreamReader(stream, Encoding.UTF8))
            {
                string line;
                line = sr.ReadLine();
                line = sr.ReadLine();
                int counter = -1;
                foreach (var part in line.Split(','))
                {
                    counter++;
                    if (part != "x")
                    {
                        busses.Add((Int32.Parse(part), counter));
                    }
                }
            }

            // all the numbers seem to be prime
            BigInteger time = 0;
            BigInteger multiple = 0;
            foreach (var (bus, arrivalTime) in busses)
            {
                if (time == 0)
                {
                    time = bus;
                    multiple = bus;
                }
                else
                {
                    while (true)
                    {
                        if ((time + arrivalTime) % bus == 0)
                        {
                            multiple *= bus;// they are prime so lcm == multiplying all of them
                            break;
                        }
                        time += multiple;
                    }
                }
            }
            return time;
        }
    }
}
