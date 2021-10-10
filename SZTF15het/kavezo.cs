using System;

namespace Ora5
{
    // Írjon programot egy kávézó napi forgalmi adatainak kezelésére!
    // 1. A programban megadható, hogy hány nap adatait akarjuk eltárolni.
    // 2. Minden napnál megadható lefgeljebb 10 ital, amit aznap felszolgáltak.
    // 3. A program megadja, hogy a vizsgált napokon milyen italokat szoltáltak fel.
    // 4. Minden ital esetén meg lehet határozni, hogy hány esetben szolgálták fel.
    // --> A feladat megoldásához készítse el a megadott metódusokat és hívja meg õket a Main-ben.

    class Kávézó
    {
        static void Main(string[] args)
        {
        }

        //a) Bekéri a vizsgált napok számát.
        static int BekerNapokSzama()
        {
            return 0;
        }

        //b) Bekéri az egyes napokon eladott italok listáját.
        static string[,] EladottItalokBekerese()
        {
            return new string[0, 0];
        }

        //c) Megadja, hogy adott napon hány darab fogyasztás volt.
        static int AdottNapiItalokSzama(string[,] italokNaponta, int index)
        {
            return 0;
        }

        //d) Az adott nap italait kiválogatja. A kimenet hossza megegyezik a tényleges fogyasztási mennyiséggel.
        static string[] AdottNapotKivalogat(string[,] italokNaponta, int index)
        {
            return new string[0];
        }

        //e) Összehasonlítja a két bemeneti stringet.
        static bool StringKisebbE(string elso, string masodik)
        {
            return false;
        }

        //f) A bemeneti tömböt növekvõ módon rendezi.
        static void NapiItalListatRendez(string[] italok)
        {
        }

        //g) A rendezett bemeneti tömbbõl kiszûri az ismétlõdõ elemeket. A kimeneti tömb mérete megegyezik a benne ténylegesen eltárolt italok számával.
        static string[] IsmetlodoItalokKiszurese(string[] italok)
        {
            return new string[0];
        }

        //h) Az összefuttatás tételt alkalmazva összefuttatja a két rendezett bemeneti tömböt. A kimeneti tömb mérete megegyezik a benne eltárolt italok számával.
        static string[] KetListatOsszefuttat(string[] elsoLista, string[] madosikLista)
        {
            return new string[0];
        }

        //i) Visszaadja a bemeneti tömbben található italok rendezett és ismétlõdésmentes listáját.
        static string[] Italok(string[,] italokNaponta)
        {
            return new string[0];
        }

        //j) Megadja, hogy az egyes italokból ténylegesen hány eladás történt.
        static int[] EladottMennyisegek(string[,] italokNaponta, string[] italok)
        {
            return new int[0];
        }
    }

    //class KávézóMegoldás
    //{
    //    static void Main(string[] args)
    //    {
    //        string[,] italok = EladottItalokBekerese();
    //        Console.WriteLine();

    //        string[] rendezettItalok = Italok(italok);

    //        int[] eladottMennyiségek = EladottMennyiségek(italok, rendezettItalok);

    //        for (int i = 0; i < rendezettItalok.Length; i++)
    //            Console.WriteLine(rendezettItalok[i] + ": " + eladottMennyiségek[i] + " darab");

    //        Console.ReadLine();
    //    }

    //    //a) Bekéri a vizsgált napok számát.
    //    static int BekerNapokSzama()
    //    {
    //        Console.Write("Hány nap eladásait akarod rögzíteni? ");
    //        return int.Parse(Console.ReadLine());
    //    }

    //    //b) Bekéri az egyes napokon eladott italok listáját.
    //    static string[,] EladottItalokBekerese()
    //    {
    //        int napok = BekerNapokSzama();
    //        string[,] italok = new string[napok, 10];

    //        for (int i = 0; i < italok.GetLength(0); i++) //napokon
    //        {
    //            int j = 0;
    //            Console.WriteLine(i + 1 + ". napi italok (max 10 darab, ha kevesebb üss üres entert)");

    //            string ital = Console.ReadLine();
    //            while (j < italok.GetLength(1) - 1 && ital != "") //amig nincs üres vagy 10
    //            {
    //                italok[i, j] = ital;
    //                j++;

    //                ital = Console.ReadLine();
    //            } //maradék null

    //            if (ital != "") italok[i, j] = ital;
    //        }
    //        return italok;
    //    }

    //    //c) Megadja, hogy adott napon hány darab fogyasztás volt.
    //    static int AdottNapiItalokSzama(string[,] italok, int index)
    //    {
    //        int j = 0;
    //        while (j < italok.GetLength(1) && italok[index, j] != null) // megszámoljuk a nem ürest
    //            j++;

    //        return j;
    //    }

    //    //d) Az adott nap italait kiválogatja. A kimenet hossza megegyezik a tényleges fogyasztási mennyiséggel.
    //    static string[] AdottNapotKivalogat(string[,] italok, int index)
    //    {
    //        int db = AdottNapiItalokSzama(italok, index);
    //        string[] fogyasztas = new string[db];

