using System;
using System.IO;

namespace SZTF1HF0012
{
    class Program
    {
        static int N;
        static int variáció = 0;
        static StreamReader sr;

        static void KiértékelElse()
        {
            string s = sr.ReadLine();
            --N;

            if (s == "endif")
                ++variáció;
            else
            if (s == "if")
                KiértékelIf();
        }

        static void KiértékelIf()
        {
            string s = sr.ReadLine();
            --N;

            if (s == "else")
            {
                ++variáció;
                KiértékelElse();
            }
            else
            if (s == "if")
                KiértékelIf();
            else
            if (s == "end" && variáció == 0)
                ++variáció;
        }

        static void Main(string[] args)
        {
            sr = new StreamReader("input.txt");

            N = int.Parse(sr.ReadLine());

            sr.ReadLine();
            --N;

            while( N>0 )
                KiértékelIf();

            sr.Close();

            File.WriteAllText("output.txt", variáció.ToString());
        }
    }
}
