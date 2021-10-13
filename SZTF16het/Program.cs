using System;

namespace SZTF16het
{
    class Program
    {
        static Random rng = new Random();

        static void Main(string[] args)
        {
            string s;
            string t = "asdasdasdasd";

            Console.WriteLine(t.ToUpper());
            Console.WriteLine(t.PadLeft(2, '-'));
            Console.WriteLine(t.Trim('a', 's'));
            Console.WriteLine(t.Remove(2, 5));
             
            char d = 'd';
            Console.WriteLine(d);
            Console.WriteLine((int)d);
            for (int i = 65; i < 90; i++)
            {
                Console.Write((char)i);
            }

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(RandomNeptun());
            }

            Console.WriteLine(MagánhangzókMegszámolása("árvíztűrő tükörfurógép"));
            Console.WriteLine(BetűkMegszámolása("árvíztűrő tükörfurógép")['r']);
            // Console.WriteLine(LeggyakoribbKarakter("árvíztűrő tükörfurógép"));
            Console.WriteLine(CheckIzogram("árvíztűrő tükörfurógép"));
            Console.WriteLine(CheckIzogram("background"));
        }

        static string RandomNeptun()
        {
            string nk = "";

            for (int i = 0; i < 6; i++)
            {
                if (rng.Next(100) < 30)
                    nk += rng.Next(10);
                else
                    nk += (char)rng.Next('A', 'Z' + 1);
            }

            return nk;
        }

        static int MagánhangzókMegszámolása( string s )
        {
            string t = "aáeéiíoöőuúüű";

            int cnt = 0;
            for (int i = 0; i < t.Length; i++)
            {
                for (int j = 0; j < s.Length; j++)
                {
                    if (s.ToLower()[j] == t[i])
                        ++cnt;
                }
            }

            for (int i = 0; i < s.Length; i++)
                if (t.Contains(char.ToLower(s[i])))
                    ++cnt;

            return cnt;
        }

        static byte[] BetűkMegszámolása( string s )
        {
            byte[] T = new byte[128];

            for (int i = 0; i < T.Length; i++)
            {
                for (int j = 0; j < s.Length; j++)
                {
                    if (s[j] == i)
                        ++T[i];
                }
            }

            /*
            for (int j = 0; j < s.Length; j++)
                ++T[s[j]];
            */

            return T;
        }

        static int LeggyakoribbKarakter(byte[] chars)
        {
            /*
            int[] T = new int[128];

            for (int j = 0; j < chars.Length; j++)
                ++T[chars[j]];

            int max = 0;
            for (int i = 1; i < T.Length; i++)
            {
                if (T[i] > T[max])
                    max = i;
            }

            return max;
            */

            int idx = 0;
            for (int i = 1; i < chars.Length; i++)
            {
                if (chars[i] > chars[idx])
                    idx = i;
            }

            return idx;
        }

        static bool CheckIzogram( string s )
        {
            /*
            int[] T = new int[1280];

            for (int i = 0; i < s.Length; i++)
                ++T[s[i]];

            for (int i = 0; i < T.Length; i++)
            {
                if (T[i] > 1)
                    return false;
            }

            return true;
            */

            for (int i = 0; i < s.Length - 1; i++)
                for (int j = i + 1; j < s.Length; j++)
                    if (s[i] == s[j])
                        return false;

            return true;

            /*
            byte[] b = BetűkMegszámolása(s);
            int i = LeggyakoribbKarakter(b);

            return !(b[i] > 1);
            */
        }
    }
}
