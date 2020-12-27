using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2020
{
    class Day23
    {
        public static int Mod(int i, int j)
        {
            return (i + j) % j;        }

        public static string Part1()
        {
            List<int> buffer = new List<int> { 3, 6, 8, 1, 9, 5, 7, 4, 2 };
            //List<int> buffer = new List<int> { 3, 8, 9, 1, 2, 5, 4, 6, 7 }; // test input
            int currentCup = 0;
            for(int _ = 0; _ < 100; _++)
            {
                //Console.Write(string.Concat(buffer));
                List<int> cups = new List<int>();
                var curCp = buffer[currentCup];
                var destinationCup = buffer[currentCup]-1;
                destinationCup = destinationCup == 0 ? 9 : destinationCup;
                cups.Add(buffer[(currentCup + 1)%9]);
                cups.Add(buffer[(currentCup + 2)%9]);
                cups.Add(buffer[(currentCup + 3)%9]);
                var removeCup = (currentCup+1) % 9;
                //Console.Write(" " + string.Concat(cups));
                buffer.RemoveAt(removeCup); removeCup %= 8; 
                buffer.RemoveAt(removeCup); removeCup %= 7;
                buffer.RemoveAt(removeCup);
                //Console.Write(" " + destinationCup);
                while (cups.Contains(destinationCup) || destinationCup == 0)
                {
                    destinationCup = Mod(destinationCup - 1, 9);
                    destinationCup = destinationCup == 0 ? 9 : destinationCup;
                }
                //Console.WriteLine(" " + destinationCup);
                var idx = buffer.IndexOf(destinationCup)+1;
                buffer.InsertRange(idx, cups);
                currentCup = Mod(buffer.IndexOf(curCp) + 1, 9);
            }
            return string.Concat(buffer);
        }

        class CircularListNode
        {
            public CircularListNode next { get; set; }
            public int value { get; set; }

            public CircularListNode(int value)
            {
                this.next = this;
                this.value = value;
            }
        }

        class CircularList
        {
            CircularListNode current;
            CircularListNode first;

            Dictionary<int, CircularListNode> nodeMap;

            public CircularList()
            {
                nodeMap = new Dictionary<int, CircularListNode>();
            }

            public void Add(int i)
            {
                if (current is null)
                {
                    current = new CircularListNode(i);
                    nodeMap.Add(i, current);
                    first = current;
                }
                else
                {
                    current.next = new CircularListNode(i);
                    nodeMap.Add(i, current.next);
                    current.next.next = first;
                }
                current = current.next;
            }

            public CircularListNode TakeThree()
            {
                CircularListNode a = current.next;
                CircularListNode b = a.next;
                CircularListNode c = b.next;
                current.next = c.next;
                c.next = null;
                return a;
            }

            public void Insert(CircularListNode a)
            {
                var next = a;
                while (next.next != null)
                    next = next.next;
                next.next = current.next;
                current.next = a;
            }

            public static void Insert(CircularListNode list, CircularListNode fragment)
            {
                var next = fragment;
                while (next.next != null)
                    next = next.next;
                next.next = list.next;
                list.next = fragment;
            }

            public CircularListNode Find(int val)
            {
                return nodeMap[val];
            }

            public static CircularListNode Find(CircularListNode n, int val)
            {
                while (n != null && n.value != val)
                    n = n.next;
                return n;
            }

            public int GetValue()
            {
                return current.value;
            }

            public void Next()
            {
                current = current.next;
            }

            public void Reset()
            {
                current = first;
            }
        }

        public static long Part2()
        {
            List<int> buffer = new List<int> { 3, 6, 8, 1, 9, 5, 7, 4, 2 };
            CircularList list = new CircularList();
            foreach(int i in buffer)
                list.Add(i);
            for (int i = 10; i < 1000001; i++)
                list.Add(i);
            list.Reset();
            for (int _ = 0; _ < 10000000; _++)
            {
                //Console.Write(string.Concat(buffer));
                CircularListNode three = list.TakeThree();
                var searchValue = list.GetValue()-1;
                searchValue = searchValue == 0 ? 1000000 : searchValue;
                while (CircularList.Find(three, searchValue) != null)
                {
                    searchValue = Mod(searchValue - 1, 1000000);
                    searchValue = searchValue == 0 ? 1000000 : searchValue;
                }
                var writeNode = list.Find(searchValue);
                CircularList.Insert(writeNode, three);
                list.Next();
            }
            var oneNode = list.Find(1);
            var a1 = oneNode.next;
            var a2 = a1.next;
            return (long) a1.value * (long) a2.value;
            //return string.Concat(buffer);
        }
    }
}