    //        int j = 0;
    //        while (j < fogyasztas.Length && italok[index, j] != null) //másoljuk a nem üresig
    //        {
    //            fogyasztas[j] = italok[index, j];
    //            j++;
    //        }

    //        return fogyasztas;
    //    }

    //    //e) Összehasonlítja a két bemeneti stringet.
    //    static bool StringKisebbE(string elso, string masodik)//masodik<elso=TRUE
    //    {
    //        //if (string.Compare(elso, masodik) == 1) return true;
    //        //else return false;
    //        return string.Compare(elso, masodik) == 1;
    //    }

    //    //f) A bemeneti tömböt növekvõ módon rendezi.
    //    static void NapiItalListatRendez(string[] italokNaponta)//minimumkiválasztásos rendezés
    //    {
    //        for (int i = 0; i < italokNaponta.Length - 1; i++)
    //        {
    //            int min = i;
    //            for (int j = i + 1; j < italokNaponta.Length; j++)
    //            {
    //                if (StringKisebbE(italokNaponta[min], italokNaponta[j])) //if(x[min] > x[j])
    //                    min = j;
    //            }
    //            string t = italokNaponta[i];
    //            italokNaponta[i] = italokNaponta[min];
    //            italokNaponta[min] = t;
    //        }
    //    }

    //    //g) A rendezett bemeneti tömbbõl kiszûri az ismétlõdõ elemeket. A kimeneti tömb mérete megegyezik a benne ténylegesen eltárolt italok számával.
    //    static string[] IsmetlodoItalokKiszurese(string[] italokNaponta)
    //    {
    //        int db = 0; //index ez is
    //        for (int i = 1; i < italokNaponta.Length; i++) //megszámol és elõrehoz
    //        {
    //            int j = 0;
    //            while (j <= db && italokNaponta[i] != italokNaponta[j] && italokNaponta[i] != null)
    //                j++;

    //            if (j > db)
    //            {
    //                db++;
    //                italokNaponta[db] = italokNaponta[i];
    //            }
    //        }

    //        string[] szûrt = new string[++db]; //átmásol
    //        for (int i = 0; i < szûrt.Length; i++)
    //            szûrt[i] = italokNaponta[i];

    //        return szûrt;
    //    }

    //    //h) Az összefuttatás tételt alkalmazva összefuttatja a két rendezett bemeneti tömböt. A kimeneti tömb mérete megegyezik a benne eltárolt italok számával.
    //    static string[] KetListatOsszefuttat(string[] elso, string[] masodik)
    //    {   //megtartja a rendezettséget!

    //        string[] összefuttatott = new string[elso.Length + masodik.Length];
    //        int i = 0; int j = 0; int db = -1; //ez is index

    //        while (i < elso.Length && j < masodik.Length)
    //        {
    //            db++;
    //            if (StringKisebbE(masodik[j], elso[i])) // if(elso[i] < masodik[j])
    //            {
    //                összefuttatott[db] = elso[i];
    //                i++;
    //            }
    //            else
    //            {
    //                if (StringKisebbE(elso[i], masodik[j])) // if (elso[i] > masodik[j])
    //                {
    //                    összefuttatott[db] = masodik[j];
    //                    j++;
    //                }
    //                else //egyenlõek
    //                {
    //                    összefuttatott[db] = elso[i];
    //                    i++;
    //                    j++;
    //                }
    //            }
    //        }

    //        while (i < elso.Length) //maradék az elsõbõl
    //        {
    //            db++;
    //            összefuttatott[db] = elso[i];
    //            i++;
    //        }

    //        while (j < masodik.Length) //maradék a másodikból
    //        {
    //            db++;
    //            összefuttatott[db] = masodik[j];
    //            j++;
    //        }
    //        return összefuttatott;
    //    }

    //    //i) Visszaadja a bemeneti tömbben található italok rendezett és ismétlõdésmentes listáját.
    //    static string[] Italok(string[,] italok)
    //    {
    //        string[] ret = new string[0];

    //        for (int i = 0; i < italok.GetLength(0); i++)
    //        {
    //            string[] adottNap = AdottNapotKivalogat(italok, i);
    //            NapiItalListatRendez(adottNap);
    //            adottNap = KetListatOsszefuttat(ret, adottNap);
    //            ret = IsmetlodoItalokKiszurese(adottNap);
    //        }
    //        return ret;
    //    }

    //    //j) Megadja, hogy az egyes italokból ténylegesen hány eladás történt.
    //    static int[] EladottMennyiségek(string[,] italok, string[] eladott)
    //    {
    //        int[] mennyiségek = new int[eladott.Length];

    //        for (int i = 0; i < italok.GetLength(0); i++)//megszámlálás
    //        {
    //            for (int j = 0; j < italok.GetLength(1); j++)
    //            {
    //                int k = 0;//kiválasztás
    //                while (k < eladott.Length && italok[i, j] != eladott[k])
    //                    k++;

    //                if (italok[i, j] != null)
    //                    mennyiségek[k]++;
    //            }
    //        }
    //        return mennyiségek;
    //    }
    //}
}
