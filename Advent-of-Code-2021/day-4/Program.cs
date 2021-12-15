using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day_4
{


    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input.txt"));

            int[] drawnNumbers = Array.ConvertAll(input[0].Split(','), Convert.ToInt32);

            input = input.Skip(2).ToArray();

            var str = input[0].Split(' ').Where(x => !string.IsNullOrEmpty(x)).ToArray();
            var length = Array.ConvertAll(str, Convert.ToInt32).Length;

            var matrixCount = (input.Length + 1) / (length + 1);
            Tuple<int, bool>[,,] data = new Tuple<int, bool>[matrixCount, length, length];

            var lineNumber = 0;

            //Prefill
            for (int k = 0; k < matrixCount; k++)
            {
                for (int i = 0; i < length; i++)
                {
                    var s = input[lineNumber].Split(' ').Where(x => !string.IsNullOrEmpty(x)).ToArray();
                    var line = Array.ConvertAll(s, Convert.ToInt32);
                    lineNumber++;

                    if (line.Length == 0) //don't judge me, i know this is dirty
                    {
                        s = input[lineNumber].Split(' ').Where(x => !string.IsNullOrEmpty(x)).ToArray();
                        line = Array.ConvertAll(s, Convert.ToInt32);
                        lineNumber++;
                    }

                    for (int j = 0; j < length; j++)
                    {
                        data[k, i, j] = new Tuple<int, bool>(Convert.ToInt32(line[j]), false);
                    }
                }
            }

            PlayGame(drawnNumbers, length, matrixCount, data);
            PlayGameAndLetItWin(drawnNumbers, length, matrixCount, data);

        }

        private static void PlayGameAndLetItWin(int[] drawnNumbers, int length, int matrixCount, Tuple<int, bool>[,,] data)
        {
            
            List<int> matrixWon = new List<int>();
            List<int> maxDraw = new List<int>();

            foreach (var draw in drawnNumbers)
            {
                for (int matrix = 0; matrix < matrixCount; matrix++)
                {
                    if (matrixWon.Contains(matrix)){
                        continue;
                    }

                    for (int row = 0; row < length; row++)
                    {
                        for (int col = 0; col < length; col++)
                        {
                            if (data[matrix, row, col].Item1 == draw)
                            {

                                data[matrix, row, col] = new Tuple<int, bool>(data[matrix, row, col].Item1, true);
                                if (CheckRow(data, matrix, row, length) || CheckCol(data, matrix, col, length))
                                {
                                    matrixWon.Add(matrix);
                                    maxDraw.Add(draw);
                                }
                            }
                        }
                    }

                }

            }

            var mWon = matrixWon.Select(x => x).Last();
            var mDraw = maxDraw.Select(x => x).Last();

            Console.WriteLine(CalculateSum(data, mWon, length) * mDraw);
        }

        private static void PlayGame(int[] drawnNumbers, int length, int matrixCount, Tuple<int, bool>[,,] data)
        {
            foreach (var draw in drawnNumbers)
            {
                for (int matrix = 0; matrix < matrixCount; matrix++)
                {
                    for (int row = 0; row < length; row++)
                    {
                        for (int col = 0; col < length; col++)
                        {
                            if (data[matrix, row, col].Item1 == draw)
                            {

                                data[matrix, row, col] = new Tuple<int, bool>(data[matrix, row, col].Item1, true);
                                if (CheckRow(data, matrix, row, length) || CheckCol(data, matrix, col, length))
                                {
                                    Console.WriteLine(CalculateSum(data, matrix, length) * draw);
                                    return;
                                }
                            }
                        }
                    }

                }

            }
        }

        private static bool CheckCol(Tuple<int, bool>[,,] data, int matrix, int col, int length)
        {
            for (int i = 0; i < length; i++)
            {
                if (!data[matrix, i, col].Item2)
                {
                    return false;
                }
            }
            return true;
        }

        private static bool CheckRow(Tuple<int, bool>[,,] data, int matrix, int row, int length)
        {
            for (int j = 0; j < length; j++)
            {
                if (!data[matrix, row, j].Item2)
                {
                    return false;
                }
            }
            return true;
        }


        private static int CalculateSum(Tuple<int, bool>[,,] data, int matrix, int length)
        {
            var sum = 0;
            for (int row = 0; row < length; row++)
            {
                for (int col = 0; col < length; col++)
                {
                    if (!data[matrix, row, col].Item2)
                    {
                        sum += data[matrix, row, col].Item1;
                    }

                }
            }
            return sum;
        }
    }
}
