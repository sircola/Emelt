using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Orokles
{
    class Program
    {

        static void Main(string[] args)
        {
            


            Alkalmazott[] alkalmazottak = new Alkalmazott[9];
            alkalmazottak[0] = new Menedzser("Kürti Zsolt", 2001, 800000, 4);
            alkalmazottak[1] = new Menedzser("Varga Klára", 2002, 650000, 300);
            alkalmazottak[2] = new Uzletkoto("Borbás Tamás", 2006, 220000, 2);
            alkalmazottak[3] = new Uzletkoto("Kártyás Eszter", 2008, 240000, 3);
            alkalmazottak[4] = new Uzletkoto("Zámbori Zalán", 2010, 290000, 1);
            alkalmazottak[5] = new Uzletkoto("Varga Péter", 2015, 420000, 4);
            alkalmazottak[6] = new Fejleszto("Nagy Géza", 2016, 320000, 6, 1000);
            alkalmazottak[7] = new Fejleszto("Szabó János", 2012, 280000, 3, 800);
            alkalmazottak[8] = new Fejleszto("Varga Dániel", 2019, 260000, 2, 600);

            //Console.WriteLine(LegjobbanKereso(alkalmazottak));

            for (int i = 0; i < alkalmazottak.Length; i++)
            {
                Console.WriteLine(alkalmazottak[i].GetHashCode());

            }

            
            

        }

        static string LegjobbanKereso(Alkalmazott[] alkalmazottak)
        {
            int maxindex = 0;
            for (int i = 1; i < alkalmazottak.Length; i++)
            {
                if (alkalmazottak[i].Jutalek > alkalmazottak[maxindex].Jutalek)
                {
                    maxindex = i;
                }
            }
            return alkalmazottak[maxindex].Nev;
        }

        

    }
}
