using System;

namespace Ora5
{
    class Program
    {
        // Moodle elõadás --> Algoritmusok, adatszerkezetek I. jegyzet

        // Összetett programozási tételek implementálása.
        // Programozási tételek összeépítéseinek alkalmazása gyakorlati feladatokon.
        // Rendezõ algoritmusok implementálása. (Modul 5.)

        static Random rnd = new Random();

        static void Main(string[] args)
        {
            int[] t = TömbLétrehozás(100);

            int[] c = Másolás(t);

            int[] y1 = new int[0], y2 = new int[0];
            Szétválogatás(t, ref y1, ref y2, 10);

            y1 = new int[] { 10, 1, 2, 3, 4 };
            y2 = new int[] { 10, 5, 6, 7, 8, 9 };
            int[] m = Metszet(y1, y2);

            int[] u = Unio(y1, y2);

            //----------------------
            MinimumkiválasztásosRendezés(t);

            c = new int[] { 9, 8, 7, 6, 5, 4, 3, 2, 1 };
            JavítottBeillesztésesRendezés(c);

            Console.ReadLine();
        }

        static int[] TömbLétrehozás(int méret)
        {
            int[] t = new int[méret];
            for (int i = 0; i < t.Length; i++)
                t[i] = rnd.Next(1001);
            return t;
        }

        #region Összetett programozási tételek
        static int[] Másolás(int[] x)
        {
            int[] y = new int[x.Length];
            for (int i = 0; i < x.Length; i++)
            {
                y[i] = x[i];
            }
            return y;
        }
        static void Szétválogatás(int[] x, ref int[] y1, ref int[] y2, int osztó)
        {
            y1 = new int[x.Length];
            y2 = new int[x.Length];
            int db1 = 0, db2 = 0; // a 'db' index is, ezért 0, ellenben a pszeudo kóddal
            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] % osztó == 0)
                {
                    y1[db1++] = x[i]; // elõbb felhasználjuk és utána növeljük az értéket, mert itt a 0 valid index
                }
                else
                    y2[db2++] = x[i];
            }
        }
        static int[] Metszet(int[] x1, int[] x2)
        {
            int[] y = new int[x1.Length];
            int db = 0;
            for (int i = 0; i < x1.Length; i++)
            {
                int j = 0; // 1 == 0
                while (j < x2.Length && x1[i] != x2[j]) // <= == <
                    j++;
                if (j < x2.Length) // <= == <
                    y[db++] = x1[i]; // db++
            }
            return y;
        }
        static int[] Unio(int[] x1, int[] x2) // önálló feladat
        {
            int[] y = new int[x1.Length + x2.Length];

            for (int i = 0; i < x2.Length; i++)
                y[i] = x2[i];

            int db = x1.Length;
            for (int j = 0; j < x2.Length; j++)
            {
                int i = 0;
                while (i < x1.Length && x1[i] != x2[j])
                    i++;

                if (i > x1.Length)
                    y[db++] = x2[j];
            }
            return y;
        }
        #endregion

        #region Rendezések
        static void MinimumkiválasztásosRendezés(int[] x)
        {
            for (int i = 0; i < x.Length - 1; i++) // (n-1)-ig
            {
                int min = i;
                for (int j = i + 1; j < x.Length; j++)
                    if (x[min] > x[j])
                        min = j;

                int t = x[i]; // csere
                x[i] = x[min];
                x[min] = t;
            }
        }
        static void JavítottBeillesztésesRendezés(int[] x)  // javítsa ki a kódot!
        {
            for (int i = 1; i < x.Length; i++) // 2-tõl 
            {
                int j = i - 1;
                int segéd = x[i];
                while (j > 0 && x[j] > segéd) // j > 0 --> j >= 0, mert a 0 megint csak valid index
                {
                    x[j + 1] = x[j];
                    j--;
                }
                x[j + 1] = segéd;
            }
        }
        #endregion

        #region További feladatok
        // 1. Készítsen metódust, ami két váltózó értékét kicseréli: a = 7, b = 3 --> a = 3, b = 7
        // 2. Készítse el az összes programozási tételt
        // 3. Rendezéseknél készítsen olyan változatot is ami csökkenõ sorrendbe rendezi a gyûjteményt
        // 4. Készítsen metódust index alapján képes törölni a tömbbõl! A metódus hívását követõen a tömb mérete legyen egyel kisebb. Paraméterek: tömb, szám
        // 5. Készítsen metódust index alapján képes beszúrni a tömbbe! A metódus hívását követõen a tömb mérete legyen egyel nagyobb. Paraméterek: tömb, szám
        #endregion
    }
}
