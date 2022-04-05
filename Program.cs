using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Anagram
{
    class Program
    {
        private const string letters = "abcdef"; // <--- letters to use in search
        private const int maxLevel = 30;                // <--- maximum length of words to find
        private const int minLevel = 2;
        private const string wordFile = @"C:\Users\Geoff\source\repos\Anagram\Words.txt";
        private static readonly int length = letters.Length;
        private static HashSet<string> dictionary;
        private static List<string> answers;
        static void Main(string[] args)
        {

            dictionary = new HashSet<string>(File.ReadAllLines(wordFile));
            answers = new List<string>();

            var sw = Stopwatch.StartNew();

            for (int i = 0; i < length; i++)
            {
                Try(new List<int>() { i });
            }

            sw.Stop();
            Console.WriteLine($"Elapsed time: {sw.ElapsedMilliseconds}");

            foreach (var word in answers) /*if (word.Length == 6)*/ Console.WriteLine(word);
        }

        static void Try(List<int> ar, int level = 0)
        {

            Display(ar);

            var word = Word(ar);

            //if (dictionary.Any(w => w == word)) Console.WriteLine(word);
            if (dictionary.Contains(word) && !answers.Contains(word)) answers.Add(word);

            var comp = Complement(ar);

            //Display(comp);
            if (level < maxLevel)
                foreach (var i in comp)
                {
                    var newArray = new List<int>(ar);
                    newArray.Add(i);
                    Try(newArray, level + 1);
                }
        }

        static void Try2(List<int> ar)
        {
            var stack = new Stack<List<int>>();
            stack.Push(ar);
            while (stack.Count > 0)
            {
                var integersInList = stack.Pop();

                Display(integersInList);

                var notInList = Complement(integersInList); 

                foreach(var i in notInList)
                {
                    var nextList = new List<int>(integersInList);
                    nextList.Add(i);
                    stack.Push(nextList);
                }

            }
        }

        static void Anagram()
        {
            var count = 0;

            Console.WriteLine($"{++count}:  {letters}");


            for (int i = letters.Length - 1; i >= 0; --i)
            {
                for (int j = i - 1; j >= 0; --j)
                {
                    var val = Swap(letters.ToCharArray(), j, i);
                    Console.WriteLine($"{++count}:  {val}");
                }
            }

        }

        static List<int> Complement(List<int> ar)
        {
            var list = new List<int>();
            for (int i = 0; i < length; i++) if (!ar.Any(e => e == i)) list.Add(i);
            return list;
        }

        static void Display(List<int> ar)
        {
            foreach (var i in ar) Console.Write($"{i} ");
            Console.WriteLine();
        }

        /// <summary>
        /// Convert the list of integers into a string using the 'letters' lookup
        /// </summary>
        static string Word(List<int> idx)
        {
            var word = new StringBuilder();
            foreach (var position in idx) word.Append(letters[position]);
            return word.ToString();
        }

        static string Swap(char[] ar, int p1, int p2)
        {
            char temp = ar[p1];
            ar[p1] = ar[p2];
            ar[p2] = temp;
            //var s = ar.ToString();
            return new string(ar);
        }
    }
}
