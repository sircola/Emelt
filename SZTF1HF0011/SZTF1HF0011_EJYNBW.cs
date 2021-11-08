using System;

namespace SZTF1HF0011
{
    class Program
    {
        static void Csere( string S, string P, int N, int MIN, int MAX) {

            if (N == 0)
            {
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
            string S = Console.ReadLine();
            string P = Console.ReadLine();
            int N = int.Parse(Console.ReadLine());
            int MIN = int.Parse(Console.ReadLine());
            int MAX = int.Parse(Console.ReadLine());

            if (!P.Contains("$"))
            {
                S = P;
                N = 0;
            }
             
            Csere(S,P,N,MIN,MAX);
        }
    }
}
