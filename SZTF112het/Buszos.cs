using System;
using System.IO;

namespace Buszos
{
    class Autobusz
    {
        int maxFerohely;
        int hatotav;
        double ar;
        int szabadHely;

        public int MaxFerohely {
            get { return maxFerohely; }
        }

        public int Hatotav {
            get { return hatotav; }
            set { hatotav = value; }
        }

        public double Ar {
            get { return ar; }
            set { ar = value; }
        }

        public int SzabadHely {
            get { return szabadHely; }
            set { szabadHely = value; }
        }

        public Autobusz(int ferohely, int hatotav, double ar)
        {
            this.maxFerohely = ferohely;
            this.hatotav = hatotav;
            this.ar = ar;
            this.szabadHely = maxFerohely;
        }

        public bool UtastHozzaad(int utasokSzama)
        {
            if (utasokSzama > this.szabadHely)
            {
                return false;
            }
            else
            {
                this.szabadHely -= utasokSzama;
                return true;
            }
        }

        public string StringgeAlakit()
        {
            return "Maximális férőhely: " + maxFerohely + ", Hatótáv: " + hatotav + ", Ár: " + ar;
        }

        public void FajlbaIr(string fajlnev)
        {
            StreamWriter sw = new StreamWriter(fajlnev, true);

            sw.WriteLine(maxFerohely + ";" + hatotav + ";" + ar + ";" + szabadHely);

            sw.Close();
        }
    }

    class Vallalat
    {
        private Autobusz[] autobuszok;

        public Vallalat()
        {
        }


        public Autobusz[] BuszokBetolteseFajlbol()
        {
            string eleresiut = "buszok.txt";
            StreamReader sr = new StreamReader(eleresiut);

            int buszokSzama = 0;

            while (!sr.EndOfStream)
            {
                buszokSzama++;
                sr.ReadLine();
            }

            Autobusz[] buszok = new Autobusz[buszokSzama];

            sr = new StreamReader(eleresiut);

            for (int i = 0; i < buszokSzama; i++)
            {
                string[] sor = sr.ReadLine().Split(';');
                buszok[i] = new Autobusz(int.Parse(sor[0]), int.Parse(sor[1]), double.Parse(sor[2]) * int.Parse(sor[1]));
            }

            sr.Close();

            return buszok;
        }

        public int LegolcsobbBuszFerohelyeinekSzama(Autobusz[] buszok)
        {
            int min = 0;

            for (int i = 1; i < buszok.Length; i++)
            {
                if (buszok[i].Ar < buszok[min].Ar)
                {
                    min = i;
                }
            }

            return buszok[min].MaxFerohely;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Vallalat vallalat = new Vallalat();
            Autobusz[] buszok = vallalat.BuszokBetolteseFajlbol();

            for (int i = 0; i < buszok.Length; i++)
            {
                Console.WriteLine(buszok[i].StringgeAlakit());
            }

            int legolcsobb = vallalat.LegolcsobbBuszFerohelyeinekSzama(buszok);
            Console.WriteLine("A legolcsóbb busz férőhelyeinek száma: " + legolcsobb);

            Random r = new Random();

            for (int i = 0; i < buszok.Length; i++)
            {
                if (buszok[i].UtastHozzaad(r.Next(10, 50)))
                {
                    Console.WriteLine("A(z) " + i + ". buszhoz az utasok hozzáadása sikeres volt.");
                }
                buszok[i].FajlbaIr("buszok.ki");

            }

            Console.ReadLine();

        }
    }
}
