using System;
using System.IO;

namespace day_1
{
    class Program
    {
        public static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input.txt"));
            int[] numbers = Array.ConvertAll(input, x => Int32.Parse(x));
         
            var res = CheckPart1(0, numbers);
            Console.WriteLine(res);

            var res2 = CheckPart2(0, numbers);
            Console.WriteLine(res2);
        }

        public static int CheckPart1(int i, int[] numbers)
        {
            if (i == numbers.Length-1) 
                return 0;

            return ((numbers[i] < numbers[i + 1]) ? 1 : 0) + CheckPart1(i + 1, numbers);          
        }
        public static int CheckPart2(int i, int[] numbers)
        {
            if (i == numbers.Length - 3)
                return 0;

            var firstMeasurement = numbers[i] + numbers[i + 1] + numbers[i + 2];
            var secondMeasurement = numbers[i + 1] + numbers[i + 2] + numbers[i + 3];

            return (firstMeasurement < secondMeasurement ? 1 : 0) + CheckPart2(i + 1, numbers);
        }
    }
}
