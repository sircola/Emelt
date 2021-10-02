using System;


namespace Ora4
{
    class Program
    {
        //Hasznos billentyűparancsok:
        //  metódusba lépés:            F12
        //  debugolás során:
        //      belépés a metódusba:    F11
        //      túllépés a metóduson:   F10
        //      kilépés a metódusból:   Shift-F11

        // osztály szintű random változó
        static Random rnd = new Random();

        static void Main(string[] args)
        {
            #region Metódusok 1.
            Üdvözlet();
            Kiír("Szia!");
            MindigIgaz();                           // ha nem érdekel a visszatérés, nem kell fogadni
            int eredmény = Összead(2, 3);           // a megfelelő metódus fog lefutni ami a paraméterek alapján fog eldőlni
            double eredmény2 = Összead(12.3, 5);

            Program.Kiír("Viszlát!"); // osztály előtaggal
            #endregion

            Console.ReadLine();

            #region Metódusok 2.
            int[] a = new int[] { 0, 1, 2 };
            int[] b = a;
            int c = 1;

            Console.Write("\na:"); for (int i = 0; i < a.Length; i++) Console.Write($"{a[i]} ");
            Console.Write("\nb:"); for (int i = 0; i < b.Length; i++) Console.Write($"{b[i]} ");
            Console.Write($"\nc: {c}");
            Console.ReadLine();

            Módosít(b, c);
            Console.Write("\na:"); for (int i = 0; i < a.Length; i++) Console.Write($"{a[i]} ");
            Console.Write("\nb:"); for (int i = 0; i < b.Length; i++) Console.Write($"{b[i]} ");
            Console.Write($"\nc: {c}");
            Console.ReadLine();

            Módosít(b, ref c);
            Console.Write("\na:"); for (int i = 0; i < a.Length; i++) Console.Write($"{a[i]} ");
            Console.Write("\nb:"); for (int i = 0; i < b.Length; i++) Console.Write($"{b[i]} ");
            Console.Write($"\nc: {c}");
            Console.ReadLine();

            Módosít(b);
            Console.Write("\na:"); for (int i = 0; i < a.Length; i++) Console.Write($"{a[i]} ");
            Console.Write("\nb:"); for (int i = 0; i < b.Length; i++) Console.Write($"{b[i]} ");
            Console.Write($"\nc: {c}");
            Console.ReadLine();

            Módosít(ref b);
            Console.Write("\na:"); for (int i = 0; i < a.Length; i++) Console.Write($"{a[i]} ");
            Console.Write("\nb:"); for (int i = 0; i < b.Length; i++) Console.Write($"{b[i]} ");
            Console.Write($"\nc: {c}");
            #endregion

            Console.ReadLine();

            #region Többdimenziós tömbök
            TöbbdimenziósTömbökÁltalános();

            int[,] generált = Tömb2DFeltölt(5, 10);
            Kiír2D(generált);
            Kiír2DSor(generált, 2);
            Kiír2DOszlop(generált, 5);

            Console.ReadLine();

            int[,] mátrix = Tömb2DFeltölt(4, 3);
            Kiír2D(mátrix);
            Kiír2DTranszponált(mátrix);
            #endregion

            Console.ReadLine();

            #region Feladatok

            #region 1.
            //1. - Készítsen metódust, ami egy kétdimenziós tömbből kiválogatja az összes 5-el osztható elemet és visszaadja egy egydimenziós tömbben.
            //2. - Az előző feladathoz készítse el azt a metódust, ami "átméretez" egy egydimenziós tömböt.
            int[,] T = Tömb2DFeltölt(100, 100);
            int[] kiválogatott = MátrixKiválogatás(T, 5);
            #endregion

            Console.ReadLine();

            #region 2.
            //Hozzon létre egy kétdimenziós tömböt, amelyben 5 futó 7 napi futásteljesítményét tárolja.
            //A mátrix[i, j] eleme megadja, hogy i.futó j.napon hány km-t futott. A teljesítményeket véletlenszerűen töltse fel.
            //Adja meg, hogy összesen hány km-t futottak a futók héten, és hogy átlagosan mennyit futottak.
            T = Tömb2DFeltölt(5, 7);
            double összKM = Összegzés(T);
            Console.WriteLine($"Az összes futott km: {összKM}");
            Console.WriteLine($"Az átlag futott km:  {összKM / T.Length}");
            #endregion

            Console.ReadLine();

            #region 3. - Cserélgetős játék
            // Egy játéktábla mezői kétféle módon vannak megjelölve (pl. * és -).
            // Kezdetben minden mező azonos jelölésű (-), kivéve a játéktábla közepén lévő mező.
            //
            // A játék során a felhasználó megadja a játéktábla egy koordinátáját.
            // A kiválsztott koordinátájú mező, illetve annak négy szomszédja az addigival ellentétse jelölésűre változik.
            //
            // A játék akkor ér véget, ha a felhasználó minden mezőt *-ra tudott változtatni.
            // Készítse el a játékot!
            //
            // A játéktábla aktuális állapotát egy kétdimeniziós logikai tömbben tárolja el!
            // Megvalósítandó metódusok:
            //  1) static bool[,] Init()                            - a játéktábla kezdeti állapotát előállító metódus
            //  2) static string State(bool[,] board)                - a játéktábla aktuális állapotát string formában megadó metódus
            //  3) static bool[,] Shoot(bool[,] board, int x, int y) - kiválasztott pontra "lövést" megvalósító metódus
            //  4) static bool IsOver(bool[,] board)                 - a metódus vizsgálja, hogy minden mező *-gá vált-e
            bool[,] board = Init();

            do
            {
                Console.Clear();
                Console.WriteLine(State(board));

                Console.Write("X: ");
                int x = int.Parse(Console.ReadLine());
                Console.Write("Y: ");
                int y = int.Parse(Console.ReadLine());

                Shoot(board, x, y);
            } while (!IsOver(board));
            Console.WriteLine("**WIN**");
            #endregion

            Console.ReadLine();

            #region 10.
            // Készítsen metódust, ami kírja a szorzótáblát 1-től 10-ig.
            Szorzotabla();
            #endregion

            Console.ReadLine();

            #region 11.
            // Készítsen metódust, ami egy mátrixnak megjeleníti a felső háromszögmátrixát. A metódus bemeneti paramétere a mátrix.
            int[,] matrix = Tömb2DFeltölt(100, 100);
            FelsoHaromszogMatrix(matrix);
            #endregion

            Console.ReadLine();

            #region 12.
            // Készítsen metódust, kivon két nXn-es mátrixot egymásból. A metódus bemeneti paramétere a két mátrix, visszatérési értéke egy újabb mátrix ami az eredményt tartalmazza.
            int[,] m1 = Tömb2DFeltölt(100, 100);
            int[,] m2 = Tömb2DFeltölt(100, 100);
            int[,] sub = MatrixKivonas(m1, m2);
            #endregion

            Console.ReadLine();

            #region 13.
            // Egy horgászverseny adatait egy kétdimenziós tömbben tároljuk: $M(i,j)$, ami azt jelenti, hogy az $i.$ horgász a $j.$ halfajból mennyit fogott.
            //  - Az M mátrix adatait adhassa meg a felhasználó, illetve legyen lehetőség véletlenszerű feltöltésre is.
            //  - Az M mátricot lehessen megjeleníteni a konzolon.
            //  - Határozza meg, hogy a horgászok összesen mennyit fogtak az egyes halfajokból.
            //  - Határozza meg, hogy az egyes horgászok hány halat fogtak összesen.
            //  - Van-e olyan horgász, aki nem fogott egyetlen halat sem ?
            //  - Melyik horgász fogta a legtöbb halat?

            Horgászverseny();
            #endregion

            #endregion

            Console.ReadLine();
        }

