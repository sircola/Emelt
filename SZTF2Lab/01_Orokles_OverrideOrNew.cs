using System;

namespace Leszarmazas
{
    class OverrideOrNew
    {
        static void Main(string[] args)
        {
            OverrideOrNew1.Test();
            OverrideOrNew2.Test();
            OverrideOrNew3.Test();

            Console.ReadLine();
        }
    }

    #region 1
    class OverrideOrNew1
    {
        // Ha a leszármazott nem öröklődik akkor nem működik az: Ős ŐL = new Leszármazott();
        // --> a Leszármazott származzon le az Ős osztályból
        public static void Test()
        {
            Ős Ő = new Ős();
            Leszármazott L = new Leszármazott();
            Ős ŐL = new Leszármazott();

            Ő.Method1();
            L.Method1();
            L.Method2();
            ŐL.Method1();

            Console.WriteLine("-----");
            Console.ReadLine();
        }

        class Ős
        {
            public void Method1()
            {
                Console.WriteLine("Ős - Method1");
            }
        }
        class Leszármazott : Ős
        {
            public void Method2()
            {
                Console.WriteLine("Leszármazott - Method2");
            }
        }
    }
    #endregion

    #region 2
    class OverrideOrNew2
    {
        // Az ős osztály kap egy Method2-t és ezt meghívjük a
        //      Ő.Method2();    // Ős - Method2
        //      ŐL.Method2();   // Ős - Method2
        public static void Test()
        {
            Ős Ő = new Ős();
            Leszármazott L = new Leszármazott();
            Ős ŐL = new Leszármazott();

            Ő.Method1();
            Ő.Method2();
            L.Method1();
            L.Method2();
            ŐL.Method1();
            ŐL.Method2();

            Console.WriteLine("-----");
            Console.ReadLine();
        }

        class Ős
        {
            public void Method1()
            {
                Console.WriteLine("Ős - Method1");
            }
            public void Method2()
            {
                Console.WriteLine("Ős - Method2");
            }
        }
        class Leszármazott : Ős
        {
            public void Method2()  // véletlen egybeesés, hogy a metódus neve megegyezik az ősben szereplő egyik metódus nevével, hogy elrejtsük az ősét "normálisan" -> használjuk a 'new' kulcsszót
            {
                Console.WriteLine("Leszármazott - Method2");
            }
        }
    }
    #endregion

    #region 3
    class OverrideOrNew3
    {
        // A Leszármazott osztályhoz hozzáadjuk a Method1()-et
        // Hogy működjön az Ős osztály Method1() legyen virtual
        //      ŐL.Method2();   // Ős - Method2
        public static void Test()
        {
            Ős Ő = new Ős();
            Leszármazott L = new Leszármazott();
            Ős ŐL = new Leszármazott();

            Ő.Method1();
            Ő.Method2();
            L.Method1();
            L.Method2();
            ŐL.Method1();
            ŐL.Method2();

            Console.WriteLine("-----");
            Console.ReadLine();
        }

        class Ős
        {
            public virtual void Method1()  // a leszármazott azonos nevő metódusa felülírhatja az 'override' kulcsszó segítségével
            {
                Console.WriteLine("Ős - Method1");
            }
            public void Method2()
            {
                Console.WriteLine("Ős - Method2");
            }
        }
        class Leszármazott : Ős
        {
            public override void Method1()  // csak virtuális metódus irható felül
            {
                //base.Method1();  // továbbra is meghívható marad az ős azonos nevű metódusa
                Console.WriteLine("Leszármazott - Method1");
            }
            public new void Method2()
            {
                Console.WriteLine("Leszármazott - Method2");
            }
        }
    }
    #endregion
}
