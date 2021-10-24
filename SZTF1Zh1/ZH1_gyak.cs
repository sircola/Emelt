using System;

namespace ZH1_Scrabble
{
    // A ZH nem értékelhetõ (0 pont), ha:
    //  - fordítás idejû hibát tartalmaz (kommentezett kódrészre pont nem szerezhetõ)
    //  - idõn túl kerül feltöltésre
    //  - nem a megfelelõ helyre kerül feltöltésre
    //  - rossz fájl(ok) kerülnek feltöltésre
    // A részfeladat 0 pont, ha:
    //  - futásidejû hibát eredményez helyes bemenet/paraméter esetén
    //  - Linq használata esetén (pl.: Array.Sort(..))

    // ZH-n általánosságban:
    //  - tömb (1d, 2d) létrehozás, feltöltés -> felhasználó alapján v random + extrák
    //  - tömb formázott megjelenítése
    //  - 'leg' - keresés, (max, min, =) kiválasztás, összegzés, megszámolás, stb..
    //  - tömb átalakítás (2d -> 1d, 1d -> 2d), "átméretezés", elem csere
    //  - rendezés
    //  - string -> char[] -> int

    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Hány darab szót szerene megadni? ");
            string[,] words = new string[int.Parse(Console.ReadLine()), 2];
            for (int i = 0; i < words.GetLength(0); i++) { Console.Write("Kérem a(z) " + i + ". szót: "); words[i, 0] = Console.ReadLine(); }

            Console.WriteLine(new string('-', 79));
            CalculateWordsScore(words);
            Print(words);

            Console.WriteLine(new string('-', 79));
            Console.Write("Szeretné lecserélni az egyik szót? (I/N) ");
            if (Console.ReadLine().ToUpper() == "I")
            {
                Console.Write("Melyik szót szeretné lecserélni? ");
                string from = Console.ReadLine();
                Console.Write("Milyen szóra szeretné lecserélni? ");
                string to = Console.ReadLine();
                ReplaceWord(words, from, to);
                Print(words);
            }

            Console.WriteLine(new string('-', 79));
            SortWordsByScore(words);
            Print(words);

            Console.WriteLine(new string('-', 79));
            Console.WriteLine("Harmadik legtöbb pontot érõ szavak:\t" + FindThirdGreatest(words));

            Console.WriteLine(new string('-', 79));
            Console.WriteLine("42-nél több pontot érõ szavak:\t\t" + GetGreaterThanMeanindOfLife(words));

            Console.WriteLine(new string('-', 79));
            Console.WriteLine("Szavak összpontszáma:\t\t\t" + CalculateGameScore(words));

            Console.WriteLine(new string('-', 79));
            int[,] most = GetMostFrequentChar(words);
            Console.WriteLine("Leggyakoribb karakter:\t\t\t" + (char)most[0, 0] + "=" + most[0, 1]);

            Console.ReadLine();
        }
        //1.
        static void CalculateWordsScore(string[,] words)
        {
            for (int i = 0; i < words.GetLength(0); i++)
                words[i, 1] = CalculateWordScore(words[i, 0]);
        }
        //2.
        static string CalculateWordScore(string s)
        {
            int score = 0;
            foreach (char c in s.ToLower())
                score += GetCharScore(c);

            return score.ToString();
        }
        //3.
        static int GetCharScore(char c)
        {
            if ("aeiounrtls".Contains(c.ToString())) return 1;
            if ("áéíóöõúüû".Contains(c.ToString())) return 2;
            if ("dg".Contains(c.ToString())) return 3;
            if ("bcmp".Contains(c.ToString())) return 5;
            if ("fhv".Contains(c.ToString())) return 7;
            if ("kjz".Contains(c.ToString())) return 8;
            if ("qwxy".Contains(c.ToString())) return 10;
            return 0;
        }
        //4.
        static void Print(string[,] words)
        {
            Console.WriteLine("Szavak\tÉrtékük");
            for (int i = 0; i < words.GetLength(0); i++)
                Console.WriteLine(words[i, 0] + "\t" + words[i, 1]);
        }
        //5.
        static void ReplaceWord(string[,] words, string from, string to)
        {
            for (int i = 0; i < words.GetLength(0); i++) // ha a szó többször is szerepel
            {
                if (words[i, 0] == from)
                {
                    words[i, 0] = to;
                    words[i, 1] = CalculateWordScore(to);
                }
            }
        }
        //6.
        static void SortWordsByScore(string[,] words)
        {
            // split into 1d array
            string[] dim1 = new string[words.GetLength(0)];
            string[] dim2 = new string[words.GetLength(0)];
            for (int i = 0; i < words.GetLength(0); i++)
            {
                dim1[i] = words[i, 0];
                dim2[i] = words[i, 1];
            }

            //sorting
            //Array.Sort(dim2, dim1);
            //Array.Reverse(dim2);
            //Array.Reverse(dim1);
            for (int i = 0; i < dim2.Length - 1; i++)
                for (int j = i + 1; j < dim2.Length; j++)
                    if (int.Parse(dim2[i]) < int.Parse(dim2[j])) // <, to reverse
                    {
                        //replace in both array
                        string temp = dim2[i];
                        dim2[i] = dim2[j];
                        dim2[j] = temp;

                        temp = dim1[i];
                        dim1[i] = dim1[j];
                        dim1[j] = temp;
                    }

            // override input 2d array
            for (int i = 0; i < words.GetLength(0); i++)
            {
                words[i, 0] = dim1[i];
                words[i, 1] = dim2[i];
            }
        }
        //7.
        static string FindThirdGreatest(string[,] words)
        {
            // rendezett tömbben!
            if (words.GetLength(0) < 3) return "-";

            int i = 0, greatest = 0, score = 0;
            string word = "", sep = "";
            while (i < words.GetLength(0) && greatest <= 3)
            {
                if (int.Parse(words[i, 1]) != score)
                {
                    score = int.Parse(words[i, 1]);
                    greatest++;
                }
                if (int.Parse(words[i, 1]) == score && greatest == 3)
                {
                    word += sep + words[i, 0];
                    sep = "; ";
                }
                i++;
            }

            if (word == "") word = "-";

            return word;
        }
        //8.
        static string GetGreaterThanMeanindOfLife(string[,] words)
        {
            int i = 0;
            string word = "", sep = "";
            while (i < words.GetLength(0) && int.Parse(words[i, 1]) > 42)
            {
                word += sep + words[i, 0];
                sep = "; ";
                i++;
            }

            if (word == "") word = "-";

            return word;
        }
        //9.
        static int CalculateGameScore(string[,] words)
        {
            int score = 0;
            for (int i = 0; i < words.GetLength(0); i++)
                score += int.Parse(words[i, 1]);

            return score;
        }
        //10.
        static int[,] GetMostFrequentChar(string[,] words)
        {
            int[] freq = new int[512];
            for (int i = 0; i < words.GetLength(0); i++)
                for (int j = 0; j < words[i, 0].Length; j++)
                    freq[words[i, 0][j]]++;

            int v = 0;
            for (int i = 0; i < freq.Length; i++)
                if (freq[i] > freq[v])
                    v = i;

            return new int[,] { { v, freq[v] } };
        }
    }

}