        #region Metódusok 1.
        static void Üdvözlet()
        {
            Console.WriteLine("Hello World!");
        }
        static void Kiír(string üzenet)
        {
            Console.WriteLine(üzenet);
        }
        static bool MindigIgaz()
        {
            return true;
        }
        static int Összead(int a, int b)        // metódus túlterhelés
        {
            return a + b;
        }
        //static double Összead(int a, int b)   // a visszatérési típusban való eltérés nem jó!
        //{
        //    return a + b;
        //}
        static int Összead(int a, int b, int c) //vagy a paraméter számában
        {
            return a + b + c;
        }
        static double Összead(double a, int b)  // vagy a paraméterek típusában
        {
            return a + b;
        }
        #endregion

        #region Metódusok 2.
        static void Módosít(int[] t, int idx)
        {
            t[idx] = idx * 50;
            idx = int.MaxValue;
        }
        static void Módosít(int[] t, ref int idx)
        {
            t[idx] = idx * 100;
            idx = int.MaxValue;
        }
        static void Módosít(int[] t)
        {
            t = new int[] { 1, 2, 3, 4, 5, 6 };
        }
        static void Módosít(ref int[] t)
        {
            t = new int[] { 1, 2, 3, 4, 5, 6 };
        }
        #endregion

        #region Többdimenziós tömbök
        static void TöbbdimenziósTömbökÁltalános()
        {
            //Tömb létrehozása
            int[] tömb = { 1, 2, 3, 4, 5 };
            int[,] tömb2D = new int[2, 3]; // sor-oszlop
            int[,,] tömb3D = new int[2, 3, 4];
            int[,] tömb2D_v2 = new int[,] {
                { 1, 2, 3 },
                { 1, 2, 3 }
            };
            int[,,] tömb3D_v2 = new int[,,] {
                { { 1, 2, 3, 4 }, { 1, 2, 3, 4 }, { 1, 2, 3, 4 } },
                { { 1, 2, 3, 4 }, { 1, 2, 3, 4 }, { 1, 2, 3, 4 } }
            };
            //Elem kiolvasása
            Console.WriteLine(tömb[4]); // =5
            Console.WriteLine(tömb2D_v2[1, 2]); // =3
            Console.WriteLine(tömb3D_v2[0, 0, 0]); // =1

            //Elemnek értékadás
            tömb[2] = 99;
            tömb2D_v2[1, 1] = 99;
            tömb3D_v2[1, 2, 0] = 99;

            //A tömb hossza
            int length = tömb.Length;
            int length2D = tömb2D_v2.Length; // összes elem - 2*3=6
            int length2D_dim0 = tömb2D_v2.GetLength(0); //0. dimenzió hossza - sorok száma - 2
            int length2D_dim1 = tömb2D_v2.GetLength(1); //1. dimenzió hossza - oszlopok száma - 3
            int length3D = tömb3D_v2.Length; // összes elem
            int length3D_dim0 = tömb3D_v2.GetLength(0); //0. dimenzió hossza
            int length3D_dim1 = tömb3D_v2.GetLength(1); //1. dimenzió hossza
            int length3D_dim2 = tömb3D_v2.GetLength(2); //2. dimenzió hossza

            // Az első eleme – elem elérése
            int e1 = tömb[0];
            int e2 = tömb2D_v2[0, 0];
            int e3 = tömb3D_v2[0, 0, 0];
            //Az utolsó eleme
            int u1 = tömb[tömb.Length - 1];
            int u2 = tömb2D_v2[tömb2D_v2.GetLength(0) - 1, tömb2D_v2.GetLength(1) - 1];
            int u3 = tömb3D_v2[tömb3D_v2.GetLength(0) - 1, tömb3D_v2.GetLength(1) - 1, tömb3D_v2.GetLength(2) - 1];

            //Tömb minden elemének kiiratása - ugyancsak nem jó
            Console.WriteLine(tömb);
            Console.WriteLine(tömb2D_v2.ToString());
            Console.WriteLine(tömb3D_v2.ToString());
        }

