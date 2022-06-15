using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Pars2012
{
    // 2. feladat: Versenyző osztály + adattagok
    class Versenyző
    {
        public string Név { get; private set; }
        public char Csoport { get; private set; }
        private string NemzetÉsKód { get; set; }
        public string Sorozat { get; set; }
        public double[] Dobások { get; private set; }

        // 8. feladat: Nemzet kódtag
        public string Nemzet
        {
            get
            {
                string[] m = NemzetÉsKód.Split(' ');
                return String.Join(" ", m.Take(m.Length - 1));
            }
        }

        // 8. feladat: Kód kódtag, zárójelek eltávolítása
        public string Kód => NemzetÉsKód.Split(' ').Last().Replace("(", "").Replace(")", "");
        
        public bool Bejutott78m => Dobások.Contains(-2);

        // 7. feladat: Legnagyobb dobás, vagy -1 ha nem volt érvényes kísérlet:
        public double Eredmény
        {
            get
            {
                double max = Dobások[0];
                foreach (var i in Dobások.Skip(1)) if (i > max) max = i;
                return max;
            }
        }

        // 3. feladat: Konstruktor kódolása
        public Versenyző(string sor)
        {
            string[] m = sor.Split(';');
            Név = m[0];
            Csoport = char.Parse(m[1]);
            NemzetÉsKód = m[2];
            Sorozat = $"{m[3]};{m[4]};{m[5]}";
            Dobások = new double[3];
            for (int i = 0; i < Dobások.Length; i++)
            {
                if (m[i + 3] == "X") Dobások[i] = -1.0; // érvénytelen kísérlet: -1.0
                else if (m[i + 3] == "-") Dobások[i] = -2.0; // már nem dobott, mert bejutott 78m feletti korábbi dobással: -2.0
                else Dobások[i] = double.Parse(m[i + 3]);
            }
        }
    }
    class Pars2012
    {
        static void Main()
        {
            // 4. feladat: Adatok beolvasása + tárolása:
            List<Versenyző> versenyzők = new List<Versenyző>();
            foreach (var i in File.ReadAllLines("Selejtezo2012.txt").Skip(1))
            {
                versenyzők.Add(new Versenyző(i));
            }

            Console.WriteLine($"5. feladat: Versenyzők száma a selejtezőben: {versenyzők.Count} fő");

            // 6. feladat: 78,00 méter feletti eredménnyel továbbjutott
            int fő78m = 0;
            foreach (var i in versenyzők) if (i.Bejutott78m) fő78m++;
            Console.WriteLine($"6. feladat: 78,00 méter feletti eredménnyel továbbjutott: {fő78m} fő");

            Console.WriteLine("9. feladat: A selejtező nyertese:");
            Versenyző nyertes = versenyzők.First();
            foreach (var v in versenyzők.Skip(1))
            {
                if (v.Eredmény > nyertes.Eredmény) nyertes = v;
            }
            Console.WriteLine($"\tNév: {nyertes.Név}");
            Console.WriteLine($"\tCsoport: {nyertes.Csoport}");
            Console.WriteLine($"\tNemzet: {nyertes.Nemzet}");
            Console.WriteLine($"\tNemzet kód: {nyertes.Kód}");
            Console.WriteLine($"\tSorozat: {nyertes.Sorozat}");
            Console.WriteLine($"\tEredmény: {nyertes.Eredmény}");

            // 10. feladat: Dontos2012.txt
            List<string> ki = new List<string>();
            ki.Add($"Helyezés;Név;Csoport;Nemzet;NemzetKód;Sorozat;Eredmény");
            for (int i = 1; i < 13; i++)
            {
                Versenyző legjobb = versenyzők.First();
                foreach (var v in versenyzők.Skip(1))
                {
                    if (v.Eredmény > legjobb.Eredmény) legjobb = v;
                }
                ki.Add($"{i};{legjobb.Név};{legjobb.Csoport};{legjobb.Nemzet};{legjobb.Kód};{legjobb.Sorozat};{legjobb.Eredmény}");
                versenyzők.Remove(legjobb);
            }
            File.WriteAllLines("Dontos2012.txt", ki, Encoding.UTF8);

            Console.ReadKey();
        }
    }
}
