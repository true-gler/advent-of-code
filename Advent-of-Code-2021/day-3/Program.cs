using System;
using System.IO;

namespace day_3
{
    class Program
    {
        static void Main(string[] args)
        {

            //Convert.ToString(c, toBase: 2)
            string[] arr = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input.txt"));

            int[] counts = new int[arr[0].Length];
            for (int i = 0; i < arr.Length-1; i++)
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
                if (counts[i] > 0)
                {
                    gammarateArray[i] = '1';
                    epsilonrateArray[i] = '0';
                }
                else {
                    gammarateArray[i] = '0';
                    epsilonrateArray[i] = '1';
                } 
            }
            
            var gammarate = Convert.ToInt64(new string(gammarateArray), 2);
            var epsilonrate = Convert.ToInt64(new string(epsilonrateArray),2);            

            Console.WriteLine((int)gammarate * epsilonrate);

        }
    }
}
