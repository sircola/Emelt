using System;

namespace Ora5
{
    // �rjon programot egy k�v�z� napi forgalmi adatainak kezel�s�re!
    // 1. A programban megadhat�, hogy h�ny nap adatait akarjuk elt�rolni.
    // 2. Minden napn�l megadhat� lefgeljebb 10 ital, amit aznap felszolg�ltak.
    // 3. A program megadja, hogy a vizsg�lt napokon milyen italokat szolt�ltak fel.
    // 4. Minden ital eset�n meg lehet hat�rozni, hogy h�ny esetben szolg�lt�k fel.
    // --> A feladat megold�s�hoz k�sz�tse el a megadott met�dusokat �s h�vja meg �ket a Main-ben.

    class K�v�z�
    {
        static void Main(string[] args)
        {
        }

        //a) Bek�ri a vizsg�lt napok sz�m�t.
        static int BekerNapokSzama()
        {
            return 0;
        }

        //b) Bek�ri az egyes napokon eladott italok list�j�t.
        static string[,] EladottItalokBekerese()
        {
            return new string[0, 0];
        }

        //c) Megadja, hogy adott napon h�ny darab fogyaszt�s volt.
        static int AdottNapiItalokSzama(string[,] italokNaponta, int index)
        {
            return 0;
        }

        //d) Az adott nap italait kiv�logatja. A kimenet hossza megegyezik a t�nyleges fogyaszt�si mennyis�ggel.
        static string[] AdottNapotKivalogat(string[,] italokNaponta, int index)
        {
            return new string[0];
        }

        //e) �sszehasonl�tja a k�t bemeneti stringet.
        static bool StringKisebbE(string elso, string masodik)
        {
            return false;
        }

        //f) A bemeneti t�mb�t n�vekv� m�don rendezi.
        static void NapiItalListatRendez(string[] italok)
        {
        }

        //g) A rendezett bemeneti t�mbb�l kisz�ri az ism�tl�d� elemeket. A kimeneti t�mb m�rete megegyezik a benne t�nylegesen elt�rolt italok sz�m�val.
        static string[] IsmetlodoItalokKiszurese(string[] italok)
        {
            return new string[0];
        }

        //h) Az �sszefuttat�s t�telt alkalmazva �sszefuttatja a k�t rendezett bemeneti t�mb�t. A kimeneti t�mb m�rete megegyezik a benne elt�rolt italok sz�m�val.
        static string[] KetListatOsszefuttat(string[] elsoLista, string[] madosikLista)
        {
            return new string[0];
        }

        //i) Visszaadja a bemeneti t�mbben tal�lhat� italok rendezett �s ism�tl�d�smentes list�j�t.
        static string[] Italok(string[,] italokNaponta)
        {
            return new string[0];
        }

        //j) Megadja, hogy az egyes italokb�l t�nylegesen h�ny elad�s t�rt�nt.
        static int[] EladottMennyisegek(string[,] italokNaponta, string[] italok)
        {
            return new int[0];
        }
    }

    //class K�v�z�Megold�s
    //{
    //    static void Main(string[] args)
    //    {
    //        string[,] italok = EladottItalokBekerese();
    //        Console.WriteLine();

    //        string[] rendezettItalok = Italok(italok);

    //        int[] eladottMennyis�gek = EladottMennyis�gek(italok, rendezettItalok);

    //        for (int i = 0; i < rendezettItalok.Length; i++)
    //            Console.WriteLine(rendezettItalok[i] + ": " + eladottMennyis�gek[i] + " darab");

    //        Console.ReadLine();
    //    }

    //    //a) Bek�ri a vizsg�lt napok sz�m�t.
    //    static int BekerNapokSzama()
    //    {
    //        Console.Write("H�ny nap elad�sait akarod r�gz�teni? ");
    //        return int.Parse(Console.ReadLine());
    //    }

    //    //b) Bek�ri az egyes napokon eladott italok list�j�t.
    //    static string[,] EladottItalokBekerese()
    //    {
    //        int napok = BekerNapokSzama();
    //        string[,] italok = new string[napok, 10];

    //        for (int i = 0; i < italok.GetLength(0); i++) //napokon
    //        {
    //            int j = 0;
    //            Console.WriteLine(i + 1 + ". napi italok (max 10 darab, ha kevesebb �ss �res entert)");

    //            string ital = Console.ReadLine();
    //            while (j < italok.GetLength(1) - 1 && ital != "") //amig nincs �res vagy 10
    //            {
    //                italok[i, j] = ital;
    //                j++;

    //                ital = Console.ReadLine();
    //            } //marad�k null

    //            if (ital != "") italok[i, j] = ital;
    //        }
    //        return italok;
    //    }

