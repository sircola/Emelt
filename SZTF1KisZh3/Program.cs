using System;

namespace EJYNBW_KIRSCHNER
{
    class Program
    {
        static Random rng = new Random();

        static int []Create()
        {
            int[] T = { -9, 5, -7, -3, 8, -10, 0, -2, -8, 4 };
            return T;
        }

        static int[] Create( int N)
        {
            int[] T = new int[N];

            for (int i = 0; i < N; i++)
            {
                T[i] = rng.Next(200) - 100;
            }
        
            return T;
        }

        static int AbsMax( int N)
        {
            int[] T = new int[N];

            for (int i = 0; i < N; i++)
            {
                T[i] = rng.Next(200) - 100;
            }

            int max = 0;

            for (int i = 0; i < T.Length; i++)
            {
                if (Math.Abs(T[i]) > Math.Abs(T[max]))
                    max = i;

            }

            return max;
        }

        static void Rendez( int[] T )
        {
            for (int j = 1; j < T.Length; j++)
            {
                for (int i = j-1; i < T.Length-1; i++)
                {
                    if( T[i] > T[i+1] )
                    {
                        int seged = T[i];
                        T[i+1] = T[i];
                        T[i] = seged;
                    }
                }

            }
        }


        static void Main(string[] args)
        {
            Console.WriteLine(Create()[4]);
            Console.WriteLine(Create(10)[4]);
            Console.WriteLine(AbsMax(10));
            Rendez(Create(10));
        }
    }
}
