using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Játszma5
{
    class Játék // 6.a feladat
    {
        public string Állás { get; private set; } // 6.b feladat
        public string AdogatóJátékos { get; private set; } // 6.b feladat
        public string FogadóJátékos { get; private set; } // 6.b feladat

        public Játék(string adogatóJátékos, string fogadóJátékos, string állás) // 6.c. feladat: konstruktor
        {
            AdogatóJátékos = adogatóJátékos; // 6.c feladat
            FogadóJátékos = fogadóJátékos; // 6.c feladat
            Állás = állás; // 6.c további inicializáció
        }

        public void Hozzáad(char labdamenetNyertese)  // 6.d feladat
        {
            Állás += labdamenetNyertese; // 'A' vagy 'F'
        }

        // 6.e feladat
        private int NyertLabdamenetekSzáma(char ki) => Állás.Count(x => x == ki); // ki-'A' vagy 'F'

        // 6.f feladat
        public bool JátékVége {
            get {
                int nyertAdogató = NyertLabdamenetekSzáma('A');
                int nyertFogadó = NyertLabdamenetekSzáma('F');
                int különbség = Math.Abs(nyertAdogató - nyertFogadó);
                return (nyertAdogató >= 4 || nyertFogadó >= 4) && különbség >= 2;
            }
        }


        public string NyertesJátékosNeve // 6.g opcionális feladat
        {
            get {
                if (!JátékVége) return "Nincs nyertes!"; // nincs még nyertes, mert folyamatban van a játék
                return NyertLabdamenetekSzáma('A') > NyertLabdamenetekSzáma('F') ? AdogatóJátékos : FogadóJátékos;
            }
        }
    }

    class Játszma5
    {
        static void Main()
        {
            // 2. feladat:
            Queue<string> labdamenetek = new Queue<string>(File.ReadLines("labdamenetek5.txt"));

            Console.WriteLine($"3. feladat: Labdamenetek száma: {labdamenetek.Count}");
            Console.WriteLine($"4. feladat: Az adogató játékos {((double)labdamenetek.Count(x => x == "A") / labdamenetek.Count) * 100}%-ban nyerte meg a labdameneteket.");

            // 5. feladat:
            int leghosszabbSorozat = 0;
            int aktSorozat = 0;
            foreach (var i in labdamenetek)
            {
                if (i == "A") aktSorozat++;
                else
                {
                    if (aktSorozat > leghosszabbSorozat) leghosszabbSorozat = aktSorozat;
                    aktSorozat = 0;
                }
            }
            // Ha a leghosszabb sorozat az állomány végén volt:
            if (aktSorozat > leghosszabbSorozat) leghosszabbSorozat = aktSorozat;
            Console.WriteLine($"5. feladat: Leghosszabb sorozat: {leghosszabbSorozat}");

            // 7. feladat:
            Játék PróbaJáték = new Játék("Mahut", "Isner", "FAFAA");
            PróbaJáték.Hozzáad('A');
            Console.WriteLine("7. feladat: A próba játék");
            Console.WriteLine($"\tÁllás: {PróbaJáték.Állás}");
            Console.WriteLine($"\tBefejeződött a játék: {(PróbaJáték.JátékVége ? "igen" : "nem")}");


            // 8. feladat:
            List<Játék> befejezettJátékok = new List<Játék>();
            Játék aktJáték = null;
            string aktAdogató = "Isner"; // első játékot Isner kezdte az 5. játszmában
            string aktFogadó = "Mahut";

            while (labdamenetek.Count > 0) // 8. feladat
            {
                if (aktJáték == null) aktJáték = new Játék(aktAdogató, aktFogadó, "");
                char labdamenetNyertese = labdamenetek.Dequeue()[0];
                aktJáték.Hozzáad(labdamenetNyertese);
                if (aktJáték.JátékVége) // ha befejeződött a játék
                {
                    befejezettJátékok.Add(aktJáték); // menteni kell a befejezet játékot a listába
                    // új játék előkészítése:
                    aktAdogató = aktJáték.FogadóJátékos; // az új adogató az előző játék fogadó játékosa
                    aktFogadó = aktJáték.AdogatóJátékos;
                    aktJáték = null;
                }
            }

            Console.WriteLine("9. feladat: Az 5. játszma végeredménye: ");
            int nyertJátékMahut = befejezettJátékok.Count(x => x.NyertesJátékosNeve == "Mahut");
            int nyertJátékIsner = befejezettJátékok.Count(x => x.NyertesJátékosNeve == "Isner");
            Console.WriteLine($"\tMahut: {nyertJátékMahut}");
            Console.WriteLine($"\tIsner: {nyertJátékIsner}");
            Console.ReadKey();
        }
    }
}
