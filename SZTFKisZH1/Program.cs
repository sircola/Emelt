using System;

namespace EJYNBW_KIRSCHNER
{
    class Program
    {
        static void Main(string[] args)
        {
            var rand = new Random();
            int num = rand.Next(0 + 1, 1001);
            int[] T = new int[num];

            for (int i = 0; i < num; i++)
                T[i] = rand.Next(-100, 0);

            int M = rand.Next(-100, 0);

            int nincs = 0;
            for (int i = 0; i < num; i++)
            {
                if (T[i] < M - 5 || T[i] > M + 5)
                    ++nincs;
            }

            Console.WriteLine(nincs);
        }
    }
}
