using System;

namespace _05_Rekurzio
{
    static class Rekurzio_02_ProgTetelek
    {
        public static int Osszegzes(int[] A, int N)
        { //Mennyi egy N elemű A tömb elemeinek az összege?

            if (N == 0) { return 0; }

            else { return A[N - 1] + Osszegzes(A, N - 1); }
        }

        public static bool Eldontes(int[] A, int N)
        { // Egy N elemű A tömbben van-e 15-el osztható szám?

            if (N == 0) { return false; }

            else { return (A[N - 1] % 15 == 0) || Eldontes(A, N - 1); }
        }

        public static int MaximumKivalasztas(int[] A, int N)
        { //Egy N elemű A tömbben hol van a(z egyik) maximális érték?

            if (N == 0) throw new Exception("Nincs tömb..."); // opcionális
            if (N == 1) { return 0; }

            else { return A[N - 1] > A[MaximumKivalasztas(A, N - 1)] ? 
                    N - 1 :
                    MaximumKivalasztas(A, N - 1); }
        }

        public static int LogaritmikusKereses(int[] A, int keresett, int alsoindex, int felsoindex)
        {
            if (alsoindex > felsoindex) return -1;

            int kozep = (alsoindex + felsoindex) / 2;
            if (A[kozep] > keresett)
                return LogaritmikusKereses(A, keresett, alsoindex, kozep - 1); // balra
            else if (A[kozep] < keresett)
                return LogaritmikusKereses(A, keresett, kozep + 1, felsoindex); // jobbra
            else
                return kozep;
        }

        public static void Teszt()
        {
            Console.WriteLine("-----");

            int[] A = new int[] { 1, 4, 5, 7, 9, 10, 11, 14, 16, 20, 25, 30, 46, 54 };

            //1. kérdés - 252
            Console.WriteLine("1. Kérdés: Mennyi a számok összege?");
            Console.WriteLine(Osszegzes(A, A.Length)); // Triviális megoldás: N=0 esetén az összeg 0.

            //2. kérdés - True
            Console.WriteLine("2. Kérdés: Van-e 15-el osztható szám?");
            Console.WriteLine(Eldontes(A, A.Length));

            //3. kérdés - 13
            Console.WriteLine("3. Kérdés: Hol található a legnagyobb szám?");
            Console.WriteLine(MaximumKivalasztas(A, A.Length));

            //4. kérdés - 10
            Console.WriteLine("4. Kérdés: Hol található a 25?");
            Console.WriteLine(LogaritmikusKereses(A, 25, 0, A.Length - 1)); //A rekurzió legmélyebb szintjén találjuk meg a megoldást, és ezt valahogy vissza kell juttatni a hívó szintjére.

            Console.WriteLine("-----");
        }

    }
}
