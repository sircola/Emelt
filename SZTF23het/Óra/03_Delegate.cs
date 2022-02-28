using System;

namespace _03_Esemenykezeles
{
    delegate double Közvetítő(double szám);
    delegate double Közvetítő2(double szám1, double szám2); // (2)
    delegate void SzövegFeldolgozó(string szöveg); // (3)

    static class Műveletek
    {
        public static double Kétszerezés(double szám) { return szám * szám; }
        public static double Felezés(double szám) { return szám / 2; }

        public static double Szorzás(double szám1, double szám2) { return szám1 * szám2; } // (2)

        public static void KiírNagy(string s) { Console.WriteLine(s.ToUpper()); } // (3)
        public static void KiírKicsi(string s) { Console.WriteLine(s.ToLower()); }
    }

    class Delegate
    {
        public static void Test1()
        {
            Console.WriteLine(new string('-', 80));

            Közvetítő közvetítő = new Közvetítő(Műveletek.Kétszerezés);
            Console.WriteLine(közvetítő(5));

            közvetítő = new Közvetítő(Műveletek.Felezés);
            Console.WriteLine(közvetítő(5));

            //Közvetítő2 közvetítő2 = new Közvetítő2(Műveletek.Felezés); // nem egyezik a paraméterezés
            Közvetítő2 közvetítő2 = new Közvetítő2(Műveletek.Szorzás);
            Console.WriteLine(közvetítő2(8, 9));
        }
        public static void Test2()
        {
            Console.WriteLine(new string('-', 80));

            Közvetítő közvetítő = new Közvetítő(Műveletek.Kétszerezés);
            közvetítő += new Közvetítő(Műveletek.Felezés); // ennek az eredménye lesz a hívás kimenete...
            // így sok értelme nincsen a visszatérési értéknek

            Console.WriteLine(közvetítő(5));
        }
        public static void Test3()
        {
            Console.WriteLine(new string('-', 80));

            SzövegFeldolgozó teszt = new SzövegFeldolgozó(Műveletek.KiírNagy);
            teszt += new SzövegFeldolgozó(Műveletek.KiírKicsi);
            teszt += new SzövegFeldolgozó(Műveletek.KiírNagy);
            teszt("Teszt Üzenet");
            Console.WriteLine(new string('*', 40));
            teszt -= new SzövegFeldolgozó(Műveletek.KiírNagy); // utolsó egyezőt szedi le
            teszt("Teszt Üzenet");
            Console.WriteLine(new string('*', 40));
            teszt -= new SzövegFeldolgozó(Műveletek.KiírNagy);
            teszt("Teszt Üzenet");
            Console.WriteLine(new string('*', 40));
            teszt -= new SzövegFeldolgozó(Műveletek.KiírNagy); // nincs kivétel ha nincsen olyan
            teszt("Teszt Üzenet");
            teszt = null;
            // visszahívási funkció -> 'Undo'
        }
    }
}
