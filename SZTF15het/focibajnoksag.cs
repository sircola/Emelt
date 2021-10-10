using System;

namespace Ora5
{
    // Készítsen programot, amely egy kispályás futball bajnokság ponttábláját kezeli. A bajnokságon 6 csapat vesz részt.
    // A csapatok neveit a csapatok tömbben tároljuk, ezek elõre adottak a programban. A bajnokság ponttáblája az eredmenyek tömbben van.
    // Az eredmenyek[i, j] értéke
    //  - 0, ha az i-edik csapat otthon kikapott a j-edik csapattól,
    //  - 1, ha a két csapat az i-edik csapat otthonában döntetlent játszott,
    //  - 3, ha az i-edik csapat otthon legyõzte a j-edik csapatot.

    class Focibajnoksag
    {
        static Random rnd = new Random();

        static void Main(string[] args)
        {
            int[,] eredmények = new int[6, 6];
            string[] csapatok = { "A", "B", "C", "D", "E", "F" };

            FociFeltoltes(eredmények);
            Console.WriteLine(FociToString(eredmények, csapatok));

            if (FocicsapatJatszottE(csapatok, "C"))
                Console.WriteLine("A 'C' csapat játszott a bajnokságban!");
            else
                Console.WriteLine("A 'C' csapat nem játszott a bajnokságban!");

            Console.WriteLine($"A 'C' csapat pontszáma: {CsapatPontszam(eredmények, csapatok, "C")}");

            int[] pontszámok = CsapatonkentiPontszam(eredmények, csapatok);
            for (int i = 0; i < csapatok.Length; i++)
            {
                Console.WriteLine($"A '{csapatok[i]}' {pontszámok[i]} pontot szerzett!");
            }

            Console.WriteLine($"A nyertes csapat: {csapatok[BajnoksagNyertes(pontszámok)]}");

            Console.ReadLine();
        }

        // a) Írjon metódust, amely az eredmenyek tömb elemeit feltölti véletlen értékekkel. Természetesen eredmenyek[i, i] minden esetben 0 lesz...
        static void FociFeltoltes(int[,] eredmények)
        {
            for (int i = 0; i < eredmények.GetLength(0); i++)
            {
                for (int j = 0; j < eredmények.GetLength(1); j++)
                {
                    // .. ha tudjuk, hogy milyen értékekkel incializálódik a tömb, akkor itt a 0 értékadása felesleges
                    //if (i == j)
                    //{
                    //    eredmények[i, j] = 0;
                    //}
                    if (i != j) // else
                    {
                        int r = rnd.Next(100); // ha a feladat azt mondja, hogy hány százalék eséllyel veszi fel a tömb adott eleme az értéket
                        //if (r == 0) eredmények[i, j] = 0; // 20%
                        if (r < 30) eredmények[i, j] = 1; // 30%
                        else if (r < 80) eredmények[i, j] = 3; // 50%
                    }
                }
            }
        }

        // b) Írjon metódust, amely elõállítja az eredményeket táblázatos formában.
        static string FociToString(int[,] eredmények, string[] csapatok)
        {
            string s = "  ";

            for (int i = 0; i < csapatok.Length; i++)
                s += $"{csapatok[i]} ";

            for (int i = 0; i < eredmények.GetLength(0); i++)
            {
                s += $"\n{csapatok[i]} ";
                for (int j = 0; j < eredmények.GetLength(1); j++)
                {
                    s += $"{eredmények[i, j]} ";
                }
            }

            return s;
        }

        // c) Írjon metódust, amellyel eldönthetõ, hogy egy adott nevû csapat indult-e a bajnokságban.
        static bool FocicsapatJatszottE(string[] csapatok, string csapat)
        {
            //int i = 0;
            //while (i < csapatok.Length && csapatok[i] != csapat)
            //{
            //    i++;
            //}
            //return i < csapatok.Length;

            for (int i = 0; i < csapatok.Length; i++) // ha a feladat nem várja el az eldöntés tételét, akkor más, de helyes megoldás is elfogadható
            {
                if (csapatok[i] == csapat)
                    return true;
            }
            return false;
        }

        // d) Írjon metódust, amely meghatározza, hogy egy adott csapat összesen hány pontot ért el a bajnokságban.
        //    (Ez az adott csapat eredmenyek tömbbeli sorának és oszlopának összegeként adható meg.)
        static int CsapatPontszam(int[,] eredmények, string[] csapatok, string csapat)
        {
            if (!FocicsapatJatszottE(csapatok, csapat)) // a metódus elején szokás vizsgálni azokat az eseteket amik a mûködése szempontjából hibát eredményeznének
                return -1; // olyan értékkel térjünk vissza, amibõl tudjuk, hogy 'hiba' történt

            int szum = 0;

            int csapatIDX = 0;
            while (csapatok[csapatIDX] != csapat)
                csapatIDX++;

            for (int i = 0; i < eredmények.GetLength(0); i++)
            {
                szum += eredmények[i, csapatIDX];
            }

            for (int j = 0; j < eredmények.GetLength(1); j++)
            {
                szum += eredmények[csapatIDX, j];
            }

            return szum;
        }

        // e) Írjon metódust, amely segítségével egy új tömbben elõállítható a bajnokság minden egyes csapatára, hogy összesen hány pontot szereztek.
        static int[] CsapatonkentiPontszam(int[,] eredmények, string[] csapatok)
        {
            int[] pontszámok = new int[csapatok.Length];
            for (int i = 0; i < csapatok.Length; i++)
                pontszámok[i] = CsapatPontszam(eredmények, csapatok, csapatok[i]); // ne készítsünk el funkciókat kétszer..használjuk a már létezõ metódusokat

            return pontszámok;
        }

        // f) Írjon metódust, amely megadja, hogy melyik csapat nyerte a bajnokságot.
        //    (Ha több csapat is maximális pontszámot szerzett, akkor közülük az elsõ csapatot hozza ki nyertesnek.)
        static int BajnoksagNyertes(int[] pontszamok)
        {
            int max = 0;
            for (int i = 0; i < pontszamok.Length; i++)
                if (pontszamok[max] < pontszamok[i])
                    max = i;
            return max;
        }
    }
}
