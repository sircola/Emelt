using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SZTF17het
{
    class Program
    {
        static void CalculateWordsScores(string[,] words)
        {
            for (int i = 0; i < words.GetLength(0); i++)
            {
                words[i, 1] = CalculateWordScore(words[i, 0]).ToString();
            }
        }

        static int CalculateWordScore(string s)
        {
            int érték = 0;
            foreach (char c in s)
                érték += GetCharScore(c);

            return érték;
        }

        static int GetCharScore(char c)
        {
            if ("aeiounrtls".Contains(c))
                return 1;

            if ("áéíóöőúüű".Contains(c))
                return 2;

            if ("dg".Contains(c))
                return 3;

            if ("bcmp".Contains(c))
                return 5;

            if ("fhv".Contains(c))
                return 7;

            if ("kjz".Contains(c))
                return 8;

            if ("qwxy".Contains(c))
                return 10;

            return 0;
        }

        static void Print(string[,] words)
        {
            Console.WriteLine("Szavak\tÉrtékük");
            for (int i = 0; i < words.GetLength(0); i++)
            {
                Console.WriteLine($"{words[i, 0]}\t{words[i, 1]}");
            }
        }

        static void ReplaceWord(string[,] words, string from, string to)
        {
            for (int i = 0; i < words.GetLength(0); i++)
            {
                if (words[i, 0].Equals(from))
                {
                    words[i, 0] = to;
                    words[i, 1] = CalculateWordScore(to).ToString();
                }
            }
        }

        static void SortWordsByScore(string[,] words)
        {
            for (int i = 0; i < words.GetLength(0) - 1; i++)
            {
                for (int j = i + 1; j < words.GetLength(0); j++)
                {
                    if (int.Parse(words[i, 1]) < int.Parse(words[j, 1]))
                    {
                        string s = words[i, 0];
                        words[i, 0] = words[j, 0];
                        words[j, 0] = s;
                        s = words[i, 1];
                        words[i, 1] = words[j, 1];
                        words[j, 1] = s;
                    }
                }
            }
        }

        static string FindThirdGreatest(string[,] words)
        {

            return "";
        }

        static string GetGreaterThanMeaningOfLife(string[,] words)
        {
            return "";
        }

        static int CalculateGameScore(string[,] words)
        {
            return 0;
        }

        static int[,] GetMostFrequentChar(string[,] words)
        {
            return null;
        }


        static void Main(string[] args)
        {
            Console.Write("Szavak száma? ");
            int szavakszáma = int.Parse(Console.ReadLine());

            string[,] szavak = new string[szavakszáma, 2];

            for (int i = 0; i < szavak.GetLength(0); i++)
            {
                Console.Write($"Kérem az {i + 1}. szót: ");
                szavak[i, 0] = Console.ReadLine();
            }

            CalculateWordsScores(szavak);
            Print(szavak);

            SortWordsByScore(szavak);
            Print(szavak);

            Console.ReadLine();

            /*
            hello
            world
            szia
            világ
            */
            /*
            Console.WriteLine("\U0010FADE".Length);
            Console.WriteLine("\U0000FADE".Length);
            */
        }
    }
}
