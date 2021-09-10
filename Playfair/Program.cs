using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace playfair
{
    class PlayfairKodolo
    {
        public List<string> Kodtabla;

        public PlayfairKodolo( string filenev)
        {
            Kodtabla = new List<string>();
            StreamReader sr = new StreamReader(filenev);
            while( !sr.EndOfStream)
                Kodtabla.Add(sr.ReadLine());
            sr.Close();
        }

        public int SorIndex( char betu )
        {
            for (int i = 0; i < Kodtabla.Count; i++)
                foreach (var c in Kodtabla[i])
                    if (betu == c)
                        return i;

            return -1;
        }

        public int OszlopIndex( char betu )
        {
            for (int i = 0; i < Kodtabla.Count; i++)
                for (int j = 0; j < Kodtabla[i].Length; j++)
                    if (Kodtabla[i][j] == betu)
                        return j;

            return -1;
        }

        public string KodolAzonosSorban( int sor, int oszlop1, int oszlop2)
        {
            int kodoszlop1 = oszlop1 + 1;
            if (kodoszlop1 >= 5)
                kodoszlop1 = 0;
            int kodoszlop2 = oszlop2 + 1;
            if (kodoszlop2 >= 5)
                kodoszlop2 = 0;

            string kod = "" + Kodtabla[sor][kodoszlop1] + Kodtabla[sor][oszlop2]; 
            return kod;
        }

        public string KodolAzonosOszlopban( int oszlop, int sor1, int sor2)
        {
            int kodsor1 = sor1 + 1;
            if (kodsor1 >= 5)
                kodsor1 = 0;
            int kodsor2 = sor2 + 1;
            if (kodsor2 >= 5)
                kodsor2 = 0;

            string kod = "" + Kodtabla[kodsor1][oszlop] + Kodtabla[kodsor2][oszlop];
            return kod;
        }

        public string KodolTeglalap( int sor1, int oszlop1, int sor2, int oszlop2)
        {
            return ""+ Kodtabla[sor1][oszlop2] + Kodtabla[sor2][oszlop1];
        }

        public string KodolBetupar( string par )
        {
            char betu1 = par[0];
            char betu2 = par[1];
            int sor1 = SorIndex(betu1);
            int sor2 = SorIndex(betu2);
            int oszlop1 = OszlopIndex(betu1);
            int oszlop2 = OszlopIndex(betu2);
            if (sor1 == sor2) return KodolAzonosSorban(sor1, oszlop1, oszlop2);
            if (oszlop1 == oszlop2) return KodolAzonosOszlopban(oszlop1, sor1, sor2);
            return KodolTeglalap(sor1,oszlop1,sor2,oszlop2);
        }
    }   


    class Program
    {
        static void Main(string[] args)
        {
            PlayfairKodolo pfk = new PlayfairKodolo("kulcstabla.txt");

            Console.Write("6. feldat - Kérek egy nagy betűt: ");            
            char c = Console.ReadLine()[0];
            Console.WriteLine($"A karakter sorának indexe: {pfk.SorIndex(c)}");
            Console.WriteLine($"A karakter oszlopának indexe:  {pfk.OszlopIndex(c)}");

            Console.Write("8. feladat - Kérek egy karatkerpárt: ");
            Console.WriteLine($"Kódolva: {pfk.KodolBetupar(Console.ReadLine())}");

            // Console.WriteLine("nyomjon ENTER-t.");
            Console.ReadKey();
        }
    }
}
