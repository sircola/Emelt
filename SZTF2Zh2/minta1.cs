using System;
using System.Collections;
using System.Collections.Generic;

namespace mintaZH
{
    class ListaBejaro<T> : IEnumerator<T> where T : IListabaTeheto, IComparable
    {
        LancoltLista<T>.ListaElem fej;
        LancoltLista<T>.ListaElem aktualis;

        public ListaBejaro(LancoltLista<T>.ListaElem fej)
        {
            this.fej = fej;
            this.aktualis = new LancoltLista<T>.ListaElem();
            this.aktualis.Kovetkezo = fej;
        }

        public T Current { get { return aktualis.Tartalom; } }

        object IEnumerator.Current { get { return aktualis.Tartalom; } }

        public void Dispose()
        {

        }

        public bool MoveNext()
        {
            if (aktualis == null) //ha üres a lista
            {
                return false;
            }

            aktualis = aktualis.Kovetkezo;
            return aktualis != null;

        }

        public void Reset()
        {
            this.aktualis = new LancoltLista<T>.ListaElem();
            this.aktualis.Kovetkezo = fej;
        }
    }

    public delegate void Beszurashandler(int hanyadikHelyre, int kulcs);

    public delegate void EventHandler<T>(EseményParaméter paraméter);
    class LancoltLista<T> : IEnumerable<T> where T : IListabaTeheto, IComparable
    {
        public event Beszurashandler BeszurasEvent;

        public event EventHandler<T> Event2;

        private ListaElem fej;

        private int korlát;

        public int Korlát {
            get { return this.korlát; }
            set {
                this.korlát = value;
                ToroljElemeketHaKell();
            }
        }

        public LancoltLista(int korlát)
        {
            this.korlát = korlát;
        }


        //Itt volt a ListaElem
        public class ListaElem
        {
            public T Tartalom { get; set; }
            public ListaElem Kovetkezo { get; set; }
        }

        public int kulcsokÖsszege()
        {
            ListaElem p = fej;
            int összeg = 0;

            while (p != null)
            {
                összeg = összeg + p.Tartalom.Kulcs;
                p = p.Kovetkezo;
            }

            return összeg;
        }

        public void ElsoElemTorlese()
        {
            if (fej == null)
            {
                return;
            }

            ListaElem p = fej;

            fej = p.Kovetkezo;
        }

        public void RendezettListábaBeszúrás(int kulcs, T tartalom)
        {
            int counter = 1;
            ListaElem uj = new ListaElem();
            uj.Tartalom = tartalom;

            ListaElem p = fej;
            ListaElem e = null;

            //ha üres a lista
            if (fej == null)
            {
                fej = uj;
                uj.Kovetkezo = null;
                //BeszurasEvent?.Invoke(counter, kulcs);

                EseményParaméter paraméter = new EseményParaméter("Teszt esemény neve", 9);
                Event2?.Invoke(paraméter);
            }


            //ha az első elem elé kell beszúrni
            else if (fej.Tartalom.Kulcs > kulcs)
            {
                uj.Kovetkezo = fej;
                fej = uj;
                //BeszurasEvent?.Invoke(counter, kulcs);

                EseményParaméter paraméter = new EseményParaméter("Teszt esemény neve", 9);
                Event2?.Invoke(paraméter);
            }
            else
            {
                p = fej;
                e = null;
                while (p != null && p.Tartalom.Kulcs < kulcs)
                {
                    e = p;
                    p = p.Kovetkezo;
                    counter++;
                }
                if (p == null) // végig értünk a listán--> uccsó után szurunk be
                {
                    uj.Kovetkezo = null;
                    e.Kovetkezo = uj;
                    //BeszurasEvent?.Invoke(counter, kulcs);

                    EseményParaméter paraméter = new EseményParaméter("Teszt esemény neve", 9);
                    Event2?.Invoke(paraméter);
                }
                else // a lista közepébe szurunk be
                {
                    uj.Kovetkezo = p;
                    e.Kovetkezo = uj;
                    //BeszurasEvent?.Invoke(counter, kulcs);

                    EseményParaméter paraméter = new EseményParaméter("Teszt esemény neve", 9);
                    Event2?.Invoke(paraméter);
                }
            }

        }


        public void RendezettListabaBeszurasTorolIsHaKell(int kulcs, T tartalom)
        {
            if (kulcs == 13)
            {
                throw new BabonaException("Babonás okok miatt nem tehető be!");
            }

            RendezettListábaBeszúrás(kulcs, tartalom);
            ToroljElemeketHaKell();
        }

        public void ToroljElemeketHaKell()
        {

            while (fej != null && kulcsokÖsszege() > korlát)
            {
                ElsoElemTorlese();
            }
        }

        public void ListábólTörlés(int kulcs)
        {
            ListaElem p = fej;
            ListaElem e = null;

            while (p != null && p.Tartalom.Kulcs != kulcs)
            {
                e = p;
                p = p.Kovetkezo;
            }

            if (p != null)
            {
                if (e == null)
                {
                    fej = p.Kovetkezo;
                }
                else
                {
                    e.Kovetkezo = p.Kovetkezo;
                }
                //felszabadítás nem kell c# garbage collerctor elintézi
            }
            else
            {
                throw new ArgumentException("nincs ilyen elem");
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            ListaBejaro<T> bejaro = new ListaBejaro<T>(fej);
            return bejaro;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            ListaBejaro<T> bejaro = new ListaBejaro<T>(fej);
            return bejaro;
        }
    }

