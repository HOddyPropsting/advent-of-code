using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public class Day19
{
    enum NodeType
    {
        AND,
        OR,
        VALUE,
        UNINT,
    }

    class Node
    {
        public Node left;
        public Node right;
        public char value;
        public int rule;
        public NodeType nodeType;

        public Node()
        {
            left = null;
            right = null;
            value = ' ';
            rule = -1;
            nodeType = NodeType.UNINT;
        }

        public Node(int rule) : this()
        {
            this.rule = rule;
        }
    }

    static (bool, int) Matches(Node n, int i, string s)
    {
        var match = (false, 0);
        switch (n.nodeType)
        {
            case NodeType.AND:
                match = Matches(n.left, i, s);
                if (match.Item1) return Matches(n.right, match.Item2, s);
                return (false, i);
            case NodeType.OR:
                match = Matches(n.left, i, s);
                if (match.Item1) return match;
                return Matches(n.right, i, s);
            case NodeType.VALUE:
                if (s[i] == n.value) return (true, i + 1);
                return (false, i);
        }
        return (false, 0);
    }

    static List<int> Matches(Node n, Dictionary<int, Node> rules, int i, string s)
    {
        List<int> match = new List<int>();
        List<int> returnMatch = new List<int>();
        if (i >= s.Length) return match; //no string left - you've gone too deep
        switch (n.nodeType)
        {
            case NodeType.AND:
                match = Matches(n.left, rules, i, s);
                foreach (var idx in match)
                    returnMatch.AddRange(Matches(n.right, rules, idx, s));
                return returnMatch;
            case NodeType.OR:
                match = Matches(n.left, rules, i, s);
                match.AddRange(Matches(n.right, rules, i, s));
                return match;
            case NodeType.VALUE:
                if (s[i] == n.value) return new List<int> { i + 1 };
                return new List<int>();
            case NodeType.UNINT:
                return Matches(rules[n.rule], rules, i, s);
        }
        return match;
    }

    public static int Part1()
    {
        int counter = 0;
        Dictionary<int, Node> rules = new Dictionary<int, Node>();
        using (Stream stream = File.Open(@"day19", FileMode.Open))
        using (TextReader sr = new StreamReader(stream, Encoding.UTF8))
        {
            string line;
            while ((line = sr.ReadLine()) != "")
            {
                var split = line.Split(':');
                int ruleNumber = Int32.Parse(split[0]);
                split = split[1].Split(' ');
                Node n = new Node();
                switch (split.Length)
                {
                    case 6: // R R | R R
                        n.nodeType = NodeType.AND;
                        n.left = new Node(Int32.Parse(split[1]));
                        n.right = new Node(Int32.Parse(split[2]));
                        Node or = new Node(ruleNumber);
                        or.nodeType = NodeType.OR;
                        or.left = n;
                        Node r = new Node();
                        r.nodeType = NodeType.AND;
                        r.left = new Node(Int32.Parse(split[4]));
                        r.right = new Node(Int32.Parse(split[5]));
                        or.right = r;
                        or.rule = ruleNumber;
                        rules.Add(ruleNumber, or);
                        break;
                    case 4: // R | R
                        n.nodeType = NodeType.OR;
                        n.left = new Node(Int32.Parse(split[1]));
                        n.right = new Node(Int32.Parse(split[3]));
                        n.rule = ruleNumber;
                        rules.Add(ruleNumber, n);
                        break;
                    case 3: // R R
                        n.nodeType = NodeType.AND;
                        n.left = new Node(Int32.Parse(split[1]));
                        n.right = new Node(Int32.Parse(split[2]));
                        n.rule = ruleNumber;
                        rules.Add(ruleNumber, n);
                        break;
                    case 2: // "a" or R
                        if (split[1][0] == '"')
                        {
                            n.value = split[1][1];
                            n.rule = ruleNumber;
                            n.nodeType = NodeType.VALUE;
                        }
                        else
                        {
                            n.rule = Int32.Parse(split[1]);
                            n.nodeType = NodeType.UNINT;
                        }
                        rules[ruleNumber] = n;
                        break;
                }
            }
            var tree = rules[0];
            while ((line = sr.ReadLine()) != null)
            {
                var valid = Matches(tree, rules, 0, line).Where(i => i == line.Length).ToList(); //some of the matches will be partial, we only need 
                if (valid.Count > 0)
                {
                    counter++;
                }
            }
        }
        return counter;
    }

    public static int Part2()
    {
        int counter = 0;
        Dictionary<int, Node> rules = new Dictionary<int, Node>();
        using (Stream stream = File.Open(@"day19", FileMode.Open))
        using (TextReader sr = new StreamReader(stream, Encoding.UTF8))
        {
            string line;
            while ((line = sr.ReadLine()) != "")
            {
                var split = line.Split(':');
                int ruleNumber = Int32.Parse(split[0]);
                split = split[1].Split(' ');
                Node n = new Node();
                switch (split.Length)
                {
                    case 6: // R R | R R
                        n.nodeType = NodeType.AND;
                        n.left = new Node(Int32.Parse(split[1]));
                        n.right = new Node(Int32.Parse(split[2]));
                        Node or = new Node(ruleNumber);
                        or.nodeType = NodeType.OR;
                        or.left = n;
                        Node r = new Node();
                        r.nodeType = NodeType.AND;
                        r.left = new Node(Int32.Parse(split[4]));
                        r.right = new Node(Int32.Parse(split[5]));
                        or.right = r;
                        or.rule = ruleNumber;
                        rules.Add(ruleNumber, or);
                        break;
                    case 4: // R | R
                        n.nodeType = NodeType.OR;
                        n.left = new Node(Int32.Parse(split[1]));
                        n.right = new Node(Int32.Parse(split[3]));
                        n.rule = ruleNumber;
                        rules.Add(ruleNumber, n);
                        break;
                    case 3: // R R
                        n.nodeType = NodeType.AND;
                        n.left = new Node(Int32.Parse(split[1]));
                        n.right = new Node(Int32.Parse(split[2]));
                        n.rule = ruleNumber;
                        rules.Add(ruleNumber, n);
                        break;
                    case 2: // "a" or R
                        if (split[1][0] == '"')
                        {
                            n.value = split[1][1];
                            n.rule = ruleNumber;
                            n.nodeType = NodeType.VALUE;
                        }
                        else
                        {
                            n.rule = Int32.Parse(split[1]);
                            n.nodeType = NodeType.UNINT;
                        }
                        rules[ruleNumber] = n;
                        break;
                }
            }
            var tree = rules[0];
            //8: 42 | 42 8 -> 42 42 42 42 | 42 42 42 | 42 42 | 42
            Node or8 = new Node(8);
            or8.nodeType = NodeType.OR;
            Node l8 = new Node(42);
            or8.left = l8;
            Node r8 = new Node();
            r8.nodeType = NodeType.AND;
            r8.left = new Node(42);
            r8.right = new Node(8);
            or8.right = r8;
            or8.rule = 8;
            rules[8] = or8;

            //11: 42 31 | 42 11 31 -> 42 42 42 31 31 31 | 42 42 31 31 | 42, 31
            Node root11 = new Node(11);
            root11.nodeType = NodeType.OR;
            Node l11 = new Node();
            l11.nodeType = NodeType.AND;
            l11.left = new Node(42);
            l11.right = new Node(31);
            root11.left = l11;
            Node r11 = new Node();
            r11.nodeType = NodeType.AND;
            r11.left = new Node(42);
            Node rr11 = new Node();
            rr11.nodeType = NodeType.AND;
            rr11.left = new Node(11);
            rr11.right = new Node(31);
            r11.right = rr11;
            root11.right = r11;
            rules[11] = root11;

            while ((line = sr.ReadLine()) != null)
            {
                var valid = Matches(tree, rules, 0, line).Where(i => i == line.Length).ToList(); //some of the matches will be partial, we only need 
                if (valid.Count > 0)
                {
                    counter++;
                }
            }
        }
        return counter;
    }
}
