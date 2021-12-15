using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day_6
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input.txt"));

            //List<int> lanternFish = new List<int>(){ 3, 4, 3, 1, 2 };
            List<int> lanternFish = new(Array.ConvertAll(input[0].Split(','), Convert.ToInt32));

            Part1(lanternFish);            
            Part2(lanternFish);
        }
        private static void Part1(List<int> lanternFish)
        {
            var l = new List<int>(lanternFish);

            Console.Write($"Initial State: ");
            l.ForEach(x => Console.Write($"{x},"));
            Console.WriteLine();

            int amount = l.Count;
            for (int i = 0; i < 80; i++)
            {
                for (int j = 0; j < amount; j++)
                {
                    if (l[j] == 0)
                    {
                        l.Add(8);
                    }

                    _ = l[j] == 0 ? l[j] = 6 : l[j] = l[j] - 1;
                }
                amount = l.Count;

                // Console.Write($"After {i+1} days: ");
                // l.ForEach(x => Console.Write($"{x},"));
                // Console.WriteLine();
            }

            Console.WriteLine("Lanternfish:" + l.Count);
        }

        private static void Part2(List<int> lanternFish)
        {                      
            Console.Write($"Initial State: ");
            lanternFish.ForEach(x => Console.Write($"{x},"));
            Console.WriteLine();

            long[] createdEachDay = new long[9];

            //example
            //after   1  2  3  4  5  6  7  8             
            //day x { 0, 1, 1, 2, 1, 0, 0, 0 }; initial at 3,4,3,1,2
            //day 0 { 1, 1, 2, 1, 0, 0, 0, 0 };
            //day 1 { 1, 2, 1, 0, 0, 1, 0, 1 }; +1 bei 8, +1 bei 6
            //day 2 { 2, 1, 0, 0, 1, 1, 1, 1 }: +1 8, +1 6
            //day 3 { 1, 0, 0, 1, 1, 3, 1, 2 }: +2 8, +2 6

            //PreFill init State
            for (int i = 0; i < lanternFish.Count; i++)
            {
                createdEachDay[lanternFish[i]] += 1;
            }            

            for (int j = 0; j < 256; j++)
            {
                long temp = createdEachDay[0];
                
                LeftShift(createdEachDay);           

                createdEachDay[6] += temp; //6
                createdEachDay[8] += temp; //8
                Console.WriteLine("day: " + j + "| " + String.Join(",", createdEachDay));
            }

            Console.WriteLine("Amount of Lanternfish:" + createdEachDay.Sum());
        }        

        private static void LeftShift(long[] createdEachDay)
        {
            for (int i = 0; i < createdEachDay.Length-1; i++)
            {               
                createdEachDay[i] = createdEachDay[i + 1];
            }
            createdEachDay[createdEachDay.Length - 1] = 0;
                            
        }
    }
}