        //Generálás
        static int[,] Tömb2DFeltölt(int sor, int oszlop)
        {
            int[,] tömb2D = new int[sor, oszlop];
            for (int i = 0; i < tömb2D.GetLength(0); i++)
            {
                for (int j = 0; j < tömb2D.GetLength(1); j++)
                {
                    tömb2D[i, j] = rnd.Next(0, 101);
                }
            }
            return tömb2D;
        }
        //A teljes tomb kiiratása
        static void Kiír2D(int[,] tömb)
        {
            for (int i = 0; i < tömb.GetLength(0); i++)
            {
                for (int j = 0; j < tömb.GetLength(1); j++)
                {
                    Console.Write(tömb[i, j] + "\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        //Az adott sor kiiratása
        static void Kiír2DSor(int[,] tömb, int sor)
        {
            for (int j = 0; j < tömb.GetLength(1); j++) //oszlopokon lépkedünk az adott sorban
            {
                Console.Write(tömb[sor, j] + "\t");
            }
            Console.WriteLine();
        }
        //Az adott oszlop kiíratása
        static void Kiír2DOszlop(int[,] tömb, int oszlop)
        {
            for (int i = 0; i < tömb.GetLength(0); i++) //sorokon lépkedünk az adott oszlopban
            {
                Console.WriteLine(tömb[i, oszlop]);
            }
            Console.WriteLine();
        }

        static void Kiír2DTranszponált(int[,] mátrix)
        {
            // Önálló feladat: módosítsa úgy a kódot, hogy a mátrix transzponáltját írja ki!  --> két cikllusfej felcserélése
            for (int i = 0; i < mátrix.GetLength(0); i++)
            {
                for (int j = 0; j < mátrix.GetLength(1); j++)
                {
                    Console.Write(mátrix[i, j] + "\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        #endregion

        #region Feladatok
        #region 1.
        static int[] MátrixKiválogatás(int[,] mátrix, int osztó)
        {
            int[] temp = new int[mátrix.GetLength(0) * mátrix.GetLength(1)];
            int tempindex = 0;
            for (int i = 0; i < mátrix.GetLength(0); i++)
            {
                for (int j = 0; j < mátrix.GetLength(1); j++)
                {
                    if (mátrix[i, j] % osztó == 0)
                    {
                        temp[tempindex++] = mátrix[i, j];
                    }
                }
            }
            TombRovidito(ref temp, tempindex);
            return temp;
        }
        static void TombRovidito(ref int[] tomb, int index)
        {
            int[] temp = new int[index];
            for (int i = 0; i < index; i++)
            {
                temp[i] = tomb[i];
            }
            tomb = temp;
        }
        #endregion

        #region 2.
        static double Összegzés(int[,] matrix)
        {
            int szum = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    szum += matrix[i, j];
                }
            }
            return szum;
        }
        #endregion

        #region 3.
        static bool[,] Init()
        {
            // true: *, false: -
            bool[,] board = new bool[3, 3];
            board[board.GetLength(0) / 2, board.GetLength(1) / 2] = true;
            return board;
        }
        static string State(bool[,] board)
        {
            string s = "   ";
            for (int i = 0; i < board.GetLength(0); i++) s += i + " "; // header

            s += "\n";
            for (int i = 0; i < board.GetLength(0); i++)
            {
                s += $" {i}";
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (!board[i, j])
                        s += " *";
                    else
                        s += " -";
                }
                s += "\n";
            }
            return s;
        }
        static void Shoot(bool[,] board, int x, int y)
        {
            if (x >= 0 && x < board.GetLength(0) && y >= 0 && y < board.GetLength(1))
            {
                board[x, y] = !board[x, y];
                if (x - 1 >= 0)
                    board[x - 1, y] = !board[x - 1, y];
                if (x + 1 < board.GetLength(0))
                    board[x + 1, y] = !board[x + 1, y];
                if (y - 1 >= 0)
                    board[x, y - 1] = !board[x, y - 1];
                if (y + 1 < board.GetLength(1))
                    board[x, y + 1] = !board[x, y + 1];
            }
        }
        static bool IsOver(bool[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
                for (int j = 0; j < board.GetLength(1); j++)
                    if (!board[i, j])
                        return false;
            return true;
        }
        #endregion

        #region 10.
        // Készítsen metódust, ami kírja a szorzótáblát 1-től 10-ig.
        static void Szorzotabla()
        {
            for (int i = 1; i <= 10; i++)
            {
                for (int j = 1; j <= 10; j++)
                {
                    Console.Write(i * j);
                    Console.Write(new string(' ', 10 - (i * j).ToString().Length));
                }
                Console.WriteLine();
            }
        }
        #endregion

        #region 11.
        // Készítsen metódust, ami egy mátrixnak megjeleníti a felső háromszögmátrixát. A metódus bemeneti paramétere a mátrix.
        static void FelsoHaromszogMatrix(int[,] matrix)
        {
            for (int i = 1; i < matrix.GetLength(0); i++)
            {
                for (int j = 1; j < matrix.GetLength(1); j++)
                {
                    if (i <= j)
                    {
                        Console.Write(matrix[i, j]);
                        Console.Write(new string(' ', 5 - matrix[i, j].ToString().Length));
                    }
                    else
                        Console.Write(new string(' ', 5));
                }
                Console.WriteLine();
            }
        }
        #endregion

        #region 12.
        // Készítsen metódust, kivon két nXn-es mátrixot egymásból. A metódus bemeneti paramétere a két mátrix, visszatérési értéke egy újabb mátrix ami az eredményt tartalmazza.
        static int[,] MatrixKivonas(int[,] matrix1, int[,] matrix2)
        {
            int[,] ret = new int[matrix1.GetLength(0), matrix1.GetLength(1)];
            for (int i = 1; i < ret.GetLength(0); i++)
                for (int j = 1; j < ret.GetLength(1); j++)
                    ret[i, j] = matrix1[i, j] - matrix2[i, j];
            return ret;
        }
        #endregion

        #region 13.
        // Egy horgászverseny adatait egy kétdimenziós tömbben tároljuk: $M(i,j)$, ami azt jelenti, hogy az $i.$ horgász a $j.$ halfajból mennyit fogott.
        //  - Az M mátrix adatait adhassa meg a felhasználó, illetve legyen lehetőség véletlenszerű feltöltésre is.
        //  - Az M mátricot lehessen megjeleníteni a konzolon.
        //  - Határozza meg, hogy a horgászok összesen mennyit fogtak az egyes halfajokból.
        //  - Határozza meg, hogy az egyes horgászok hány halat fogtak összesen.
        //  - Van-e olyan horgász, aki nem fogott egyetlen halat sem ?
        //  - Melyik horgász fogta a legtöbb halat?
        static void Horgászverseny()
        {
            Console.Write("[F]elhasználói vagy [V]életlenszerű adatfeltöltést szeretne? ");
            string választás = Console.ReadLine();
            Console.Write("Versenyzők száma: ");
            int versenyzőkSzáma = int.Parse(Console.ReadLine());
            Console.WriteLine("Halfajok száma: ");
            int halfajokszáma = int.Parse(Console.ReadLine());

            int[,] verseny = new int[versenyzőkSzáma, halfajokszáma];

            if (választás == "F")
                VersenyLétrehozásUser(verseny);
            else
                VersenyLétrehozásRandom(verseny);

            VersenyKiír(verseny);
            ÖsszesHalFajonként(verseny);
            ÖsszesHalHorgászonként(verseny);
            EgyetlenHal(verseny);
            LegtöbbHal(verseny);
        }
        static void VersenyLétrehozásRandom(int[,] verseny)
        {
            Random rnd = new Random();
            for (int i = 0; i < verseny.GetLength(0); i++)
                for (int j = 0; j < verseny.GetLength(1); j++)
                    verseny[i, j] = rnd.Next(100);
        }
        static void VersenyLétrehozásUser(int[,] verseny)
        {
            for (int i = 0; i < verseny.GetLength(0); i++)
                for (int j = 0; j < verseny.GetLength(1); j++)
                {
                    Console.Write($"Adja meg, hogy a(z) {i}. horgász a(z) {j}. halfajból mennyit fogott: ");
                    verseny[i, j] = int.Parse(Console.ReadLine());
                }
        }
        static void VersenyKiír(int[,] verseny)
        {
            for (int i = 0; i < verseny.GetLength(0); i++)
            {
                Console.Write($"A(z) {i}. horgász fogásának eredménye: ");
                for (int j = 0; j < verseny.GetLength(1); j++)
                {
                    Console.Write($"{verseny[i,j]} ");
                }
                Console.WriteLine();
            }
        }
        static void ÖsszesHalFajonként(int[,] verseny)
        {
            for (int j = 0; j < verseny.GetLength(1); j++)
            {
                int sum = 0;
                for (int i = 0; i < verseny.GetLength(0); i++)
                    sum += verseny[i, j];
                Console.WriteLine($"A(z) {j}. halfajból fogott halak száma: {sum}");
            }
        }
        static void ÖsszesHalHorgászonként(int[,] verseny)
        {
            for (int i = 0; i < verseny.GetLength(0); i++)
            {
                int sum = 0;
                for (int j = 0; j < verseny.GetLength(1); j++)
                    sum += verseny[i, j];
                Console.WriteLine($"A(z) {i}. horgász összesen {sum} alat fogott!");
            }
        }
        static void EgyetlenHal(int[,] verseny)
        {
            for (int i = 0; i < verseny.GetLength(0); i++)
            {
                int sum = 0;
                for (int j = 0; j < verseny.GetLength(1); j++)
                    sum += verseny[i, j];
                if (sum == 0)
                    Console.Write($"A(z) {i}. horgász egyetlen halat sem fogott!");
            }
        }
        static void LegtöbbHal(int[,] verseny)
        {
            int max = 0, idx = 0;
            for (int i = 0; i < verseny.GetLength(0); i++)
            {
				int act = 0;
                for (int j = 0; j < verseny.GetLength(1); j++)
                    act += verseny[i, j];
                if (act > max)
                {
                    idx = i;
                    max = act;
                }
            }
            Console.Write($"A legtöbb halat a(z) {idx}. horgász fogta: {max}!");
        }
        #endregion

        #endregion
    }
}
