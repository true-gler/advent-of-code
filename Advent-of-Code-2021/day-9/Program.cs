using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace day_9
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input.txt"));

            List<int[]> terrain = new List<int[]>();
            foreach (string line in input)
            {
                int[] inputs = (Array.ConvertAll(line.ToCharArray(), s => Int32.Parse(s.ToString())));
                terrain.Add(inputs);
            }

            var field = terrain.ToArray();
            
            var lowPoints = new List<(int y, int x)>();

            var riskPoints = 0;
            int y = 0;

            foreach (var item in field)
            {
                for (int x = 0; x < item.GetLength(0); x++)
                {
                    var isLow = IsLowPoint(y, x, field);
                    if (isLow)
                    {
                        lowPoints.Add(new (y,x));
                        riskPoints += field[y][x] + 1;
                    }
                }
                y++;
            }
            Console.WriteLine(riskPoints);
        }

        private static bool IsLowPoint(int y, int x, int[][] field)
        {
            var point = field[y][x];

            if (y > 0 && point >= field[y - 1][x])
                return false;
            if (x > 0 && point >= field[y][x - 1])
                return false;
            if (y+1 < field.Length && point >= field[y + 1][x])
                return false;
            if (x+1 < field[0].Length && point >= field[y][x + 1])
                return false;

            return true;
        }
    }
}
