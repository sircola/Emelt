using System;
using System.IO;

namespace SZTF1HF0012
{
    class Program
    {
        static int N;
        static StreamReader sr;
        static int[] szintek = new int[0];

        static void NövelSzint( int szint )
        {
            if( szintek.Length < szint+1 )
            {
                int[] új = new int[szint + 1];
                for (int i = 0; i < szintek.Length; i++)
                    új[i] = szintek[i];
                szintek = új;
            }
            szintek[szint] = szintek[szint] + 1;
        }

        static int Számol()
        {
            File.Delete("output.txt");
            for (int i = 0; i < szintek.Length; i++)
            {
                File.AppendAllText("output.txt", "" + szintek[i].ToString() + "\n");
            }
            return szintek.Length;
        }

        static void KiértékelElse( int szint )
        {
            string s = sr.ReadLine().Trim();
            --N;

            if (s == "endif")
                NövelSzint(szint);
            else
            if (s == "if")
                KiértékelIf(szint+1);
        }

        static void KiértékelIf( int szint )
        {
            string s = sr.ReadLine().Trim();
            --N;

            if (s == "else")
            {
                NövelSzint(szint);
                KiértékelElse(szint);
            }
            else
            if (s == "if")
                KiértékelIf(szint+1);
        }

        static void Main(string[] args)
        {
            sr = new StreamReader("input1.txt");

            N = int.Parse(sr.ReadLine().Trim());

            sr.ReadLine().Trim();
            --N;

            NövelSzint(0);

            while (N > 0)
            {
                KiértékelIf(0);
                if ( N>0 )
                    KiértékelElse(0);
            }

            sr.Close();

            Számol();
        }
    }
}
