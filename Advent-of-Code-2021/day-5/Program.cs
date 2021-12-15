using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day_5
{
    class Program
    {
        public struct Line
        {
            public Line(int X1, int Y1, int X2, int Y2)
            {
                this.X1 = X1;
                this.Y1 = Y1;
                this.X2 = X2;
                this.Y2 = Y2;
            }

            public int X1 { get; set; }
            public int Y1 { get; set; }
            public int X2 { get; set; }
            public int Y2 { get; set; }
        }

        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input.txt"));
            var lines = new List<Line>();

            foreach (var item in input)
            {

                var splitted = item.Split("->");

                var firstCoordinate = splitted[0].Split(',');
                var secondCoordinate = splitted[1].Split(',');

                lines.Add(new Line(Int32.Parse(firstCoordinate[0]), Int32.Parse(firstCoordinate[1]), Int32.Parse(secondCoordinate[0]), Int32.Parse(secondCoordinate[1])));

            }

            int maxX = Math.Max(lines.Select(x => x.X1).Max(), lines.Select(x => x.X2).Max());
            int maxY = Math.Max(lines.Select(x => x.Y1).Max(), lines.Select(x => x.Y2).Max());


            int[,] result = new int[maxX + 1, maxY + 1];

            //part 1 -- naive
            foreach (var line in lines)
            {
                Console.WriteLine($"{line.X1} {line.Y1} -> {line.X2} {line.Y2}");
                ProcessPoints(line, result);
            }

            // PrintResult(result);

            int[,] result2 = new int[maxX + 1, maxY + 1];

            //part 2 
            foreach (var line in lines)
            {
                Console.WriteLine($"{line.X1} {line.Y1} -> {line.X2} {line.Y2}");
                ProcessPointsDiagonal(line, result2);
            }

            PrintResult(result2);


        }

        private static void ProcessPoints(Line line, int[,] result)
        {
            var length = Math.Abs(line.X1 - line.X2);
            var height = Math.Abs(line.Y1 - line.Y2);

          
            for (int i = 0; i <= length; i++)
            {
                for (int j = 0; j <= height; j++)
                {
                    if (line.X1 == line.X2)
                    {
                        if (line.Y1 > line.Y2)
                        {
                            result[line.X1 + i, line.Y1 - j] += 1;
                        }
                        if (line.Y1 < line.Y2)
                        {
                            result[line.X1 + i, line.Y1 + j] += 1;
                        }
                    }
                    if (line.Y1 == line.Y2)
                    {
                        if (line.X1 > line.X2)
                        {
                            result[line.X1 - i, line.Y1 + j] += 1;
                        }
                        if (line.X1 < line.X2)
                        {
                            result[line.X1 + i, line.Y1 + j] += 1;
                        }
                    }
                }
            }
        }

        private static void ProcessPointsDiagonal(Line line, int[,] result)
        {
            var x1 = line.X1;
            var x2 = line.X2;
            var y1 = line.Y1;
            var y2 = line.Y2;

            var dx = Math.Sign(x2 - x1);
            var dy = Math.Sign(y2 - y1);

            var x = x1;
            var y = y1;

            result[x, y] += 1;
            
            while (x != x2 || y != y2)
            {
                if (IsInRange(x, x1, x2))
                {
                    x += dx;
                }
                if (IsInRange(y, y1, y2))
                {
                    y += dy;
                }

                result[x, y] += 1;
            }
        }
                
        private static bool IsInRange(int val, int b1, int b2)
        {
            return b1 < b2 ? val < b2 : val > b2;
        }

        private static void PrintResult(int[,] result)
        {
            var overlap = 0;
            Console.Clear();
            for (int x = 0; x < result.GetLength(0); x++)
            {
                for (int y = 0; y < result.GetLength(1); y++)
                {
                    Console.Write(result[x, y] == 0 ? "." : result[x, y]);
                    if (result[x, y] >= 2)
                    {
                        overlap++;
                    }
                }
                Console.Write("\n");
            }
            Console.WriteLine(overlap);
        }


    }
}
