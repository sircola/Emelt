using System;

namespace Ora5
{
    // K�sz�tsen programot, amely egy kisp�ly�s futball bajnoks�g pontt�bl�j�t kezeli. A bajnoks�gon 6 csapat vesz r�szt.
    // A csapatok neveit a csapatok t�mbben t�roljuk, ezek el�re adottak a programban. A bajnoks�g pontt�bl�ja az eredmenyek t�mbben van.
    // Az eredmenyek[i, j] �rt�ke
    //  - 0, ha az i-edik csapat otthon kikapott a j-edik csapatt�l,
    //  - 1, ha a k�t csapat az i-edik csapat otthon�ban d�ntetlent j�tszott,
    //  - 3, ha az i-edik csapat otthon legy�zte a j-edik csapatot.

    class Focibajnoksag
    {
        static Random rnd = new Random();

        static void Main(string[] args)
        {
            int[,] eredm�nyek = new int[6, 6];
            string[] csapatok = { "A", "B", "C", "D", "E", "F" };

            FociFeltoltes(eredm�nyek);
            Console.WriteLine(FociToString(eredm�nyek, csapatok));

            if (FocicsapatJatszottE(csapatok, "C"))
                Console.WriteLine("A 'C' csapat j�tszott a bajnoks�gban!");
            else
                Console.WriteLine("A 'C' csapat nem j�tszott a bajnoks�gban!");

            Console.WriteLine($"A 'C' csapat pontsz�ma: {CsapatPontszam(eredm�nyek, csapatok, "C")}");

            int[] pontsz�mok = CsapatonkentiPontszam(eredm�nyek, csapatok);
            for (int i = 0; i < csapatok.Length; i++)
            {
                Console.WriteLine($"A '{csapatok[i]}' {pontsz�mok[i]} pontot szerzett!");
            }

            Console.WriteLine($"A nyertes csapat: {csapatok[BajnoksagNyertes(pontsz�mok)]}");

            Console.ReadLine();
        }

        // a) �rjon met�dust, amely az eredmenyek t�mb elemeit felt�lti v�letlen �rt�kekkel. Term�szetesen eredmenyek[i, i] minden esetben 0 lesz...
        static void FociFeltoltes(int[,] eredm�nyek)
        {
            for (int i = 0; i < eredm�nyek.GetLength(0); i++)
            {
                for (int j = 0; j < eredm�nyek.GetLength(1); j++)
                {
                    // .. ha tudjuk, hogy milyen �rt�kekkel incializ�l�dik a t�mb, akkor itt a 0 �rt�kad�sa felesleges
                    //if (i == j)
                    //{
                    //    eredm�nyek[i, j] = 0;
                    //}
                    if (i != j) // else
                    {
                        int r = rnd.Next(100); // ha a feladat azt mondja, hogy h�ny sz�zal�k es�llyel veszi fel a t�mb adott eleme az �rt�ket
                        //if (r == 0) eredm�nyek[i, j] = 0; // 20%
                        if (r < 30) eredm�nyek[i, j] = 1; // 30%
                        else if (r < 80) eredm�nyek[i, j] = 3; // 50%
                    }
                }
            }
        }

        // b) �rjon met�dust, amely el��ll�tja az eredm�nyeket t�bl�zatos form�ban.
        static string FociToString(int[,] eredm�nyek, string[] csapatok)
        {
            string s = "  ";

            for (int i = 0; i < csapatok.Length; i++)
                s += $"{csapatok[i]} ";

            for (int i = 0; i < eredm�nyek.GetLength(0); i++)
            {
                s += $"\n{csapatok[i]} ";
                for (int j = 0; j < eredm�nyek.GetLength(1); j++)
                {
                    s += $"{eredm�nyek[i, j]} ";
                }
            }

            return s;
        }

        // c) �rjon met�dust, amellyel eld�nthet�, hogy egy adott nev� csapat indult-e a bajnoks�gban.
        static bool FocicsapatJatszottE(string[] csapatok, string csapat)
        {
            //int i = 0;
            //while (i < csapatok.Length && csapatok[i] != csapat)
            //{
            //    i++;
            //}
            //return i < csapatok.Length;

            for (int i = 0; i < csapatok.Length; i++) // ha a feladat nem v�rja el az eld�nt�s t�tel�t, akkor m�s, de helyes megold�s is elfogadhat�
            {
                if (csapatok[i] == csapat)
                    return true;
            }
            return false;
        }

        // d) �rjon met�dust, amely meghat�rozza, hogy egy adott csapat �sszesen h�ny pontot �rt el a bajnoks�gban.
        //    (Ez az adott csapat eredmenyek t�mbbeli sor�nak �s oszlop�nak �sszegek�nt adhat� meg.)
        static int CsapatPontszam(int[,] eredm�nyek, string[] csapatok, string csapat)
        {
            if (!FocicsapatJatszottE(csapatok, csapat)) // a met�dus elej�n szok�s vizsg�lni azokat az eseteket amik a m�k�d�se szempontj�b�l hib�t eredm�nyezn�nek
                return -1; // olyan �rt�kkel t�rj�nk vissza, amib�l tudjuk, hogy 'hiba' t�rt�nt

            int szum = 0;

            int csapatIDX = 0;
            while (csapatok[csapatIDX] != csapat)
                csapatIDX++;

            for (int i = 0; i < eredm�nyek.GetLength(0); i++)
            {
                szum += eredm�nyek[i, csapatIDX];
            }

            for (int j = 0; j < eredm�nyek.GetLength(1); j++)
            {
                szum += eredm�nyek[csapatIDX, j];
            }

            return szum;
        }

        // e) �rjon met�dust, amely seg�ts�g�vel egy �j t�mbben el��ll�that� a bajnoks�g minden egyes csapat�ra, hogy �sszesen h�ny pontot szereztek.
        static int[] CsapatonkentiPontszam(int[,] eredm�nyek, string[] csapatok)
        {
            int[] pontsz�mok = new int[csapatok.Length];
            for (int i = 0; i < csapatok.Length; i++)
                pontsz�mok[i] = CsapatPontszam(eredm�nyek, csapatok, csapatok[i]); // ne k�sz�ts�nk el funkci�kat k�tszer..haszn�ljuk a m�r l�tez� met�dusokat

            return pontsz�mok;
        }

        // f) �rjon met�dust, amely megadja, hogy melyik csapat nyerte a bajnoks�got.
        //    (Ha t�bb csapat is maxim�lis pontsz�mot szerzett, akkor k�z�l�k az els� csapatot hozza ki nyertesnek.)
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
