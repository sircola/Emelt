using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintaZH1_L3_macskas
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] korzetek = new string[2];

            // a. feladat
            KorzeteketBeker(korzetek);

            for (int i = 0; i < korzetek.Length; i++)
            {
                Console.WriteLine(korzetek[i]);
            }

            Console.ReadLine();

            // b. feladat
            int[,] haviTermelesek = TermeleseketEloallit(korzetek.Length);

            // c. feladat
            Megjelenit(korzetek, haviTermelesek);

            // d. feladat
            Console.WriteLine("Adj meg egy körzet nevet: ");
            string korzet = Console.ReadLine();

            int[] haviAdatok = KorzetTermelese(korzet, korzetek, haviTermelesek);

            if (haviAdatok.Length == 0)
            {
                Console.WriteLine("Nincs ilyen körzet: {0}", korzet);
            }
            else
            {
                Console.WriteLine("{0} körzet termelései:", korzet);

                foreach (int termeles in haviAdatok)
                {
                    Console.Write("{0} ", termeles);
                }

                Console.WriteLine();
            }

            // e. feladat
            int[] osszesitesKorzetenkent = TermelesekOsszesitese(haviTermelesek);

            Console.WriteLine("Termelések körzetenként: ");

            for (int i = 0; i < osszesitesKorzetenkent.Length; i++)
            {
                Console.WriteLine("{0}: {1}", korzetek[i], osszesitesKorzetenkent[i]);
            }

            // f. feladat
            Console.Write("Add meg normát egész számként: ");
            int norma = int.Parse(Console.ReadLine());
            int normaFelettiekSzama = NormaFelettiTermelesekSzamossaga(haviTermelesek, norma);

            Console.WriteLine("A norma felettiek számossága: {0}", normaFelettiekSzama);

            // g. feladat
            Console.WriteLine("Az átlag alatti körzetek legalább 3 alkalommal:");
            int[] atlagAlattiakIndexei = AtlagAlattiakIndexei(haviTermelesek);

            for (int i = 0; i < atlagAlattiakIndexei.Length; i++)
            {
                Console.Write("{0} ", atlagAlattiakIndexei[i]);
            }

            Console.ReadLine();
        }

        static void KorzeteketBeker(string[] korzetek)
        {
            for (int i = 0; i < korzetek.Length; i++)
            {
                Console.Write("Add meg a(z) {0}. körzetet: ", i + 1);
                string korzet = Console.ReadLine();

                // Speciális karakterek átalakítása szóközzé
                char[] specialisKarakterek = new char[] { '!', ',', ';', '?', ':', '>', '<', '[', ']', '-', '@', '{', '}' };

                for (int j = 0; j < specialisKarakterek.Length; j++)
                {
                    korzet = korzet.Replace(specialisKarakterek[j], ' '); // szóközökké átállítás hogy le lehessen trimmelni
                }

                // Üres szóközök eltüntetése a jobb és bal oldalról
                korzet = korzet.Trim();

                // Elemér megoldása :(
                //for (int j = 13; j > 1; j--)
                //{
                //    korzet = korzet.Replace(SzokozoketEloallit(j), " ");
                //}

                int k = 0;
                bool torlesBekapcsolva = false;

                while (k < korzet.Length)
                {
                    if (korzet[k] == ' ' && torlesBekapcsolva)
                    {
                        korzet = korzet.Remove(k, 1);
                    }
                    else if (k > 0 && korzet[k - 1] == ' ' && korzet[k] == ' ')
                    {
                        torlesBekapcsolva = true;
                        korzet = korzet.Remove(k, 1);
                    }
                    else if (korzet[k] != ' ')
                    {
                        torlesBekapcsolva = false;
                    }

                    if (!torlesBekapcsolva) // nem kell törölni, akkor megyünk előre
                    {
                        k++;
                    }
                }

                korzetek[i] = korzet;
            }
        }

        static string SzokozoketEloallit(int szokozSzam)
        {
            string szokozok = "";

            for (int i = 0; i < szokozSzam; i++)
            {
                szokozok = szokozok + " ";
            }

            return szokozok;
        }

        static int[,] TermeleseketEloallit(int korzetekSzama)
        {
            int[,] termelesek = new int[korzetekSzama, 13];
            Random r = new Random();

            for (int i = 0; i < termelesek.GetLength(0); i++)
            {
                for (int j = 0; j < termelesek.GetLength(1); j++)
                {
                    if (j % 2 == 1) // páratlan hónap
                    {
                        termelesek[i, j] = r.Next(60, 81);
                    }
                    else
                    {
                        termelesek[i, j] = r.Next(45, 100);
                    }
                }
            }

            return termelesek;
        }

        static void Megjelenit(string[] korzetek, int[,] haviTermelesek)
        {
            int leghosszabbIndex = LeghosszabbKorzetNevIndexe(korzetek);
            int leghosszabbNevhossz = korzetek[leghosszabbIndex].Length;

            for (int i = 0; i < haviTermelesek.GetLength(0); i++)
            {
                Console.Write("{0}: ", korzetek[i]);
                Console.Write(SzokozoketEloallit(leghosszabbNevhossz - korzetek[i].Length));

                for (int j = 0; j < haviTermelesek.GetLength(1) - 1; j++) // végén ne legyen , 
                {
                    Console.Write("{0}k, ", haviTermelesek[i, j]);
                }

                Console.Write("{0}k\n", haviTermelesek[i, haviTermelesek.GetLength(1) - 1]);
            }
        }

        static int LeghosszabbKorzetNevIndexe(string[] korzetek)
        {
            int max = 0;

            for (int i = 1; i < korzetek.Length; i++)
            {
                if (korzetek[i].Length > korzetek[max].Length)
                {
                    max = i;
                }
            }

            return max;
        }

        static int[] TermelesekOsszesitese(int[,] haviTermelesek)
        {
            int[] osszegek = new int[haviTermelesek.GetLength(0)];

            for (int i = 0; i < haviTermelesek.GetLength(0); i++)
            {
                for (int j = 0; j < haviTermelesek.GetLength(1); j++)
                {
                    osszegek[i] += haviTermelesek[i, j];
                }
            }

            return osszegek;
        }

        static int[] KorzetTermelese(string korzet, string[] korzetek, int[,] haviTermelesek)
        {
            int i = 0;

            while (i < korzetek.Length && korzetek[i] != korzet)
            {
                i++;
            }

            if (i < korzetek.Length) // vam ilyen körzet
            {
                int[] eredmeny = new int[haviTermelesek.GetLength(1)];

                for (int j = 0; j < eredmeny.Length; j++)
                {
                    eredmeny[j] = haviTermelesek[i, j];
                }

                return eredmeny;
            }
            else
            {
                return new int[0];
            }

        }

        static int NormaFelettiTermelesekSzamossaga(int[,] termelesek, int norma)
        {
            int db = 0;

            for (int i = 0; i < termelesek.GetLength(0); i++)
            {
                for (int j = 0; j < termelesek.GetLength(1); j++)
                {
                    if (termelesek[i,j] > norma)
                    {
                        db++;
                    }
                }
            }

            return db;
        }

        static int[] AtlagAlattiakIndexei(int[,] haviTermelesek)
        {
            // átlag kiszámítása
            float atlag = 0;

            for (int i = 0; i < haviTermelesek.GetLength(0); i++)
            {
                for (int j = 0; j < haviTermelesek.GetLength(1); j++)
                {
                    atlag += haviTermelesek[i, j];
                }
            }

            atlag = atlag / haviTermelesek.Length;

            Console.WriteLine("Átlag: {0}", atlag); // teszteléshez

            // egy tömbbe fogom számlálni az átlag alatti hónapok számát
            int[] atlagAlattiHonapokSzamossaga = new int[haviTermelesek.GetLength(0)];

            for (int i = 0; i < atlagAlattiHonapokSzamossaga.Length; i++)
            {
                int j = 0;

                while (j < haviTermelesek.GetLength(1) && atlagAlattiHonapokSzamossaga[i] < 3) // ha meg van a három akkor már nem is foglalkozom a körzettel tovább
                {
                    if (haviTermelesek[i,j] < atlag)
                    {
                        atlagAlattiHonapokSzamossaga[i]++; // megnövelem az adott körzethez tartozó nónapok számát
                    }

                    j++;
                }
            }

            // megszámlálom hogy végül mennyi ilyen körzet lett
            int db = 0;

            for (int i = 0; i < atlagAlattiHonapokSzamossaga.Length; i++)
            {
                if (atlagAlattiHonapokSzamossaga[i] == 3) // >= is lehetne hogyha nem állítottam volna le 3 nál a számlálást
                {
                    db++;
                }
            }

            // kiválogatás
            int[] indexekTombje = new int[db];
            int utolsoSzabadHely = 0;

            for (int i = 0; i < atlagAlattiHonapokSzamossaga.Length; i++)
            {
                if (atlagAlattiHonapokSzamossaga[i] == 3)
                {
                    indexekTombje[utolsoSzabadHely] = i; // kimentem a körzet indexét
                    utolsoSzabadHely++;
                }
            }

            return indexekTombje;
        }
    }
}
