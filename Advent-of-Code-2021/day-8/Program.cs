using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day_8
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input.txt"));

            List<Tuple<List<string>, List<string>>> list = new();
            List<string> o = new();
            for (int i = 0; i < input.Length; i++)
            {
                var str = input[i].Split('|').Where(x => !string.IsNullOrEmpty(x)).ToArray();
                var display = new List<string>(str[0].Split(' ').Where(x => !string.IsNullOrEmpty(x)).ToArray());
                var output = new List<string>(str[1].Split(' ').Where(x => !string.IsNullOrEmpty(x)).ToArray());
                //Part 1
                foreach (var item in output)
                {
                    if (item.Length == 4 || item.Length == 2 || item.Length == 7 || item.Length == 3)
                    {
                        o.Add(item);
                    }
                    Console.WriteLine(o.Count());
                }

                list.Add(Tuple.Create(display, output));
            }

            //Part 2

            var sum = 0;

            foreach (var item in list)
            {
                var digits = new HashSet<char>[10];
                var pattern = item.Item1.Select(x => x.ToHashSet()).ToArray();

                digits[1] = pattern.First(x => x.Count() == 2);
                digits[4] = pattern.First(x => x.Count() == 4);
                digits[7] = pattern.First(x => x.Count() == 3);
                digits[8] = pattern.First(x => x.Count() == 7);

                digits[0] = IdentifyPattern(pattern, digits, 6, 2, 3);
                digits[2] = IdentifyPattern(pattern, digits, 5, 1, 2);
                digits[3] = IdentifyPattern(pattern, digits, 5, 2, 3);
                digits[5] = IdentifyPattern(pattern, digits, 5, 1, 3);
                digits[6] = IdentifyPattern(pattern, digits, 6, 1, 3);
                digits[9] = IdentifyPattern(pattern, digits, 6, 2, 4);


                var output = item.Item2;

                var result = 0;                
                for (int i = 0; i < output.Count(); i++)
                {
                    for (int j = 0; j < digits.Length; j++)
                    {                      
                        if (digits[j].SetEquals(output[i]))
                        {
                            result += j * (int)Math.Pow(10, (output.Count() - i -1));                                              
                        }
                    }                 
                }                
                sum += result;

            }
            Console.WriteLine(sum);
        }


        //Unique is the common counts with one and four
        private static HashSet<char> IdentifyPattern(HashSet<char>[] pattern, HashSet<char>[] digits, int count, int commonWithOne, int commonWithFour)
        {
            return pattern.First(x =>
               x.Count() == count &&
               x.Intersect(digits[1]).Count() == commonWithOne &&
               x.Intersect(digits[4]).Count() == commonWithFour
           );

        }
    }
}
