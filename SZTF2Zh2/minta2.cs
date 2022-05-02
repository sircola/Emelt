using System;

namespace mintZH_masik
{
    interface IJáték
    {
        string Név { get; set; }

        bool JátszhatVele(int életkor);
    }


    class VeszélyesJáték : IJáték
    {
        public string Név { get; set; }


        public VeszélyesJáték(string név)
        {
            this.Név = név;
        }


        public bool JátszhatVele(int életkor)
        {
            return életkor >= 18;
        }
    }

    class SajátKészítésűJáték : IJáték
    {
        public string Név { get; set; }

        public SajátKészítésűJáték(string név)
        {
            this.Név = név;
        }


        public bool JátszhatVele(int életkor)
        {
            return true;
        }
    }

    delegate void ÚjJátékFelvéveHandler(IJáték jatek);

    //ehelyett ionkább a JátékLista belső osztályaként definiálom a JátékListaElem osztályt
    //class JátékListaElem2<T> where T : IJáték
    //{
    //    T tartalom;
    //}
    class JátékLista<T> where T : IJáték
    {
        public event ÚjJátékFelvéveHandler ÚjJátékFelvéveEvent;

        JátékListaElem fej;

        private int életkorKorlát;
        public int ÉletkorKorlát {
            get { return életkorKorlát; }
            set {
                this.életkorKorlát = value;
                TöröldAzÖsszesKorhatárosJátékot();
            }
        }

        public JátékLista(int életkorKorlát)
        {
            this.életkorKorlát = életkorKorlát;
        }

        class JátékListaElem
        {

            public T Tartalom { get; set; }

            public JátékListaElem Kovetkezo { get; set; }

            public JátékListaElem(T tartalom, JátékListaElem kovetkezo)
            {
                this.Tartalom = tartalom;
                this.Kovetkezo = kovetkezo;
            }

            public JátékListaElem()
            {

            }
        }

        //beszuras a lista végére ha játszhat vele
        public void ÚjJátékFelvétele(T jatek)
        {

            if (!jatek.JátszhatVele(életkorKorlát))
            {
                return;
            }

            JátékListaElem p = fej;
            JátékListaElem e = null;

            JátékListaElem uj = new JátékListaElem();
            uj.Tartalom = jatek;
            uj.Kovetkezo = null;

            //ha üres a lista, elso helyre szurok be
            if (fej == null)
            {
                fej = uj;

            }
            else //ha nem ures a lista, végére beszúrjuk
            {
                while (p != null)
                {
                    e = p;
                    p = p.Kovetkezo;
                }

                e.Kovetkezo = uj;
            }
            ÚjJátékFelvéveEvent?.Invoke(jatek);
        }

        public void JátékTörlése(T jatek)
        {
            JátékListaElem p = fej;
            JátékListaElem e = null;


            //a lista középső elemét
            //a lista utolsó elemét
            while (p != null && !p.Tartalom.Equals(jatek))
            {
                e = p;
                p = p.Kovetkezo;
            }
            //a lista 1. elemét
            if (p != null) //ha true akk megtaláltuk az elemet
            {
                if (e == null) //ezesetben az 1. elemt találtam meg
                {
                    fej = fej.Kovetkezo;
                }
                else
                {
                    //ha közbülső elem vagy uccsó elem egyben kezelem
                    e.Kovetkezo = p.Kovetkezo;
                }
            }
            else
            {
                throw new ArgumentException("nincs itt a játékod!!!");
            }

        }

        public T JátékKeresés(T jatek)
        {
            JátékListaElem p = fej;
            JátékListaElem e = null;

            while (p != null && !p.Tartalom.Equals(jatek))
            {
                e = p;
                p = p.Kovetkezo;
            }

            if (p != null)
            {
                //új sorrend beállítása
                if (p != fej)
                {
                    e.Kovetkezo = p.Kovetkezo;
                    p.Kovetkezo = fej;
                    fej = p;
                }

                return p.Tartalom;
            }
            else
            {
                throw new ArgumentException("Nem találtam a játékod! Játszál másssal hülyegyerek!");
            }
        }

        public void TöröldAzÖsszesKorhatárosJátékot()
        {
            JátékListaElem p = fej;
            JátékListaElem e = null;

            while (p != null)
            {
                if (!p.Tartalom.JátszhatVele(életkorKorlát))
                {
                    if (e == null)// ha p elso elem
                    {
                        fej = p.Kovetkezo;
                        p = fej;
                    }
                    else //akkor a kozbulso vagy uccsó helyen van
                    {
                        e.Kovetkezo = p.Kovetkezo;
                        p = e.Kovetkezo;
                    }
                }
                else
                {
                    e = p;
                    p = p.Kovetkezo;
                }
            }
        }
    }


