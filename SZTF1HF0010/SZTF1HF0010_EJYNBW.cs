using System;

namespace SZTF1HF0010
{
    class Program
    {
        static void Main()
        {
            string[] a = Console.ReadLine().Split(' ');
            int M = int.Parse(a[0]); 
            int N = int.Parse(a[1]);

            string[] T = new string[M];

            for (int i = 0; i < T.Length; i++)
                T[i] = Console.ReadLine();

            int sor = 0;
            int oszlop = 0;
            int utolsósor = M - 1;
            int utolsóoszlop = N - 1;
            int j;

            while ( sor <= utolsósor && oszlop <= utolsóoszlop )
            {
                if (oszlop <= utolsóoszlop) {
                    for (j = utolsósor; j >= sor; j--)
                        Console.Write(T[j][oszlop]);
                    ++oszlop;
                }

                for (j = oszlop; j <= utolsóoszlop; j++)
                    Console.Write(T[sor][j]);
                ++sor;

                for (j = sor; j <= utolsósor; j++)
                    Console.Write(T[j][utolsóoszlop]);
                --utolsóoszlop;

                if( sor<=utolsósor)
                {
                    for (j = utolsóoszlop; j >= oszlop; j--)
                        Console.Write(T[utolsósor][j]);
                    --utolsósor;
                }
            }
        }
    }
}
