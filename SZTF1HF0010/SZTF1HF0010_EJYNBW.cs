using System;

namespace SZTF1HF0010
{
    class Program
    {
        static void Main()
        {
 /*
            string[] a = Console.ReadLine().Split(' ');
            int M = int.Parse(a[0]); 
            int N = int.Parse(a[1]);

            string[] T = new string[M];

            for (int i = 0; i < T.Length; i++)
                T[i] = Console.ReadLine();
*/
 /*
            int M = 23;
            int N = 15;
            string[] T =
            { "XrNTZaLkRx0xEVz",
"vWwc4NEgEpBO8kT",
"K7kiBiohYoeUWy6",
"Okb8LMyOPMib8un",
"lDZYNTdljh6FyNP",
"THIyAhD0y7ciuvi",
"6gNA3BVYLcAZRQE",
"7Z10TAtzT6HCqRA",
"BIP0aQRYKdYJWmZ",
"iM9UZchuCHDGTtl",
"Zu6aBS9QGNf1gMV",
"RwdTOVtwRnjedix",
"aYupetCMK78Vt6K",
"b0HGQ6s0FpvYfMh",
"DWy8DhwRyseKsLF",
"00q6cjwwsqIXOZJ",
"vnZtucmkstE89eB",
"mZeZovy8AY6p6IV",
"jISPXRKZhtEFXDx",
"anhQ3IJPBdWepun",
"MgCxaWgkhJB0WwK",
"PAv2NJ8fEczThB0",
"CluTHn3ptBT306r"};
*/

            int M = 12;
            int N = 11;
            string[] T = {
"0xWTy7MSv9D",
"k6EfxtlQCCw",
"BEEuykIbi7t",
"4ZXkZVVp4rl",
"eNVmQ6lchOM",
"uWhf4yrajRv",
"R1EidtqVcy4",
"gqRyctBP5PE",
"hv12KQmYU7u",
"IWTHyTSMmBe",
"pFRnvmXytLG",
"cJ8haIzzeDW"
            };

            int sor = 0;
            int oszlop = 0;
            int utolsósor = M - 1;
            int utolsóoszlop = N - 1;
            int j;

            while (sor <= utolsósor && oszlop <= utolsóoszlop)
            {
                if (oszlop <= utolsóoszlop)
                {
                    for (j = utolsósor; j >= sor; j--)
                    {
                        Console.Write(T[j][oszlop]);
                        char[] s = T[j].ToCharArray();
                        s[oszlop] = 'X';
                        T[j] = new string(s);
                    }
                    ++oszlop;
                }

                if (sor <= utolsósor) 
                { 
                    for (j = oszlop; j <= utolsóoszlop; j++)
                        Console.Write(T[sor][j]);
                    ++sor;
                }

                if (oszlop <= utolsóoszlop)
                {
                    for (j = sor; j <= utolsósor; j++)
                        Console.Write(T[j][utolsóoszlop]);
                    --utolsóoszlop;
                }

                if( sor<=utolsósor )
                {
                    for (j = utolsóoszlop; j >= oszlop; j--)
                        Console.Write(T[utolsósor][j]);
                    --utolsósor;
                }
            }

            Console.WriteLine("\n");
            foreach (string s in T)
                Console.WriteLine(s);
            Console.ReadLine();
        }
    }
}
