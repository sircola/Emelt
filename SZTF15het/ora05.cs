using System;

namespace Ora5
{
    class Program
    {
        // Moodle el�ad�s --> Algoritmusok, adatszerkezetek I. jegyzet

        // �sszetett programoz�si t�telek implement�l�sa.
        // Programoz�si t�telek �ssze�p�t�seinek alkalmaz�sa gyakorlati feladatokon.
        // Rendez� algoritmusok implement�l�sa. (Modul 5.)

        static Random rnd = new Random();

        static void Main(string[] args)
        {
            int[] t = T�mbL�trehoz�s(100);

            int[] c = M�sol�s(t);

            int[] y1 = new int[0], y2 = new int[0];
            Sz�tv�logat�s(t, ref y1, ref y2, 10);

            y1 = new int[] { 10, 1, 2, 3, 4 };
            y2 = new int[] { 10, 5, 6, 7, 8, 9 };
            int[] m = Metszet(y1, y2);

            int[] u = Unio(y1, y2);

            //----------------------
            Minimumkiv�laszt�sosRendez�s(t);

            c = new int[] { 9, 8, 7, 6, 5, 4, 3, 2, 1 };
            Jav�tottBeilleszt�sesRendez�s(c);

            Console.ReadLine();
        }

        static int[] T�mbL�trehoz�s(int m�ret)
        {
            int[] t = new int[m�ret];
            for (int i = 0; i < t.Length; i++)
                t[i] = rnd.Next(1001);
            return t;
        }

        #region �sszetett programoz�si t�telek
        static int[] M�sol�s(int[] x)
        {
            int[] y = new int[x.Length];
            for (int i = 0; i < x.Length; i++)
            {
                y[i] = x[i];
            }
            return y;
        }
        static void Sz�tv�logat�s(int[] x, ref int[] y1, ref int[] y2, int oszt�)
        {
            y1 = new int[x.Length];
            y2 = new int[x.Length];
            int db1 = 0, db2 = 0; // a 'db' index is, ez�rt 0, ellenben a pszeudo k�ddal
            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] % oszt� == 0)
                {
                    y1[db1++] = x[i]; // el�bb felhaszn�ljuk �s ut�na n�velj�k az �rt�ket, mert itt a 0 valid index
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
        static int[] Unio(int[] x1, int[] x2) // �n�ll� feladat
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

        #region Rendez�sek
        static void Minimumkiv�laszt�sosRendez�s(int[] x)
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
        static void Jav�tottBeilleszt�sesRendez�s(int[] x)  // jav�tsa ki a k�dot!
        {
            for (int i = 1; i < x.Length; i++) // 2-t�l 
            {
                int j = i - 1;
                int seg�d = x[i];
                while (j > 0 && x[j] > seg�d) // j > 0 --> j >= 0, mert a 0 megint csak valid index
                {
                    x[j + 1] = x[j];
                    j--;
                }
                x[j + 1] = seg�d;
            }
        }
        #endregion

        #region Tov�bbi feladatok
        // 1. K�sz�tsen met�dust, ami k�t v�lt�z� �rt�k�t kicser�li: a = 7, b = 3 --> a = 3, b = 7
        // 2. K�sz�tse el az �sszes programoz�si t�telt
        // 3. Rendez�sekn�l k�sz�tsen olyan v�ltozatot is ami cs�kken� sorrendbe rendezi a gy�jtem�nyt
        // 4. K�sz�tsen met�dust index alapj�n k�pes t�r�lni a t�mbb�l! A met�dus h�v�s�t k�vet�en a t�mb m�rete legyen egyel kisebb. Param�terek: t�mb, sz�m
        // 5. K�sz�tsen met�dust index alapj�n k�pes besz�rni a t�mbbe! A met�dus h�v�s�t k�vet�en a t�mb m�rete legyen egyel nagyobb. Param�terek: t�mb, sz�m
        #endregion
    }
}