    interface IListabaTeheto
    {
        int Kulcs { get; }
    }

    class BabonaException : System.Exception
    {
        public BabonaException(string message) : base(message)
        {

        }
    }

    public class EseményParaméter
    {
        public string eseményNeve;
        public int eseménySzama;

        public EseményParaméter(string eseményNeve, int eseménySzama)
        {
            this.eseményNeve = eseményNeve;
            this.eseménySzama = eseménySzama;
        }
    }

    class Személy : IListabaTeheto, IComparable
    {
        public string Név { get; set; }

        public int Életkor { get; set; }
        public int Kulcs { get { return this.Név.Length * this.Életkor; } }

        public Személy(string név, int életkor)
        {
            this.Név = név;
            this.Életkor = életkor;
        }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }
    }


    class Program
    {
        static void BeszurasKiiro(int hanyadikHelyre, int kulcs)
        {
            Console.WriteLine($"A '{hanyadikHelyre}'.  helyre ebszúrtam a '{kulcs}' kulcsú elemet!");
        }

        static void Event2Kiiro(EseményParaméter paraméter)
        {
            Console.WriteLine(paraméter.eseményNeve + "\t" + paraméter.eseménySzama);
        }

        static void Teszt1()
        {
            LancoltLista<Személy> lista = new LancoltLista<Személy>(180);
            Személy s1 = new Személy("B", 77);
            Személy s2 = new Személy("A", 12);
            Személy s3 = new Személy("C", 80);

            lista.BeszurasEvent += BeszurasKiiro;

            lista.Event2 += Event2Kiiro;

            lista.RendezettListabaBeszurasTorolIsHaKell(s1.Kulcs, s1);
            lista.RendezettListabaBeszurasTorolIsHaKell(s2.Kulcs, s2);
            lista.RendezettListabaBeszurasTorolIsHaKell(s3.Kulcs, s3);

            try
            {
                Személy s4 = new Személy("D", 13);
                lista.RendezettListabaBeszurasTorolIsHaKell(s4.Kulcs, s4);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }


            //lista.Korlát = 100;

            try
            {
                lista.ListábólTörlés(80);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            

            Console.WriteLine(lista.kulcsokÖsszege());

            Console.WriteLine();

            foreach (var item in lista)
            {
                Console.WriteLine(item.Név);
            }

            ;
        }
        static void Main(string[] args)
        {
            Teszt1();
            
        }
    }
}

/***
1. Készítsen egy generikus láncolt lista adatszerkezetet, mely olyan elemek felvételére képes csak és
kizárólag, melyek megvalósítják az IListabaTeheto és az IComparable interfészeket! (12 pont)
2. Az interfész írjon elő egy int típusú csak olvasható tulajdonságot, mely egy kulcsként fog
funkcionálni! (2 pont)
3. Készítsen egy tetszőleges osztályt, amely megvalósítja ezt az interfészt! (Pl: készítsen egy személy
osztályt (név, életkor). A kulcsképzés: név betűinek száma * életkor) (5 pont)
4. A láncolt listának készítsen egy speciális beszúrás metódust! A metódus úgy működjön, hogy a
beszúrandó elemnek megkeresi a kulcs szerinti helyét. Vagyis legyen a lista eleve rendezett! (8
pont)
5. Amint az elemet beszúrta, indítson el egy eseményt, mely a feliratkozó metódusok számára átadja
hogy hányadik helyre szúrta be az elemet, valamint átadja az elem kulcsát! (3 pont)
6. Az előző eseményt készítse el más néven abban a formában is, hogy a felhasznált
metódusreferencia a .NET beépített generikus EventHandler<> metódusreferenciája legyen!
Készítsen megfelelő eseményparaméter osztályt! (bónusz 4 pont)
7. Készítsen a láncolt listának egy korlát adattagot, mely egy egész szám. Ez a konstruktoron át
legyen beállítható. Készítsen hozzá írható/olvasható tulajdonságot is! Ha egy spec. beszúrás után
az elemek kulcsainak összege meghaladja a korlátot, akkor kezdjen el a legkisebb elemektől
kezdve elemeket törölgetni a listából addig, amíg újra a korlát alá nem kerül az elemek összege!
Ha az írható tulajdonságon keresztül megváltoztatjuk a korlát értékét, akkor szintén addig
törölgessünk elemeket a legkisebb elemektől kezdve, amíg a korlát alá nem kerül az elemek
kulcsainak összege. (6 pont)
8. Hogyha olyan elemet akarunk beszúrni, melynek kulcsa éppen 13, akkor dobjon el egy saját
készítésű BabonaException nevű kivételt, melyben szerepeljen a következő hibaüzenet: „Babonás
okok miatt nem tehető be!” (4 pont)
9. Készítsen a listának egy metódust, mellyel a megadott kulcsú elem törölhető! (4 pont)
10. Biztosítsa, hogy a lista a foreach ciklus segítségével bejárható legyen! (6 pont)
***/