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

            S = P.Replace("$", S);

            if(S.Length <= MAX || N < 300 )
                Csere(S, P, --N, MIN, MAX);
            else 
                Csere(S, P, 0, MIN, MAX);
        }

        static void Main(string[] args)
        {
            /*
            string S = Console.ReadLine();
            string P = Console.ReadLine();
            int N = int.Parse(Console.ReadLine());
            int MIN = int.Parse(Console.ReadLine());
            int MAX = int.Parse(Console.ReadLine());
            */
            /*
            string S = "uuf2";
            string P = "8$ukt$$";
            int N = 5;
            int MIN = 3;
            int MAX = 96;
*/
            string S = "xp2mzx2t9";
            string P = "b7lsa$$i6$";
            int N = 4;
            int MIN = 7;
            int MAX = 59;

            if (!P.Contains("$"))
            {
                S = P;
                N = 0;
            }

            Csere(S,P,N,MIN,MAX);

            Console.WriteLine();
            // Console.WriteLine("888uuf2uktuuf2uuf2ukt8uuf2uktuuf2uuf28uuf2uktuuf2uuf2ukt88uuf2uktuuf2uuf2ukt8uuf2uktuuf2uuf28u");
            Console.WriteLine("7lsab7lsab7lsaxp2mzx2t9xp2mzx2t9i6xp2mzx2t9b7lsaxp2mz");
            Console.ReadKey();
        }
    }
}
