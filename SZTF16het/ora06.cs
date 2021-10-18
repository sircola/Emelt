using System;

namespace Ora6
{
    class Program
    {
        static Random rnd = new Random();

        static void Main(string[] args)
        {
            for (int i = 0; i < 5; i++)
                Console.WriteLine($"Random neptun: {RandomNeptun()}");
            Console.ReadLine();

            // https://www.lipsum.com/
            string test = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit.Morbi quis hendrerit dui, in mollis mi.Nullam pharetra metus sapien, in suscipit sem mollis eget.Nullam turpis purus, aliquet faucibus porta id, accumsan vitae massa.Donec sit amet enim laoreet, sollicitudin dolor eget, tristique metus.Donec et iaculis eros.Vivamus maximus arcu sit amet congue tincidunt.Ut gravida ultricies nisl, eu euismod libero luctus eu.Proin turpis neque, varius at nunc eu, dignissim vulputate neque.Vivamus auctor ipsum in vestibulum malesuada.Morbi urna mi, faucibus in laoreet vitae, facilisis eget neque.
Vivamus vitae justo rutrum, dignissim ante sit amet, mollis leo.In consequat, quam nec ultricies placerat, eros tortor blandit eros, eget scelerisque nunc magna eu tortor.Phasellus maximus ultricies ipsum at egestas.Nullam eget odio urna.Vivamus vel rhoncus nulla.Integer ac urna a massa vulputate vestibulum.Duis hendrerit accumsan aliquam.In porta erat quis nisl ornare hendrerit.In leo nunc, tristique lobortis ultrices at, lobortis nec est.Morbi tellus arcu, vestibulum porta aliquet a, venenatis ut lorem.
Suspendisse vel nulla faucibus neque convallis iaculis.Praesent consectetur semper leo, eget venenatis orci cursus sit amet.Aliquam dapibus, tortor at dignissim condimentum, arcu dolor porta metus, sed varius arcu nunc a urna.Vivamus dolor lectus, rutrum a tincidunt et, dictum ac metus.Vivamus vitae augue eu metus efficitur posuere sed at mi.Duis a orci quis erat vehicula vehicula.Nunc a magna at lacus vestibulum scelerisque non vel nunc.Donec ultrices mi nec quam auctor fermentum.
Aliquam accumsan faucibus leo.Pellentesque vehicula et lectus a porta.Suspendisse et quam nec diam egestas luctus.Vivamus vulputate, sapien ac fringilla commodo, libero sem ullamcorper diam, volutpat finibus tellus risus vitae diam.Cras eu ante diam.Aliquam erat volutpat.Pellentesque commodo metus lectus, a dictum est posuere sit amet.
Donec mollis blandit nibh ac molestie.Suspendisse potenti.Quisque interdum egestas neque.Aenean varius metus et leo auctor, ut lobortis diam malesuada.Curabitur eu sem orci.Praesent in eros posuere, consectetur sem sed, auctor ipsum.Sed quis risus quis augue egestas dapibus.Suspendisse vel finibus ex, at rutrum nibh.Quisque ut laoreet tortor.Proin euismod luctus tristique.Aenean tortor felis, ultricies eu velit ac, pellentesque tempor urna.Suspendisse rhoncus eget nisi non feugiat.Suspendisse convallis risus ac rutrum maximus.Ut et pellentesque urna.Nam imperdiet sem tristique dui semper viverra.";

            Console.WriteLine($"Magánhangzók száma: {MagánhangzókMegszámolása(test)}");
            Console.ReadLine();

            byte[] betűk = BetűkMegszámolása(test);
            int kód = LeggyakoribbKarakter(betűk);
            Console.WriteLine($"Leggyakoribb karakter: {(char)kód} - {betűk[kód]}");
            Console.ReadLine();

            test = RemoveSpaces(test).Replace("e", "");

            Console.WriteLine(test);
            Console.ReadLine();

            string palindrom = "Madam in Eden, I'm Adam!";
            Console.WriteLine($"Palindrom-e?: \"{palindrom}\" --> {Palindrom(palindrom)}");
            Console.ReadLine();

            Console.WriteLine($"Mumbling: {Mumbling("abcd")}");
            Console.WriteLine($"Mumbling: {Mumbling("Trump")}");
            Console.ReadLine();

            Console.WriteLine(Szétválogatás(test));
            Console.ReadLine();
        }


