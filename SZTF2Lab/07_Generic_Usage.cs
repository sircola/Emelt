using System;
using System.Collections.Generic;

namespace _06_Generikus_LancoltLista
{
    class Test { }//(1) - Ismeretlen típusú változó inicializálása
    class Test_IComp : IComparable { public int CompareTo(object obj) { return 0; } }//(3) - Interface/Leszármazott megszorítás
    class Test_Const { public Test_Const(int a) { } }//(4) - Konstruktor megszorítás

    class E02_Generic_Usage
    {
        public static void Teszt()
        {
            //(1) - Ismeretlen típusú változó inicializálása
            Minta1<int> m1 = new Minta1<int>();
            Minta1<Test> m2 = new Minta1<Test>();


            //(2) - T típus megszorítása
            Minta2_E<int> m3 = new Minta2_E<int>(); //OK
            //Minta2_E<Test> m4 = new Minta2_E<Test>(); //NOK - mert referencia

            //Minta2_R<int> m5 = new Minta2_R<int>(); //NOK - mert érték
            Minta2_R<Test> m6 = new Minta2_R<Test>(); //OK


            //(3) - Interface/Leszármazott megszorítás
            RendezettParos<int> m7 = new RendezettParos<int>(1, 2); //OK
            //RendezettParos<Test> m8 = new RendezettParos<Test>(new Test(), new Test()); //NOK
            RendezettParos<Test_IComp> m9 = new RendezettParos<Test_IComp>(new Test_IComp(), new Test_IComp()); //OK


            //(4) - Konstruktor megszorítás
            ObjektumGyar<int> m10 = new ObjektumGyar<int>();
            //ObjektumGyar<Test_Const> m11 = new ObjektumGyar<Test_Const>(); //NOK


            //(5) - Statikus osztály
            //StatikusOsztály.Print(); //NOK
            StatikusOsztály<int>.Print(); //OK

            StatikusOsztály<int>.Print<int>();//OK


            //(6) - Több különböző paraméter
            SokParaméter<Test, int, Test_IComp, object> m12 = new SokParaméter<Test, int, Test_IComp, object>();


            //(7) - Generikus öröklés
            SzamLista m13 = new SzamLista();
            int c = m13.Count;

            GenerikusLeszarmazott<int> m14 = new GenerikusLeszarmazott<int>();


            //(8) - Generikus metódus
            GenMetodminta m15 = new GenMetodminta();
            m15.Min<int>(10, 11);
            m15.Min(10, 11); //Ha a hívásnak megfelel generikus és nem generikus változat is, akkor az előbbi fut le

            GenOsztályÉsMetodus<int> m16 = new GenOsztályÉsMetodus<int>();
            m16.Test(new GenOsztályÉsMetodus<int>());


            //(9) - Osztályban osztály
            Kulso k = new Kulso();
            k.Muvelet();
        }
    }


    #region (0) Generikus típus neve lehet bármi, de T, K-nak szokták elnevezni
    class Minta0<asd, dffe, eofk> { asd a; dffe d; eofk e; }
    #endregion


    #region (1) Ismeretlen típusú változó inicializálása
    class Minta1<T>
    {
        T a;
        public Minta1() { a = default(T); } // A típustúl függő alapértéket állítja be
        public Minta1(T a) { this.a = a; } // A példányosításkor a konstruktoron keresztül kerül beállításra
    }
    #endregion


    #region (2) T típus megszorítása
    class Minta2_E<T> where T : struct { } // Legyen 'érték' típus a T
    class Minta2_R<T> where T : class { } // Legyen 'referencia' típus a T
    #endregion


    #region (3) Interface/Leszármazott megszorítás
    class RendezettParos<T> where T : IComparable
    {
        T kicsi, nagy; // A T típusnak meg kell valósítania az IComparable interface-t

        public RendezettParos(T a, T b)
        {
            kicsi = a.CompareTo(b) < 0 ? a : b;
            nagy = a.CompareTo(b) < 0 ? b : a;
        }
    }
    #endregion


    #region (4) Konstruktor megszorítás
    class ObjektumGyar<T> where T : new() //OK
    {
        public T[] Letrehozas(int darab)
        {
            T[] A = new T[darab];
            for (int i = 0; i < darab; i++)
                A[i] = new T();
            return A;
        }
    }
    #endregion


    #region (5) Statikus osztály
    static class StatikusOsztály<T>
    {
        public static void Print() { Console.WriteLine("Hello"); }
        public static void Print<T>() { Console.WriteLine($"Szia"); }
    }
    #endregion


    #region (6) Több különböző paraméter
    class SokParaméter<A, B, C, D>
        where A : class
        where B : struct
        where C : IComparable
    {
        public A CsinálValamit(B valami) { return default(A); }
    }
    #endregion


    #region (7) Generikus öröklés
    class SzamLista : List<int> { }

    class GenerikusOs<A, B> { }
    class GenerikusLeszarmazott<T> : GenerikusOs<T, int> { }
    #endregion


    #region (8) Generikus metódus
    class GenMetodminta
    {
        public int Min(int a, int b)
        {
            return a.CompareTo(b) < 0 ? a : b;
        }
        public T Min<T>(T a, T b) where T : IComparable //T független mindentől
        {
            return a.CompareTo(b) < 0 ? a : b;
        }
    }

    class GenOsztályÉsMetodus<T>
    {
        public void Test<U>(GenOsztályÉsMetodus<U> minta) where U : T { } // U különböző de T-re megszorítjuk
    }
    #endregion


    #region (9) Osztályban osztály
    class Kulso
    {
        class Belso //külső számára látható, egyéb osztályok nem látják
        {
            public int valtozo;
        }

        public void Muvelet()
        {
            Belso x = new Belso();
            x.valtozo = 5;
        }
    }
    #endregion
}
