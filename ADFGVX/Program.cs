using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADFGVX
{
    class ADFGVXrejtjel
    {
        private char[,] Kodtabla;
        private string Uzenet;
        private string Kulcs;

        public string AtalakitottUzenet()
        {
            // 5. feladat:
            string ujuzenet = Uzenet.Replace(" ", "");
            int ujhossz = ujuzenet.Length;
            while (ujhossz % Kulcs.Length != 0) { 
                ++ujhossz;
                ujuzenet += "x";
            }

            return ujuzenet;
        }

        public string Betupar( char k)
        {
            string[] adfgvx = new string[] { "A", "D", "F", "G", "V", "X" };

            for( int sorIndex=0; sorIndex<=5; sorIndex++)
            {
                for( int oszlopIndex=0; oszlopIndex <=5; oszlopIndex++)
                {
                    if (Kodtabla[sorIndex, oszlopIndex] == k)
                        return adfgvx[sorIndex] + adfgvx[oszlopIndex];
                }
            }

            return "hiba";
        }


        public string Kodszoveg()
        {
            string uzenet = "";

            foreach( var c in AtalakitottUzenet())
                uzenet += Betupar(c);

            return uzenet;
        }

        public string KodoltUzenet()
        {
            string kodszoveg = Kodszoveg();
            int sorokSzama = kodszoveg.Length / Kulcs.Length;
            int oszlopokSzama = Kulcs.Length;
            char[,] m = new char[sorokSzama, oszlopokSzama];
            int index = 0;
            for (int sor = 0; sor < sorokSzama; sor++)
            {
                for (int oszlop = 0; oszlop < oszlopokSzama; oszlop++)
                {
                    m[sor, oszlop] = kodszoveg[index++];
                }
            }

            string kodoltUzenet = "";
            for (char ch = 'A'; ch <= 'Z'; ch++)
            {
                int oszlopIndex = Kulcs.IndexOf(ch);
                if (oszlopIndex != -1)
                {
                    for (int sorIndex = 0; sorIndex < sorokSzama; sorIndex++)
                    {
                        kodoltUzenet += m[sorIndex, oszlopIndex];
                    }
                }
            }
            return kodoltUzenet;
        }

        public ADFGVXrejtjel(string kodtablaFile, string uzenet, string kulcs)
        {
            Uzenet = uzenet;
            Kulcs = kulcs;

            Kodtabla = new char[6, 6];
            int sorIndex = 0;
            foreach (var sor in System.IO.File.ReadAllLines(kodtablaFile))
            {
                for (int oszlopIndex = 0; oszlopIndex < sor.Length; oszlopIndex++)
                {
                    Kodtabla[sorIndex, oszlopIndex] = sor[oszlopIndex];
                }
                sorIndex++;
            }
        }
    }


    class Program
    {
       
        static void Main(string[] args)
        {
            Console.WriteLine("2. feladat");
            Console.Write("kérem a kulcsot: [HOLD]: ");
            string kulcs = Console.ReadLine().ToUpper();
            if (kulcs == "")
                kulcs = "HOLD";
            Console.Write("kérem az üzenete: [szeretem a csokit]:");
            string uzenet = Console.ReadLine().ToLower();
            if (uzenet == "")
                uzenet = "szeretem a csokit";

            ADFGVXrejtjel rejtjel = new ADFGVXrejtjel("kodtabla.txt", uzenet, kulcs);

            Console.WriteLine($"5. feladat: Az átlakított üzenet: {rejtjel.AtalakitottUzenet()}");

            Console.WriteLine($"6. feladat: s->{rejtjel.Betupar('s')}, x->{rejtjel.Betupar('x')}");

            Console.WriteLine($"7. felatad: a kódszöveg: {rejtjel.Kodszoveg()}");

            Console.WriteLine($"8. feladat: a kódolt üzenet: {rejtjel.KodoltUzenet()}");

            Console.WriteLine("nyomjon ENTER-t.");
            Console.ReadKey();
        }
    }
}
