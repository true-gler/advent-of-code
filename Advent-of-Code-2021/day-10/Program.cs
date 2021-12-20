using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day_10
{
    class Program
    {
        static void Main(string[] args)
        {

            var charSet = new List<char> { '{', '(', '[', '<' };

            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input.txt"));

            SolvePart1(charSet, input);
            SolvePart2(charSet, input);

        }

        private static void SolvePart2(List<char> charSet, string[] input)
        {
            var multiplicators = new List<(char c, int factor)> { (')', 1), (']', 2), ('}', 3), ('>', 4) };
            Stack syntaxStack = new Stack();
            List<long> sumList = new List<long>();
            bool corrupted = false;

            foreach (var item in input)
            {
                syntaxStack.Clear();
                corrupted = false;
                foreach (var current in item)
                {
                    if (charSet.Any(x => x == current))
                    {
                        syntaxStack.Push(current);
                        continue;
                    }

                    char last = (char)syntaxStack.Peek();

                    if (last == current - (char)2 || last == current - (char)1)
                    {
                        syntaxStack.Pop();
                    }
                    else
                    {
                        corrupted = true;
                    }
                }

                if (!corrupted)
                {
                    long sum = 0;
                    foreach (char elem in syntaxStack)
                    {
                        var c = GetOppositeCharacter(elem);
                        sum *= 5;
                        sum += multiplicators.Single(x => x.c == c).factor;
                    }
                    sumList.Add(sum);
                }
            }

            sumList.Sort();
            sumList.Reverse();
            long mid = sumList[sumList.Count / 2];

            Console.WriteLine($"Total syntax errors {mid}");
        }

        private static void SolvePart1(List<char> charSet, string[] input)
        {
            var multiplicators = new List<(char c, int factor)> { (')', 3), (']', 57), ('}', 1197), ('>', 25137) };
            Stack syntaxStack = new Stack();
            var sum = 0;

            foreach (var item in input)
            {
                syntaxStack.Clear();
                foreach (var current in item)
                {
                    if (charSet.Any(x => x == current))
                    {
                        syntaxStack.Push(current);
                        continue;
                    }

                    char last = (char)syntaxStack.Peek();

                    if (last == current - (char)2 || last == current - (char)1)
                    {
                        syntaxStack.Pop();
                    }
                    else
                    {
                        char opposite = GetOppositeCharacter(last);

                        Console.WriteLine($"Expected {opposite}, but found {current} instead.");
                        var score = multiplicators.Single(x => x.c == current).factor;
                        sum += score;
                        break;
                    }
                }

            }
            Console.WriteLine($"Total syntax errors {sum}");
        }

        private static char GetOppositeCharacter(char last)
        {
            char opposite = ' ';
            if (last == '(')
                opposite = ')';
            else
                opposite = (char)(last + 2);
            return opposite;
        }
    }
}
