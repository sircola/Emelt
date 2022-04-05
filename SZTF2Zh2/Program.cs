using System;
using System.Collections.Generic;
using System.Collections;


namespace SZTF2Zh2
{
    public interface IListabaTeheto
    {
        int Kulcs { get; }
    }

    public class BeszurasEventArgs : EventArgs
    {
        public int Hely { get; }

        public BeszurasEventArgs(int hely)
        {
            this.Hely = hely;
        }
    }

    public class BabonaException : Exception
    {
        public BabonaException() : base("Babonás okok miatt nem tehető be!")
        {
        }
    }

    public class ListaBejaro<T> : IEnumerator<T>
    {
        public T Current => aktualis.Tartalom;
        object IEnumerator.Current => aktualis.Tartalom;

        private ListaElem<T> fej;
        private ListaElem<T> aktualis;

        public ListaBejaro(ListaElem<T> fej)
        {
            this.fej = fej;
            this.aktualis = new ListaElem<T>();
            this.aktualis.Kovetkezo = fej;
        }

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            if (aktualis == null)
            {
                return false;
            }
            aktualis = aktualis.Kovetkezo;
            return aktualis != null;
        }

        public void Reset()
        {
            aktualis = new ListaElem<T>();
            aktualis.Kovetkezo = fej;
        }
    }

    public class Szemely : IListabaTeheto, IComparable
    {
        public string Nev { get; set; }
        public int Eletkor { get; set; }

        public Szemely(string nev, int eletkor)
        {
            Nev = nev;
            Eletkor = eletkor;
        }

        public int Kulcs => Nev.Length * Eletkor;

        public int CompareTo(object obj)
        {
            Szemely masik = obj as Szemely;
            return (Nev.Length * Eletkor).CompareTo(masik.Nev.Length * masik.Eletkor);
        }
    }

    public class ListaElem<T>
    {
        public T Tartalom { get; set; }
        public ListaElem<T> Kovetkezo { get; set; }
    }
    public class LancoltLista<T> : IEnumerable<T> where T : IListabaTeheto, IComparable
    {
        public delegate void BeszurasHandler(int hely);
        public event BeszurasHandler BeszurasTortent;
        public event EventHandler<BeszurasEventArgs> BeszurasTortent2;

        public LancoltLista(int korlat)
        {
            this.Korlat = korlat;
        }

        private ListaElem<T> fej;

        public ListaElem<T> ElsoElem {
            get {
                return fej;
            }
        }

        private int korlat;
        public int Korlat {
            get {
                return korlat;
            }
            set {
                korlat = value;
                Tisztit();
            }
        }

        private int kulcsosszeg;

        public void Tisztit()
        {
            while (kulcsosszeg > Korlat)
            {
                if (fej != null)
                {
                    kulcsosszeg -= fej.Tartalom.Kulcs;
                    fej = fej.Kovetkezo;
                }
            }
        }

        public void ElemTorles(int kulcs)
        {
            //több elemnek is lehet ugyanaz a kulcsa!
            ListaElem<T> p = fej;
            ListaElem<T> e = null;
            while (p != null)
            {
                if (p.Tartalom.Kulcs == kulcs)
                {
                    if (e == null)
                    {
                        fej = p.Kovetkezo;
                    }
                    else
                    {
                        e.Kovetkezo = p.Kovetkezo;
                    }
                    kulcsosszeg -= p.Tartalom.Kulcs;
                }
                else
                {
                    e = p;
                    p = p.Kovetkezo;
                }
            }
        }

        public void SpecialisBeszuras(T tartalom)
        {
            if (tartalom.Kulcs == 13)
            {
                throw new BabonaException();
            }

            int hely = 0;
            ListaElem<T> uj = new ListaElem<T>();
            uj.Tartalom = tartalom;
            ListaElem<T> p = fej;
            ListaElem<T> e = null;
            while (p != null && p.Tartalom.Kulcs < tartalom.Kulcs)
            {
                e = p;
                p = p.Kovetkezo;
                hely++;
            }
            if (e == null)
            {
                uj.Kovetkezo = fej;
                fej = uj;
            }
            else
            {
                uj.Kovetkezo = p;
                e.Kovetkezo = uj;
            }
            kulcsosszeg += uj.Tartalom.Kulcs;
            BeszurasTortent?.Invoke(hely);
            BeszurasTortent2?.Invoke(this, new BeszurasEventArgs(hely));
            Tisztit();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new ListaBejaro<T>(ElsoElem);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new ListaBejaro<T>(ElsoElem);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            LancoltLista<Szemely> juzerek = new LancoltLista<Szemely>(1000);
            juzerek.SpecialisBeszuras(new Szemely("Marika", 70)); //420
            juzerek.SpecialisBeszuras(new Szemely("Gyuszi", 60)); //360
            juzerek.SpecialisBeszuras(new Szemely("Eva", 50)); //150
            juzerek.SpecialisBeszuras(new Szemely("Aurél", 70)); //350

            foreach (Szemely item in juzerek)
            {
                Console.WriteLine(item.Nev + " " + item.Eletkor);
            }

            Console.ReadLine();
        }
    }
}

/*
1.Készítsen egy generikus láncolt lista adatszerkezetet, mely olyan elemek felvételére 
képes csak és
kizárólag, melyek megvalósítják az IListabaTeheto és az IComparable interfészeket! (12 pont)
2.Az interfész írjon elő egy int típusú csak olvasható tulajdonságot, mely egy kulcsként fog
funkcionálni! (2 pont)
3.Készítsen egy tetszőleges osztályt, amely megvalósítja ezt az interfészt! (Pl: készítsen 
egy személy
osztályt (név, életkor). A kulcsképzés: név betűinek száma * életkor) (5 pont)
4.A láncolt listának készítsen egy speciális beszúrás metódust! A metódus úgy működjön, 
hogy a
beszúrandó elemnek megkeresi a kulcs szerinti helyét. Vagyis legyen a lista eleve 
rendezett! (8 pont)
5.Amint az elemet beszúrta, indítson el egy eseményt, mely a feliratkozó metódusok számára 
átadja
hogy hányadik helyre szúrta be az elemet, valamint átadja az elem kulcsát! (3 pont)
6.Az előző eseményt készítse el más néven abban a formában is, hogy a felhasznált
metódusreferencia a .NET beépített generikus EventHandler<> metódusreferenciája legyen!
Készítsen megfelelő eseményparaméter osztályt! (bónusz 4 pont)
7.Készítsen a láncolt listának egy korlát adattagot, mely egy egész szám. Ez a konstruktoron 
át
legyen beállítható. Készítsen hozzá írható/olvasható tulajdonságot is! Ha egy spec. beszúrás 
után
az elemek kulcsainak összege meghaladja a korlátot, akkor kezdjen el a legkisebb elemektől
kezdve elemeket törölgetni a listából addig, amíg újra a korlát alá nem kerül az elemek
összege!
Ha az írható tulajdonságon keresztül megváltoztatjuk a korlát értékét, akkor szintén addig
törölgessünk elemeket a legkisebb elemektől kezdve, amíg a korlát alá nem kerül az elemek
kulcsainak összege. (6 pont)
8.Hogyha olyan elemet akarunk beszúrni, melynek kulcsa éppen 13, akkor dobjon el egy saját
készítésű BabonaException nevű kivételt, melyben szerepeljen a következő hibaüzenet: „Babonás
okok miatt nem tehető be!” (4 pont)
9.Készítsen a listának egy metódust, mellyel a megadott kulcsú elem törölhető! (4 pont)
10.Biztosítsa, hogy a lista a foreach ciklus segítségével bejárható legyen! (6 pont)
*/