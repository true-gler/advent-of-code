using day_11;
using System;
using System.Collections.Generic;
using System.IO;

namespace day_11
{
    class Octopus
    {
        public int Value { get; set; }
        public bool Flashed { get; set; }

        public Octopus(int value, bool flashed)
        {
            this.Value = value;
            this.Flashed = flashed;
        }
    }
    class Program
    {

        private static int sum = 0;
        private static int step = 1;
        private static bool synchronized = false; 
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input.txt"));

            List<List<Octopus>> terrain = new();

            foreach (string line in input)
            {
                int[] inputs = (Array.ConvertAll(line.ToCharArray(), s => Int32.Parse(s.ToString())));

                var a = new List<Octopus>();
                terrain.Add(a);
                foreach (var value in inputs)
                {
                    a.Add(new Octopus(value, false));
                }

            }

            Console.WriteLine("Before any steps");
            PrintTerrain(terrain);
            SimulatePart1(terrain);
            SimulatePart2(terrain);

        }

        private static void SimulatePart1(List<List<Octopus>> terrain)
        {
            while (step <= 100)
            {
                IncreaseEnergyLevel(terrain); //Step 1

                for (int j = 0; j < terrain.Count; j++)
                {
                    for (int i = 0; i < terrain[j].Count; i++)
                    {
                        if (terrain[j][i].Value > 9 && !terrain[j][i].Flashed)
                        {                            
                            terrain[j][i].Value = 0;
                            terrain[j][i].Flashed = true;
                            Flash(terrain, j, i);
                            sum += 1;
                        }
                    }
                }

                Console.WriteLine($"After Step {step}");
                PrintTerrain(terrain);
                step++;
            }
            Console.WriteLine(sum);
        }

        private static void SimulatePart2(List<List<Octopus>> terrain)
        {
            while (!synchronized)
            {
                IncreaseEnergyLevel(terrain); //Step 1

                for (int j = 0; j < terrain.Count; j++)
                {
                    for (int i = 0; i < terrain[j].Count; i++)
                    {
                        if (terrain[j][i].Value > 9 && !terrain[j][i].Flashed)
                        {
                            terrain[j][i].Value = 0;
                            terrain[j][i].Flashed = true;
                            Flash(terrain, j, i);
                            sum += 1;
                        }
                    }
                }

                Console.WriteLine($"After Step {step}");
                PrintTerrain(terrain);
                step++;
            }            
        }

        private static void IncreaseEnergyLevel(List<List<Octopus>> terrain)
        {
            for (int j = 0; j < terrain.Count; j++)
            {
                for (int i = 0; i < terrain[j].Count; i++)
                {
                    terrain[j][i].Value += 1;
                    terrain[j][i].Flashed = false;
                }
            }

        }
        private static void Flash(List<List<Octopus>> terrain, int yp, int xp)
        {
            for (int y = -1; y <= 1; y++)
            {
                for (int x = -1; x <= 1; x++)
                {
                    var yaxis = yp + y;
                    var xaxis = xp + x;
                    if (yaxis >= terrain.Count || xaxis >= terrain[0].Count || yaxis < 0 || xaxis < 0)
                    {
                        continue;
                    }
                    var a = terrain[yaxis][xaxis];
                    if(!a.Flashed)
                        a.Value += 1;

                    if (a.Value > 9 && !a.Flashed)
                    {
                        a.Flashed = true;
                        a.Value = 0;
                        Flash(terrain, yaxis, xaxis);
                        sum += 1;
                    }
                }

            }
        }

        private static void PrintTerrain(List<List<Octopus>> terrain)
        {
            synchronized = true;
            for (int j = 0; j < terrain.Count; j++)
            {
                for (int i = 0; i < terrain[j].Count; i++)
                {
                    if (terrain[j][i].Value == 0)
                        Console.ForegroundColor = ConsoleColor.Blue;
                    else
                        synchronized = false;

                    Console.Write(terrain[j][i].Value);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine();
            }
            Console.WriteLine();           
        }
    }
}
