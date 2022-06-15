using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Felfedezesek
{
    class Elem
    {
        public string Év;
        public string Név;
        public string Vegyjel;
        public int Rendszám;
        public string Felfedező;

        public Elem(string sor)
        {
            string[] adatok = sor.Split(';');
            Év = adatok[0];
            Név = adatok[1];
            Vegyjel = adatok[2];
            Rendszám = int.Parse(adatok[3]);
            Felfedező = adatok[4];
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

            List<Elem> elemek = new List<Elem>();

            StreamReader sr = new StreamReader("felfedezesek.txt");

            sr.ReadLine();
            while (!sr.EndOfStream)
            {
                elemek.Add(new Elem(sr.ReadLine()));
            }

            sr.Close();

            Console.WriteLine("5. feladat: Elemek száma: " + elemek.Count);

            int db = 0;
            foreach (Elem e in elemek)
                if (e.Felfedező.Contains("W.Ramsay"))
                    ++db;

            Console.WriteLine("6. feladat: William Ramsay: "+db+" db");

            
            Console.Write("7. feladat: Kérek egy vegyjelet: ");
            string vegyjel = Console.ReadLine().ToLower();

            Elem er = null;
            Console.WriteLine("8. feladat: Keresés");
            foreach (Elem e in elemek)
                if (e.Vegyjel.ToLower().Contains(vegyjel))
                    er = e;
            if (er!=null)
                Console.WriteLine("\tFelfedezés: " + er.Év);
            else
                Console.WriteLine("\tNincs ilyen elem az adatforrásban!");

            Console.WriteLine("9. feladat: Statisztika 19. század");

            Dictionary<string, int> stat = new Dictionary<string, int>();

            foreach (Elem e in elemek)
                if (e.Év != "Ókor")
                {
                    if (stat.ContainsKey(e.Év))
                        ++stat[e.Év];
                    else
                        stat.Add(e.Év, 1);
                }

            foreach (KeyValuePair<string, int> e in stat)
                if (int.Parse(e.Key) >= 1801 && int.Parse(e.Key) <= 1900)
                    Console.WriteLine("\t" + e.Key + ": " + e.Value + "db");

            // 10. feladat
            StreamWriter sw = new StreamWriter("felfedezesek_rendszama.txt");

            sw.WriteLine("Év;Név;Vegyjel;Rendszám;Felfedező");
            for(int i=1; i<=118; i++)
                foreach (Elem e in elemek)
                    if (e.Rendszám == i)
                        sw.WriteLine(e.Év + ";" + e.Név + ";" + e.Vegyjel + ";" + e.Rendszám + ";" + e.Felfedező);

            sw.Flush();
            sw.Close();

            Console.ReadLine();
        }
    }
}
