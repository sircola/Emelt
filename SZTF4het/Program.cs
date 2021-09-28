using System;

namespace SZTF
{
    class Program
    {
        static int[,] GenFelt(int sor, int oszlop, int min, int max)
        {
            var rand = new Random();
            int[,] m = new int[sor, oszlop];

            for (int i = 0; i < m.GetLength(0); i++)
            {
                for (int j = 0; j < m.GetLength(1); j++)
                {
                    m[i, j] = rand.Next(min, max);
                }
            }

            return m;
        }

        static void FelsöHáromszög(int[,] m)
        {
            for (int i = 0; i < m.GetLength(0); i++)
            {
                for (int j = 0; j < m.GetLength(1); j++)
                {
                    if (i <= j)
                    {
                        Console.Write(m[i, j]);
                        Console.Write(new string(' ', 5 - m[i, j].ToString().Length));
                    }
                    else
                        Console.Write(new string(' ', 5));
                }
            }
        }

        static void Kiir(int i)
        {
            Console.Write(i);
            Kiir(++i);
        }

        static int[] Halak(int[,] m)
        {
            int[] halak = new int[m.GetLength(0)];

            for (int i = 0; i < m.GetLength(0); i++)
            {
                halak[i] = 0;
                for (int j = 0; j < m.GetLength(1); j++)
                {
                    halak[i] += m[i, j];
                }
            }

            // 13. feladat utsó kérdés
            int max = halak[0];
            int melyik = 0;
            for (int i = 1; i < halak.Length; i++)
                if (max < halak[i])
                {
                    melyik = i;
                    max = halak[i];
                }
            Console.WriteLine("melyik: " + melyik);


            max = 0;
            int idx = 0;
            for (int i = 0; i < m.GetLength(0); i++)
            {
                int act = 0;
                for (int j = 0; j < m.GetLength(1); j++)
                {
                    act += m[i, j];
                }

                if (act > max)
                {
                    idx = i;
                    max = act;
                }
            }

            return new int[] { idx, max };
        }

        static void Main(string[] args)
        {
            int[,] m = GenFelt(10, 10, 0, 100);
            FelsöHáromszög(m);
            Halak(m);
        }
    }
}
