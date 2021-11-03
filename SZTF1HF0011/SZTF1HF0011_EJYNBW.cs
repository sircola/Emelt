using System;

namespace SZTF1HF0011
{
    class Program
    {
        static void Csere( string S, string P, int N, int MIN, int MAX) {

            if (N < 0)
                return;

            if (N == 0)
            {
                if( S.Length < MIN)
                    return;

                for (int i = MIN-1; i < MAX; i++)
                    if (i < S.Length)
                        Console.Write(S[i]);
                    else
                        Console.Write('-');

                return;
            }

            string s = "";

            for (int i = 0; i < P.Length; i++)
            {
                if (P[i] == '$')
                    s += S;
                else
                    s += P[i];
            }
            S = s;

            if(S.Length <= MAX )
                Csere(S, P, --N, MIN, MAX);
            else 
                Csere(S, P, 0, MIN, MAX);
        }

        static void Main(string[] args)
        {
            /*
            string S = "x";
            string P = "$a$b$c$";
            int N = 999999999;
            int MIN = 33;
            int MAX = 65;
            */

            /*
            string S = "oe";
            string P = "$nik";
            int N = 10;
            int MIN = 10;
            int MAX = 40;
            */

            /*
            string S = "e";
            string P = "$agl$";
            int N = 2;
            int MIN = 1;
            int MAX = 15;
            */

            /*
            string S = "b";
            string P = "$o$";
            int N = 10;
            int MIN = 1;
            int MAX = 3;
            */
            /*
            string S = "a";
            string P = "$";
            int N = 1;
            int MIN = 1;
            int MAX = 1;
            */

            /*
            string S = "0";
            string P = "1";
            int N = 1000000000;
            int MIN = 10000;
            int MAX = 10009;
            */

            /*
            string S = "0";
            string P = "$1$";
            int N = 576;
            int MIN = 1;
            int MAX = 10;
            */

            string S = Console.ReadLine();
            string P = Console.ReadLine();
            int N = int.Parse(Console.ReadLine());
            int MIN = int.Parse(Console.ReadLine());
            int MAX = int.Parse(Console.ReadLine());

            Csere(S,P,N,MIN,MAX);

            // Console.WriteLine("\nxaxbxcxaxaxbxcxbxaxbxcxcxaxbxcxbx");
            // Console.WriteLine("\niknikniknikniknikniknik--------");
            // Console.ReadLine();
        }
    }
}
