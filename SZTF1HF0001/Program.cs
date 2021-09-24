using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SZTF1HF0001
{
    class Program
    {
        static void Main(string[] args)
        {
            int S = 491;
            int P = 268;
            int[] szamjegyek = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            for( int i=S; i<S+(P*2); i+=2)
            {
                int szam = i;
                while (szam > 0)
                {
                    ++szamjegyek[szam % 10];
                    szam = (int)(szam / 10);
                }
            }

            for( int i=0; i<10; i++)
            {
                Console.Write(szamjegyek[i]);
                if (i < 9)
                    Console.Write(", ");
            }
        }
    }
}
