using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day_7
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input.txt"));

           // List<int> list = new List<int>(){ 16, 1, 2, 0, 4, 2, 7, 1, 2, 14 };
            List<int> list = new(Array.ConvertAll(input[0].Split(','), Convert.ToInt32));            

            //part 1
            var med = GetMedian(list.ToArray());

            var sum = list.Select(x => (int)(Math.Abs(x - med))).Sum();
            Console.WriteLine(sum);

            //part 2

            double a = list.Average(); // 488.507
            double avg = Math.Round(a); // Rounds to 489
            avg = 488; //but this is correct

            var sum2 = list.Select(x => GaussSum((int)Math.Abs(x - avg))).Sum();
            Console.WriteLine(sum2);
        }

        private static int GaussSum(int v)
        {
            return ((v * v ) + v) / 2;
        }

        public static double GetMedian(int[] sourceNumbers)
        {
            //make sure the list is sorted, but use a new array
            int[] sortedPNumbers = (int[])sourceNumbers.Clone();
            Array.Sort(sortedPNumbers);

            int size = sortedPNumbers.Length;
            int mid = size / 2;
            double median = (size % 2 != 0) ? (double)sortedPNumbers[mid] : ((double)sortedPNumbers[mid] + (double)sortedPNumbers[mid - 1]) / 2;
            return median;
        }
    }
}