    class Program
    {

        static void KiiroJatekFelvéve(IJáték játék)
        {
            Console.WriteLine(játék.Név);
        }
        static void Teszt1()
        {
            JátékLista<IJáték> lista = new JátékLista<IJáték>(13);

            SajátKészítésűJáték s1 = new SajátKészítésűJáték("pingpong");
            SajátKészítésűJáték s2 = new SajátKészítésűJáték("zoknibáb");
            VeszélyesJáték v1 = new VeszélyesJáték("lángszóró");
            VeszélyesJáték v2 = new VeszélyesJáték("egyetem");

            lista.ÚjJátékFelvéveEvent += KiiroJatekFelvéve;

            lista.ÚjJátékFelvétele(s1);
            lista.ÚjJátékFelvétele(s2);
            lista.ÚjJátékFelvétele(v1);
            lista.ÚjJátékFelvétele(v2);

            //lista.JátékTörlése(jatek3);

            //lista.JátékKeresés(jatek3);

            //lista.ÉletkorKorlát = 7;

            lista.JátékTörlése(s1);

            IJáték keresettJaték;
            try
            {
                //keresettJaték = lista.JátékKeresés(s2);
                keresettJaték = lista.JátékKeresés(s1);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }


            //iratkozzon le az eseménykezelőkről
            lista.ÚjJátékFelvéveEvent -= KiiroJatekFelvéve;

            Console.WriteLine();

            lista.ÚjJátékFelvétele(s1);

        }

        static void Main(string[] args)
        {
            Teszt1();
        }
    }
}

/***
A, Feladat
Készítsen egy IJáték nevű interfészt, ami az alábbiakat írja elő:
 Név nevű, szöveget visszaadó, csak olvasható tulajdonságot
 JátszhatVele(életkor : szám) nevű metódust
Készítsen egy JátékLista nevű, gyakoriság szerint rendezett láncolt lista osztályt az alábbiak szerint:
 Az osztály legyen generikus, a T generikus típus határozza meg, hogy miket lehet tárolni ebben
a láncolt listában. Készítse el az ehhez szükséges JátékListaElem osztályt is.
 Készítsen megszorítást, hogy csak IJáték interfészt megvalósító objektumokat lehessen tárolni
 A JátékLista tartalmazzon egy ÉletkorKorlát egész szám típusú tulajdonságot, amely a
konstruktorban kapjon kezdeti értéket.
 Az osztály rendelkezzen a megszokott láncolt lista metódusokkal:
o ÚjJátékFelvétele
Ellenőrizze először, hogy a paraméterként átadott objektum felvehető-e a listába (a
JátszhatVele metódus meghívása a listában tárolt életkorral igaz választ ad), és ha igen,
vegye fel a listába az elemet
o JátékTörlése
Törölje a paraméterként átadott objektumot a listából
o JátékKeresés
Adja vissza a paraméterként átadott nevű objektumot a listából
 A lista legyen gyakoriság szerint rendezett, emiatt minden új elem kerüljön a lista végére,
illetve minden keresés után a keresett elem kerüljön a lista elejére.
 Ha megváltozik a lista saját ÉletkorKorlát mezője, akkor törölje az összes olyan elemet a
láncból, amelyik nem felel meg az új korlátnak.
 Készítsen egy ÚjJátékFelvéve nevű eseménykezelőt, amelyhez csak olyan metódussal
lehessen feliratkozni, aminek egyetlen paramétere egy IJátékot-ot megvalósító objektum
(készítse el a szükséges delegáltat is).
 Módosítsa úgy az ÚjJátékFelvétele metódust, hogy (amennyiben a beszúrni kívánt objektum
életkora megfelelő), az hívja meg a fenti eseménykezelőt. Az eseményre feliratkozott
metódusoknak paraméterként adja át a beszúrandó objektumot.
Készítsen egy SajátKészítésűJáték nevű osztályt, amely megvalósítja az IJáték interfészt
 Egy mezőben tárolja a játék nevét (ezt adja vissza a Név tulajdonság)
 A JátszhatVele metódus mindig igaz értékkel térjen vissza
Készítsen egy VeszélyesJáték nevű osztályt, ami a SajátKészítésűJáték leszármazottja
 A JátszhatVele metódus csak 18 éven felüliek esetén adjon vissza igazat
Készítse el az alábbi főprogramot:
 Hozzon létre láncolt lista objektumot, iratkozzon fel az eseménykezelőkre
 A listákat töltse fel néhány minta objektummal (jó és rossz életkorral)
 Mutasson egy-egy példát törlésre és keresésre, iratkozzon le az eseménykezelőkrő
***/