    //    //c) Megadja, hogy adott napon h�ny darab fogyaszt�s volt.
    //    static int AdottNapiItalokSzama(string[,] italok, int index)
    //    {
    //        int j = 0;
    //        while (j < italok.GetLength(1) && italok[index, j] != null) // megsz�moljuk a nem �rest
    //            j++;

    //        return j;
    //    }

    //    //d) Az adott nap italait kiv�logatja. A kimenet hossza megegyezik a t�nyleges fogyaszt�si mennyis�ggel.
    //    static string[] AdottNapotKivalogat(string[,] italok, int index)
    //    {
    //        int db = AdottNapiItalokSzama(italok, index);
    //        string[] fogyasztas = new string[db];

    //        int j = 0;
    //        while (j < fogyasztas.Length && italok[index, j] != null) //m�soljuk a nem �resig
    //        {
    //            fogyasztas[j] = italok[index, j];
    //            j++;
    //        }

    //        return fogyasztas;
    //    }

    //    //e) �sszehasonl�tja a k�t bemeneti stringet.
    //    static bool StringKisebbE(string elso, string masodik)//masodik<elso=TRUE
    //    {
    //        //if (string.Compare(elso, masodik) == 1) return true;
    //        //else return false;
    //        return string.Compare(elso, masodik) == 1;
    //    }

    //    //f) A bemeneti t�mb�t n�vekv� m�don rendezi.
    //    static void NapiItalListatRendez(string[] italokNaponta)//minimumkiv�laszt�sos rendez�s
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

    //    //g) A rendezett bemeneti t�mbb�l kisz�ri az ism�tl�d� elemeket. A kimeneti t�mb m�rete megegyezik a benne t�nylegesen elt�rolt italok sz�m�val.
    //    static string[] IsmetlodoItalokKiszurese(string[] italokNaponta)
    //    {
    //        int db = 0; //index ez is
    //        for (int i = 1; i < italokNaponta.Length; i++) //megsz�mol �s el�rehoz
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

    //        string[] sz�rt = new string[++db]; //�tm�sol
    //        for (int i = 0; i < sz�rt.Length; i++)
    //            sz�rt[i] = italokNaponta[i];

    //        return sz�rt;
    //    }

    //    //h) Az �sszefuttat�s t�telt alkalmazva �sszefuttatja a k�t rendezett bemeneti t�mb�t. A kimeneti t�mb m�rete megegyezik a benne elt�rolt italok sz�m�val.
    //    static string[] KetListatOsszefuttat(string[] elso, string[] masodik)
    //    {   //megtartja a rendezetts�get!

    //        string[] �sszefuttatott = new string[elso.Length + masodik.Length];
    //        int i = 0; int j = 0; int db = -1; //ez is index

    //        while (i < elso.Length && j < masodik.Length)
    //        {
    //            db++;
    //            if (StringKisebbE(masodik[j], elso[i])) // if(elso[i] < masodik[j])
    //            {
    //                �sszefuttatott[db] = elso[i];
    //                i++;
    //            }
    //            else
    //            {
    //                if (StringKisebbE(elso[i], masodik[j])) // if (elso[i] > masodik[j])
    //                {
    //                    �sszefuttatott[db] = masodik[j];
    //                    j++;
    //                }
    //                else //egyenl�ek
    //                {
    //                    �sszefuttatott[db] = elso[i];
    //                    i++;
    //                    j++;
    //                }
    //            }
    //        }

    //        while (i < elso.Length) //marad�k az els�b�l
    //        {
    //            db++;
    //            �sszefuttatott[db] = elso[i];
    //            i++;
    //        }

    //        while (j < masodik.Length) //marad�k a m�sodikb�l
    //        {
    //            db++;
    //            �sszefuttatott[db] = masodik[j];
    //            j++;
    //        }
    //        return �sszefuttatott;
    //    }

    //    //i) Visszaadja a bemeneti t�mbben tal�lhat� italok rendezett �s ism�tl�d�smentes list�j�t.
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

    //    //j) Megadja, hogy az egyes italokb�l t�nylegesen h�ny elad�s t�rt�nt.
    //    static int[] EladottMennyis�gek(string[,] italok, string[] eladott)
    //    {
    //        int[] mennyis�gek = new int[eladott.Length];

    //        for (int i = 0; i < italok.GetLength(0); i++)//megsz�ml�l�s
    //        {
    //            for (int j = 0; j < italok.GetLength(1); j++)
    //            {
    //                int k = 0;//kiv�laszt�s
    //                while (k < eladott.Length && italok[i, j] != eladott[k])
    //                    k++;

    //                if (italok[i, j] != null)
    //                    mennyis�gek[k]++;
    //            }
    //        }
    //        return mennyis�gek;
    //    }
    //}
}
