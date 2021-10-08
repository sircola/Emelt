using System;

namespace SZTF15het
{
    class Program
    {

        static int BekérNapokSzáma()
        {
            Console.WriteLine("Kérem a napok számát:");
            return int.Parse(Console.ReadLine());
        }

        string[,] EladottItalokBekérése()
        {
            string[,] italok = new string[BekérNapokSzáma(), 10];

            for (int i = 0; i < italok.GetLength(0); i++)
            {
                Console.WriteLine("" + i + ". napi italok (max 10), üres enter tovább lép");

                int j = 0;
                string ital = Console.ReadLine();
                while (j < italok.GetLength(1) && ital != "")
                {
                    italok[i, j] = ital;
                    j++;
                    ital = Console.ReadLine();
                }
                if (ital != "") italok[i, j] = ital;

                /*
                string ital = Console.ReadLine();
                for (int j = 0; j < italok.GetLength(1) && ital != ""; j++) {
                    if (ital != "")
                        italok[i, j] = ital;
                    ital = Console.ReadLine();
                }
                */
            }

            return italok;
        }

        static int AdottNapiItalokSzáma(string[,] italokNaponta, int index)
        {
            int j = 0;
            while (j < italokNaponta.GetLength(1) && italokNaponta[index, j] != null)
                j++;
            return j;
        }

        static string[] AdottNapotKiválogat(string[,] italokNaponta, int index)
        {
            string[] italok = new string[AdottNapiItalokSzáma(italokNaponta, index)];

            int j = 0;
            while (j < italokNaponta.GetLength(1) && italokNaponta[index, j] != null)
                italok[j] = italokNaponta[index, j++];

            /*
            for (int i=0; i<italok.Length; i++)
                italok[i] = italokNaponta[index, i];
            */

            return italok;
        }


        static bool StringKisebbE(string elso, string masodik)
        {
            return string.Compare(elso, masodik) == 1;

            /*
            if (string.Compare(elso, masodik) == 1 )
                return true;
            
            return false;
            */
        }

        void NapiItalListatRendez(string[] italok)
        {
            for (int i = 0; i < italok.Length - 1; i++)
            {
                int min = i;
                for (int j = i + 1; j < italok.Length; j++)
                    if (StringKisebbE(italok[min], italok[j]))
                        min = j;
                string s = italok[i];
                italok[i] = italok[min];
                italok[min] = s;
            }
        }

        static string[] IsmetlodoItalokKiszurese(string[] italok)
        {
            int db = 0;
            for (int i = 1; i < italok.Length; i++)
            {
                int j = 0;
                while (j <= db && italok[i] != italok[j])
                {
                    ++j;
                }
                if (j > db)
                {
                    ++db;
                    italok[db] = italok[i];
                }
            }

            string[] szűrt = new string[++db];
            for (int i = 0; i < szűrt.Length; i++)
                szűrt[i] = italok[i];

            return szűrt;
        }

        static void Main(string[] args)
        {

        }
    }
}