        #region Karaktersorozatok - char, string
        static void StringÁltalánosságban()
        {
            string _null;
            string üres = "";
            string cccccc = new string('c', 6);
            string s1 = "Hello", s2 = "World";

            Console.WriteLine($"Az 's1' hossza, karaktereinek száma: {s1.Length}");
            Console.WriteLine($"Az 's1' 3. karaktere: {s1[2]}");
            Console.WriteLine($"Az 's1'-hez fűzött karakter: {s1 + s2[4]}");
            //s1[0] = 's';

            Console.WriteLine("Soronként a karakterek:");
            for (int i = 0; i < s1.Length; i++)
                Console.WriteLine(s1[i]);

            Console.WriteLine($"Legyen minden nagybetűs: {s1.ToUpper()}");

            // string szétvágása a megadott karakterknél:
            string[] split = "Első,Második;Harmadik.".Split(new char[] { ',', ';', '.' });
            string[] split2 = "Első,Második;;;;;;Harmadik.".Split(new char[] { ',', ';', '.' });
            string[] split3 = "Első,Második;;;;;;Harmadik.".Split(new string[] { "Második" }, StringSplitOptions.RemoveEmptyEntries);

            // string-ből kivágás
            string world = "Hello World!".Substring(6, 5);

            // stringben lévő karakter indexe
            int index = "Hello World!".IndexOf('ő'); // ha nincs benne akkor -1

            //..stb..

            // string karakter tömbb-é alakítása
            char[] c = s1.ToCharArray();
            c[2] = 'x';
            s1 = new string(c); // karaktertömb string-é alakítása

            //ne használjuk a Linq lekérdezéseket: töröljük a 'using System.Linq' névtér importot
            s1.Contains("a");
        }
        static void CharÁltalánosságban()
        {
            char c = 'O';
            Console.WriteLine("A c: " + c);

            Console.WriteLine("A karakter betű? " + char.IsLetter(c));
            Console.WriteLine("A karakter szám? " + char.IsDigit(c));
            Console.WriteLine("A karakter betű vagy szám? " + char.IsLetterOrDigit(c));
            Console.WriteLine("A karakter kisbetűs? " + char.IsLower(c));
            Console.WriteLine("A karakter nagybetűs? " + char.IsUpper(c));
            Console.WriteLine("A karakter kisbetűvé alakítva: " + char.ToLower(c));
            Console.WriteLine("A karakter nagybetűssé alakítva: " + char.ToUpper(c)); // "".ToUpper()

            // ASCII kódtábla alapján lehet számmá, vagy a számot karakterré alakítani
            // a-z-ig a karakterek számból
            // http://jbwyatt.com/I/asciiGood.gif
            for (int i = 97; i < 123; i++)
                Console.Write((char)i);
            Console.WriteLine();
            for (int i = 65; i < 91; i++)
                Console.Write((char)i);

            Console.WriteLine();
        }

        static string RandomNeptun()
        {
            // A neptunkód 6 karakterből áll. Minden karakter az angol ABC nagybetűje, illetve 0 és 9 közötti számjegy lehet.
            // A generálás során az adott karakter 30% eséllyel lesz szám, 70% eséllyel pedig betű.
            string s = "";
            for (int i = 0; i < 6; i++)
                if (rnd.Next(100) < 30) s += rnd.Next(10);
                else s += (char)rnd.Next((int)'A', 'Z' + 1);
            return s;
        }
        static int MagánhangzókMegszámolása(string s)
        {
            // A metódus megszámolja, hogy hány darab magánhangzó (aáeéiíoóöőuúüű) található a string-ben.
            int db = 0;
            for (int i = 0; i < s.Length; i++)
                //if (s[i] == 'a' || s[i] == 'á' ...)
                if ("aáeéiíoóöőuúüű".Contains(s[i].ToString()))
                    db++;

            return db;
        }
        static byte[] BetűkMegszámolása(string s)
        {
            // A metódus megszámolja a karaktereket, vagyis hogy melyik karakterből hány darab van a string-ben.
            byte[] chars = new byte[128];
            for (int i = 0; i < s.Length; i++)
                chars[(byte)s[i]]++; // a tömb indexe a karakter kódja, az érték a darabszámot jelöli
            return chars;
        }
        static int LeggyakoribbKarakter(byte[] chars)
        {
            // A metódus visszaadja azt a karaktert, amelyikből a legtöbb található a tömbben.
            int maxID = 0;
            for (int i = 1; i < chars.Length; i++)
                if (chars[i] > chars[maxID])
                    maxID = i;

            return maxID;
        }
        static string RemoveSpaces(string s)
        {
            // A metódus a paraméter kapott string-ből eltávolítja a whitespace karaktereket.

            //s = s.Remove(0, 1000); // index alapján töröl
            //s.Replace(' ', '') // nincs 'üres' karakter
            return s.Replace(" ", "").Replace("\r", "").Replace("\n", "");
        }

        static bool Palindrom(string s)
        {
            // A metódus megvizsgálja, hogy a string palindrom-e (úgy, hogy a szóközöket és az írásjeleket figyelmen kívül hagyja)

            // csak a betűk megtartása
            string s2 = "";
            for (int i = 0; i < s.Length; i++)
                if (char.IsLetter(s[i]))
                    s2 += char.ToLower(s[i]);

            // palindrom vizsgálat
            int j = 0;
            while (j < s2.Length / 2 && s2[j] == s2[s2.Length - 1 - j])
                j++;

            return j >= s2.Length / 2;
        }

