using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markov
{
    public class Program
    {
        public static Dictionary<string, List<string>> Chain { get; set; }

        public static Random Rand = new Random();

        public static void Main()
        {
            Chain = new Dictionary<string, List<string>>();

            List<string> Titles = Titles = File.ReadAllLines("data.txt").ToList();
            for (int i = 0; i < Titles.Count; i++)
            {
                Titles[i] = Titles[i].ToLower();
            }

            for (var i = 0; i < Titles.Count; i++)
            {
                string[] Words = Titles[i].Split(' ');
                for (var j = 0; j < Words.Count()-1; j++)
                {
                    if (Chain.ContainsKey(Words[j]))
                    {
                        Chain[Words[j]].Add(Words[j + 1]);
                    }
                    else
                    {
                        Chain[Words[j]] = new List<string> { Words[j + 1] };
                    }
                }
            }

            while (true)
            {
                Console.WriteLine(Generate(10));
                Console.ReadKey(true);
            }
        }

        public static string Generate(int MinLength, int MaxLength)
        {
            string Word = Chain.ElementAt(Rand.Next(Chain.Count)).Key;
            var Title = new List<string>() { Word };
            while (Chain.ContainsKey(Word))
            {
                var NextWords = Chain[Word];
                Word = NextWords[Rand.Next(NextWords.Count)];
                Title.Add(Word);

                if (Title.Count >= MaxLength) break;
            }
            if (Title.Count < MinLength) return Generate(MinLength, MaxLength);
            return string.Join(" ", Title);
        }

        public static string Generate(int MinLength)
        {
            string Word = Chain.ElementAt(Rand.Next(Chain.Count)).Key;
            var Title = new List<string>() { Word };
            while (Chain.ContainsKey(Word))
            {
                var NextWords = Chain[Word];
                Word = NextWords[Rand.Next(NextWords.Count)];
                Title.Add(Word);
            }
            if (Title.Count < MinLength) return Generate(MinLength);
            return string.Join(" ", Title);
        }

        public static string Generate()
        {
            string Word = Chain.ElementAt(Rand.Next(Chain.Count)).Key;
            var Title = new List<string>() { Word };
            while (Chain.ContainsKey(Word))
            {
                var NextWords = Chain[Word];
                Word = NextWords[Rand.Next(NextWords.Count)];
                Title.Add(Word);
            }
            return string.Join(" ", Title);
        }
    }
}