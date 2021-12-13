using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day_2
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input.txt"));

            ILookup<string, int> lookup =
                      input.ToLookup(s => s.Split(' ')[0], s => Int32.Parse(s.Split(' ')[1]));

            //part 1 
            int y = lookup.Where(x => x.Key == "forward").SelectMany(x => x).Sum();
            int x = lookup.Where(x => x.Key == "down").SelectMany(x => x).Sum()
                - lookup.Where(x => x.Key == "up").SelectMany(x => x).Sum();

            Console.WriteLine(x * y);

            //part 2

            long x2 = 0, y2 = 0, aim = 0;
            int value = 0;
            var data = input.Select(x => x.Split(' ')).ToArray();

            foreach (var row in data)
            {                
                value = Int32.Parse(row[1]);
                switch (row[0])
                {
                    case "up":
                        aim -= value;
                        break;
                    case "down":
                        aim += value;
                        break;
                    case "forward":
                        x2 += value;
                        y2 += value * aim;
                        break;
                    default:
                        break;
                }
            }

            Console.WriteLine(x2 * y2);
        }
    }
}