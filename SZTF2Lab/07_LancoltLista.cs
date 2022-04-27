using System;

namespace _06_Generikus_LancoltLista
{
    class E04_LancoltLista
    {
        public static void Teszt()
        {
            LancoltLista<string> lista = new LancoltLista<string>();    // A megadott <string> típus természetesen helyettesíthető bármilyen más típussal,
                                                                        // hiszen generikus listát hoztunk létre, csupán ennek megfelelően kell feltöltenünk elemekkel.
                                                                        // (Tehát <int> típusú lista esetén a "Béla" helyett valamilyen egész számot kell átadnunk.)
            lista.ElejereBeszur("Béla");
            lista.ElejereBeszur("Józsi");

            lista.VegereBeszur("Kristóf");
            lista.VegereBeszur("Pista");

            lista.Bejar(Kiir); // átadjuk a metódust ... callback

            Console.ReadLine();

            lista.Torol("Józsi");

            lista.Bejar(Kiir);

            Console.ReadLine();
        }

        public static void Kiir(string elem)
        {
            Console.WriteLine("Ennek a személynek a neve: " + elem);
        }

    }

    class LancoltLista<T>           // A listát bármely típussal szeretnénk használni, ezért generikus típust adunk meg neki (amelynek a neve esetünkben T).
    {                               // Ennek megfelelően létrehozhatunk <int>, <string>, <MyClass>, stb. típusú elemekkel feltöltött listákat.
        class ListaElem             // Beágyazott osztályok esetén a belső osztályt más osztályok egyáltalán nem látják (nem azonos a private kulcsszóval!),
        {                           // azonban mivel a ListaElem példányok csak a LancoltLista számára fontosak, erre nincs is szükség.
            public ListaElem kovetkezo;
            public T tartalom;      // A tartalom adattag fogja tárolni a tényleges értékét az adott listaelemnek.
        }                           // Egy int típusú lista esetében (ahol T => int) az átadott számot, egy string típusú lista esetében (ahol T => string) pedig az átadott szöveget.

        ListaElem fej;              // A fej fog a lista legelső elemére mutatni.
                                    // Amennyiben egyetlen elem van a listában, az lesz a fej.
                                    // Amennyiben nincs még elem a listában, a fej értéke NULL, mivel kezdeti értéket nem adunk neki.

        public T this[int i]        // 'indexer' - lehetőségével képesek vagyunk indexelni a listát
        {
            get     // i. elem kiolvasása
            {
                if (fej == null)
                    throw new IndexOutOfRangeException("Üres!");

                ListaElem p = fej;
                int j = 0;
                while (fej != null && j != i)
                {
                    j++;
                    p = p.kovetkezo;
                }

                if (p != null)
                    return p.tartalom;
                throw new IndexOutOfRangeException("Nincs ennyi eleme!");
            }
            set     // i. helyre beszúrás
            {
                ListaElem uj = new ListaElem();
                uj.tartalom = value;

                if (fej == null || i == 0)
                {
                    uj.kovetkezo = fej;
                    fej = uj;
                }
                else
                {
                    ListaElem p = fej;
                    int j = 1;
                    while (p.kovetkezo != null && j < i)
                    {
                        p = p.kovetkezo;
                        j++;
                    }
                    uj.kovetkezo = p.kovetkezo;
                    p.kovetkezo = uj;
                }
            }
        }


        public void ElejereBeszur(T elem)       // A lista elejére való beszúrás algoritmusa. Ebben az esetben az újonnan beszúrt elem lesz az új fej.
        {
            ListaElem uj = new ListaElem();     // Beszúráskor mindig létre kell hoznunk egy új ListaElem példányt,
            uj.tartalom = elem;                 // amelybe elhelyezzük a beszúrni kívánt adatot (vagyis a függvény bemeneti paraméterét).

            uj.kovetkezo = fej;
            fej = uj;
        }

        public void VegereBeszur(T elem)        // A lista végére való beszúrás algoritmusa.
        {                                       // Ebben az esetben végiglépkedünk a listán, majd amennyiben elértük a végét (p.kovetkezo == null), beszúrjuk az új elemet.
            ListaElem uj = new ListaElem();
            uj.tartalom = elem;

            if (fej == null)                    // Ha a fej értéke NULL, akkor tudjuk, hogy nincs elem a listában, így a beszúrni kívánt elem lesz a fej.
            {
                fej = uj;
            }
            else
            {
                ListaElem p = fej;
                while (p.kovetkezo != null)
                {
                    p = p.kovetkezo;
                }
                p.kovetkezo = uj;
            }
        }

        public void Torol(T elem)           // A listából való törlés algoritmusa. Bejárjuk a listát, majd amennyiben megtaláltuk az adott elemet, kitöröljük.
        {                                   //  Ha X.kovetkezo = Y, Y. kovetkezo = Z, akkor Y törlése esetén X.kovetkezo legyen Z (vagyis Y.kovetkezo).

            ListaElem e = null;             // Az 'e' változó mindig az előző elemre mutat.
            ListaElem p = fej;              // A 'p' változó az aktuálisan vizsgált elem.
            while (p != null && !p.tartalom.Equals(elem))   // Lépkedjünk végig a listán egészen addig, amíg el nem érünk a végére (tehát p == null),
            {                                               // vagy ameddig az aktuálisan vizsgált p elem nem egyenlő a keresett elemmel.
                e = p;
                p = p.kovetkezo;
            }
            if (p != null)                   // Ha az előző ciklus azért szakadt meg, mert elértük a lista végét, akkor p == null, tehát nincs törlendő elem, a metódus visszatér. 
            {
                if (e == null)               // Ha az e elem (vagyis az előző elem) értéke NULL, az csak abban az esetben fordulhat elő,
                {                            // ha a keresett elem rögtön a lista első vizsgált eleme, tehát a fej. Ebben az esetben a fejet töröljük,
                    fej = fej.kovetkezo;     // az új fej pedig a fejet követő elem lesz.
                }
                else
                {
                    e.kovetkezo = p.kovetkezo;
                }
            }
        }
        public void TeljesTorles()
        {
            fej = null;
        }

        public delegate void Muvelet(T elem);
        public void Bejar(Muvelet M)        // Delegált segítségével átadhatunk valamely metódust, amelyet szeretnénk végrehajtani a lista minden egyes elemének tartalmán. 
        {
            ListaElem p = fej;
            while (p != null)
            {
                M(p.tartalom);
                p = p.kovetkezo;
            }
        }
    }

}
