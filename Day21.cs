using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2020
{
    class Day21
    {
        public static int Part1()
        {

            Dictionary<string, List<HashSet<string>>> map = new Dictionary<string, List<HashSet<string>>>();
            HashSet<string> uniqueIngredients = new HashSet<string>();
            Dictionary<string, int> number = new Dictionary<string, int>();
            using (Stream stream = File.Open(@"day21", FileMode.Open))
            using (TextReader sr = new StreamReader(stream, Encoding.UTF8))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var parts = line.Split(" ");
                    bool containsFound = false;
                    HashSet<string> ingredients = new HashSet<string>();
                    List<string> allergens = new List<string>();
                    foreach(var part in parts)
                    {
                        if (part == "(contains")
                        {
                            containsFound = true;
                            continue;
                        }
                        if (containsFound)
                            allergens.Add(part.Trim(',').Trim(')'));
                        else
                        {
                            ingredients.Add(part);
                            if (number.ContainsKey(part))
                                number[part]++;
                            else
                                number[part] = 1;
                        }
                    }
                    foreach(var allergen in allergens)
                    {
                        if(map.ContainsKey(allergen))
                        {
                            map[allergen].Add(ingredients);
                        } else
                        {
                            map.Add(allergen, new List<HashSet<string>> { ingredients });
                        }
                    }
                    uniqueIngredients.UnionWith(ingredients);
                }
            }
            Dictionary<string, HashSet<string>> final = new Dictionary<string, HashSet<string>>();
            foreach (var (allergen, ingredientsList) in map)
            {
                HashSet<string> ingredients = ingredientsList[0];
                foreach (var list in ingredientsList)
                    ingredients = ingredients.Intersect(list).ToHashSet();
                final.Add(allergen, ingredients);
            }

            foreach (var (_, list) in final)
                foreach(var ing in list)
                    number.Remove(ing);

            

            return number.Select(x => x.Value).Sum();
        }
        public static string Part2()
        {

            Dictionary<string, List<HashSet<string>>> map = new Dictionary<string, List<HashSet<string>>>();
            HashSet<string> uniqueIngredients = new HashSet<string>();
            Dictionary<string, int> number = new Dictionary<string, int>();
            using (Stream stream = File.Open(@"day21", FileMode.Open))
            using (TextReader sr = new StreamReader(stream, Encoding.UTF8))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var parts = line.Split(" ");
                    bool containsFound = false;
                    HashSet<string> ingredients = new HashSet<string>();
                    List<string> allergens = new List<string>();
                    foreach (var part in parts)
                    {
                        if (part == "(contains")
                        {
                            containsFound = true;
                            continue;
                        }
                        if (containsFound)
                            allergens.Add(part.Trim(',').Trim(')'));
                        else
                        {
                            ingredients.Add(part);
                            if (number.ContainsKey(part))
                                number[part]++;
                            else
                                number[part] = 1;
                        }
                    }
                    foreach (var allergen in allergens)
                    {
                        if (map.ContainsKey(allergen))
                        {
                            map[allergen].Add(ingredients);
                        }
                        else
                        {
                            map.Add(allergen, new List<HashSet<string>> { ingredients });
                        }
                    }
                    uniqueIngredients.UnionWith(ingredients);
                }
            }
            Dictionary<string, HashSet<string>> final = new Dictionary<string, HashSet<string>>();
            foreach (var (allergen, ingredientsList) in map)
            {
                HashSet<string> ingredients = ingredientsList[0];
                foreach (var list in ingredientsList)
                    ingredients = ingredients.Intersect(list).ToHashSet();
                final.Add(allergen, ingredients);
            }

            while(final.Select(x => x.Value.Count).Sum() > final.Count)
            {
                foreach (var (allergen, ingredients) in final.Where(x => x.Value.Count == 1))
                    foreach (var (other, otherList) in final.Where(x => x.Value.Count > 1))
                        otherList.ExceptWith(ingredients);
            }

            return string.Join(',', final.ToList().OrderBy(s => s.Key).Select(x => x.Value.ToList()[0]));
        }

    }
}
