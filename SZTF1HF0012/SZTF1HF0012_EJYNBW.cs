using System;
using System.IO;


namespace SZTF1HF0012
{
    class Program
    {
        static int variáció = 1;

         static void KiértékelIf( StreamReader sr )
        {
            ++variáció;

            string s = sr.ReadLine().Trim();

            if (s == "else")
                KiértékelIf(sr);

            if (s == "if")
                KiértékelIf(sr);

            if ( s == "endif")
                return;

 
        }

        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader("input.txt");

            int N = int.Parse(sr.ReadLine());

            sr.ReadLine();

            if( sr.ReadLine() == "if" )
                KiértékelIf(sr);

            sr.Close();

            StreamWriter sw = new StreamWriter("output.txt");
            
            sw.WriteLine(variáció);
            
            sw.Close();
        }
    }
}
