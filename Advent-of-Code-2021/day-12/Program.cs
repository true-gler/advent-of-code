using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day_12
{
    class Cave
    {
        public string Name { get; set; }
        public bool Visited { get; set; }

        public Cave(string name, bool visited)
        {
            this.Name = name;
            this.Visited = visited;
        }
    }
    class Program
    {
        public static List<string> cavesystem = new();
        public static Stack<string> str = new();
        static void Main(string[] args)
        {

            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testinput.txt"));

            List<(string from, string to)> paths = new();
            List<Cave> caves = new();
            foreach (var item in input)
            {
                var entry = item.Split('-');
                paths.Add((entry[0], entry[1]));

                if (entry[0] != "start" && entry[1] != "end")
                {
                    paths.Add((entry[1], entry[0]));
                }

                if (!caves.Any(x => x.Name == entry[0]))
                    caves.Add(new Cave(entry[0], false));
                if (!caves.Any(x => x.Name == entry[1]))
                    caves.Add(new Cave(entry[1], false));
            }
            CalculatePaths(paths, caves);
        }

        private static void CalculatePaths(List<(string from, string to)> paths, List<Cave> caves)
        {

            
            MakeStep(paths, caves, "start");                           

        }

        private static string MakeStep(List<(string from, string to)> paths, List<Cave> caves, string to)
        {
   
            var currentCave = caves.First(x => x.Name == to);

            if (!currentCave.Name.Any(char.IsUpper))
            {
                currentCave.Visited = true;
            }

            str.Push(to);

            if (to == "end")
            {
                caves.ForEach(x => x.Visited = false);               
                str.Pop();
                foreach (var item in str)
                {
                    Console.Write(item);
                }
                Console.WriteLine();
            }

            var reachableCaves = paths.Where(x => x.from == to && !caves.Any(y => y.Name == x.to && y.Visited == true)).ToList();

            foreach (var item in reachableCaves)
            {                
                var s = MakeStep(paths, caves, item.to) + item.to + ",";
               
            }
            str.Pop();
            return "";
        }
    }
}
