using System;
using System.IO;
using System.Linq;

namespace day_3
{
    class Program
    {
        private static bool oxygen= true;
        private static char bitCriteria = ' ';
        private static int ones = 0, zeros = 0;
        
        static void Main(string[] args)
        {


            string[] arr = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input.txt"));


            Part1(arr); //Naive approach
            Part2(arr); //more fancy

        }

        private static void Part1(string[] arr)
        {
            int[] counts = new int[arr[0].Length];
            for (int i = 0; i < arr.Length - 1; i++)
            {
                var numbers = arr[i].ToCharArray();
                for (int j = 0; j < arr[i].Length; j++)
                {
                    _ = numbers[j] == '1' ? counts[j]++ : counts[j]--;
                }

            }

            char[] gammarateArray = new char[arr[0].Length];
            char[] epsilonrateArray = new char[arr[0].Length];

            for (int i = 0; i < counts.Length; i++)
            {
                _ = counts[i] > 0 ? gammarateArray[i] = '1' : gammarateArray[i] = '0';
                if (counts[i] > 0)
                {
                    gammarateArray[i] = '1';
                    epsilonrateArray[i] = '0';
                }
                else
                {
                    gammarateArray[i] = '0';
                    epsilonrateArray[i] = '1';
                }
            }

            long gammarate = Convert.ToInt64(new string(gammarateArray), 2);
            long epsilonrate = Convert.ToInt64(new string(epsilonrateArray), 2);

            Console.WriteLine(gammarate * epsilonrate);
        }
        private static void Part2(string[] arr)
        {           
            var oxygenRating = Reduce(arr, 0);          
            long o = Convert.ToInt64(new string(oxygenRating), 2);

            oxygen = false;

            var srubberRating = Reduce(arr, 0);
            long c = Convert.ToInt64(new string(srubberRating), 2);

            Console.WriteLine(o * c);

        }

        private static string Reduce(string [] arr, int index)
        {
            if (arr.Length == 1) return arr[0];

            foreach (var item in arr)
            {
                _ = item[index] == '1' ? ones++ : zeros++;

                if (oxygen)
                    _ = ones >= zeros ? bitCriteria = '1' : bitCriteria = '0';
                else
                    _ = ones < zeros ? bitCriteria = '1' : bitCriteria = '0';
            }

            return Reduce(arr.Where(x => x[index] == bitCriteria).ToArray(), index+1);
         
        }

    }
}
