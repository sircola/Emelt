using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Feherjek
{
    class Elelmiszer
    {
        public string nev, kategoria;
        public int kj, kcal;
        public double feherje, zsir, szenhidrat;
        public Elelmiszer( string sor)
        {
            string[] adatok = sor.Split(':');
            nev = adatok[0];
            kategoria = adatok[1];
            kj = int.Parse(adatok[2]);
            kcal = Convert.ToInt32(adatok[3]);
            feherje = double.Parse(adatok[4]);
            zsir = Convert.ToDouble(adatok[5]);
            szenhidrat = Convert.ToDouble(adatok[6]);
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            List<Elelmiszer> elelmiszerek = new List<Elelmiszer>();

            StreamReader sr = new StreamReader("feherjek.txt");

            sr.ReadLine();
            while( !sr.EndOfStream )
            {
                elelmiszerek.Add( new Elelmiszer(sr.ReadLine()) );
            }

            sr.Close();

            Console.WriteLine("3. feladat");

            Elelmiszer max = elelmiszerek[0];
            for (int i = 0; i < elelmiszerek.Count; i++)
                if (elelmiszerek[i].feherje > max.feherje)
                    max = elelmiszerek[i];

            Console.WriteLine("3. feladat");

            int db = 0;
            double osszeg = 0;
            for(int i=0; i<elelmiszerek.Count; i++)
                if( string.Compare(elelmiszerek[i].kategoria,"Gabonafélék") == 0)
                {
                    ++db;
                    osszeg += elelmiszerek[i].feherje;
                }

            Console.WriteLine("gab átl feh. tart: " + Math.Round(osszeg / db, 2));

            Console.WriteLine("6. feladat");
            string be = Console.ReadLine();

            foreach (Elelmiszer e in elelmiszerek)
                if (e.nev.ToLower().Contains(be.ToLower()) == true)
                    Console.WriteLine("\tNév: "+e.nev);

            Console.WriteLine("7. feladat");

            Dictionary<string, int> stat = new Dictionary<string, int>();
            
            foreach (Elelmiszer e in elelmiszerek)
                if (stat.ContainsKey(e.kategoria))
                    ++stat[e.kategoria];
                else
                    stat.Add(e.kategoria, 1);

            foreach (KeyValuePair<string, int> e in stat)
                if (e.Value < 10)
                    Console.WriteLine("\t" + e.Key + " - " + e.Value);

            Console.WriteLine("8. feladat");

            StreamWriter sw = new StreamWriter("gabonafelek.txt");

            sw.WriteLine(elelmiszerek[0].kategoria+";"+elelmiszerek[1].feherje);
            sw.Flush();
            sw.Close();
        }
    }
}