        static string Mumbling(string s)
        {
            // Önálló feladat
            // A metódus a bemeneti string-et az alábbi logai szerint alakítja át:
            // "abcd" --> "A-Bb-Ccc-Dddd"
            // "trump" --> "T-Rr-Uuu-Mmmm-Ppppp"
            string ret = "";
            for (int i = 0; i < s.Length; i++)
            {
                ret += char.ToUpper(s[i]);
                ret += new string(char.ToLower(s[i]), i);

                if (i != s.Length - 1)
                    ret += '-';
            }

            return ret;
        }
        #endregion

        #region Szétválogatás (helyben)
        static string Szétválogatás(string s)
        {
            // P: a string elejére kerüljenek a betűk a végére a nem betűk
            char[] x = s.ToCharArray();
            int bal = 0, /* bal <- 1 */ jobb = x.Length - 1;
            char segéd = x[0];
            while (bal < jobb)
            {
                while (bal < jobb && !char.IsLetter(x[jobb]))
                    jobb--;

                if (bal < jobb)
                {
                    x[bal] = x[jobb];
                    bal++;
                    while (bal < jobb && char.IsLetter(x[bal]))
                        bal++;
                    if (bal < jobb)
                    {
                        x[jobb] = x[bal];
                        jobb++;
                    }
                }
            }
            x[bal] = segéd;
            return new string(x);
        }
        #endregion


        #region További feladatok
        // Készítsen metódust, ami a bemeneti karaktersorozatot átalakítja a következő módon:
        // "Te tudsz így beszélni?" --> "Teve tuvudsz ívígy beveszévélnivi?"
        static string TeveConverter(string s)
        {
            return "";
        }

        // Készítsen metódust, ami a bemeneti karaktersorozatról eldönti, hogy Izogram-e vagy sem.
        // Minden olyan szó izogrammnak tekinthető, melyben a betűk nem ismétlődnek. A metódus
        // visszetérési értéket legyen igaz, ha a karaktersorozat izogram, hamis, ha nem.
        // Példa:
        //  hydropneumatics --> true
        //  background --> true
        //  isograms --> false
        static bool CheckIzogram(string s)
        {
            for (int i = 0; i < s.Length - 1; i++)
                for (int j = i + 1; j < s.Length; j++)
                    if (s[i] == s[j])
                        return false;
            return true;

            // vagy
            char[] chars = s.ToLower().ToCharArray();

            //minimumkiválasztásos rendezés
            for (int i = 0; i < chars.Length - 1; i++)
            {
                int min = i;
                for (int j = i + 1; j < chars.Length; j++)
                {
                    if (chars[min] > chars[j])
                        min = j;
                }
                char t = chars[i];
                chars[i] = chars[min];
                chars[min] = t;
            }

            for (int i = 0; i < chars.Length - 1; i++)
                if (chars[i] == chars[i + 1])
                    return false;
            return true;
        }

        // Készítsen metódust, melynek bemenete egy karakter tömb. A metódusnak a feladata, hogy egy olyan karakter tömböt adjon vissza,
        // ami csak a nem egyedi értékeket tartalmazza, vagyis minden olyan elemet figyelmen kívül kell hagyni a bemeneti tömbben, melyek
        // csak egyszer szerepelnek benne. A metódus a feldolgozás során ne módosítsa az eredeti (bemeneti) tömböt!
        // Példa:
        //  { 'a', 'a', 'a' } --> { 'a', 'a', 'a' }
        //  { 'a', 'b', 'c', 'a', 'd', 'e', 'f' } --> { 'a', 'a' }
        //  { 'a', 'b', 'c' } --> {}
        //  { 'a', 'b', 'a', 'a', 'b', 'c' } --> { 'a', 'b', 'a', 'a', 'b' }
        static char[] DropUnique(char[] c)
        {
            string t = "";

            for (int i = 0; i < c.Length; i++)
            {
                int count = 0;
                for (int j = 0; j < c.Length; j++)
                    if (c[i] == c[j])
                        count++;
                if (count > 1)
                    t += c[i];
            }
            return t.ToCharArray();
        }

        // Készítsen metódust, ami a bemenetként kapott decimális számot átalakítja bináris számmá.
        // Például:
        //  0 --> 0
        //  1 --> 1
        //  10 --> 1010
        //  100 --> 1100100
        //  9000 -> 10001100101000
        static string DecToBin(int dec)
        {
            int maradék;
            string result = "";
            while (dec > 0)
            {
                maradék = dec % 2;
                dec /= 2;
                result = maradék.ToString() + result;
            }
            Console.WriteLine("Binary:  {0}", result);

            return Convert.ToString(dec, 2);
        }
        #endregion
    }
}
