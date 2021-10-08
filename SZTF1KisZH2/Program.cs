using System;

namespace EJYNBW_KIRSCHNER
{
    class Program
    {
        static int[,] Feltölt(int N, Random rng)
        {
            int[,] T = new int[N, N];

            for (int i = 0; i < T.GetLength(0); i++)
                for (int j = 0; j < T.GetLength(1); j++)
                    T[i, j] = rng.Next(0, 1000);

            return T;
        }

        static double[] Átlag(int[,] T)
        {
            double[] átlag = new double[T.GetLength(0)];

            for (int i = 0; i < T.GetLength(0); i++)
            {
                for (int j = 0; j < T.GetLength(1); j++)
                    átlag[i] += T[i, j];
                átlag[i] /= T.GetLength(1);
            }
            return átlag;
        }

        static void Átméretez(ref int[] T, int Y)
        {
            int[] temp = new int[Y];

            for (int i = 0; i < Y && i < T.Length; i++)
                temp[i] = T[i];

            T = temp;

            return;
        }

        static void Main(string[] args)
        {
            Random rng = new Random();

            int[,] T = Feltölt(10, rng);
            double[] átlag = Átlag(T);

            int[] m = new int[10];
            Átméretez(ref m, 13);

        }
    }
}
