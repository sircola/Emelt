using System;

namespace SZTF27het
{
    class NincsIlyenElemKivétel : Exception
    {
        public NincsIlyenElemKivétel() 
            : base("Nincs ilyen elem.")
        {
        }
    }

    class VoltMárKivétel : Exception
    {
        public VoltMárKivétel()
            : base("Volt már.")
        {
        }
    }

    public enum Oldal {
        jó,
        gonosz,
        civil
    }

    class Szuperhős
    {
        public string Név { get; set; }
        public bool Mutáns { get; set; }
        public int Erő { get; set; }
        public int Gyorsaság { get; set; }
        public Oldal Oldal { get; set; }

        public Szuperhős(string n, bool m, int e, int gy, Oldal o ) 
        {
            Név = n;
            Mutáns = m;
            Erő = e;
            Gyorsaság = gy;
            Oldal = o;
        }
    }

    class ListaElem
    {
        public Szuperhős szuperhős { get; set; }
        public ListaElem következő { get; set; }
    }

    class LáncoltLista
    {
        public ListaElem fej;

        public void ElemBeszúrás( Szuperhős sz )   // névsor szerint
        {
            if( fej == null )
            {
                fej = new ListaElem();
                fej.szuperhős = sz;
                fej.következő = null;
                return;
            }

            ListaElem p = fej;
            while (p != null)
            {
                if (sz.Név == p.szuperhős.Név)
                    throw new VoltMárKivétel();
                p = p.következő;
            }

            ListaElem a = null;
            ListaElem e = null;
            p = fej;

            while ( p != null && a == null )
            {
                if (string.Compare(p.szuperhős.Név, sz.Név) > 0)
                    a = p;
                else
                {
                    e = p;
                    p = p.következő;
                }
            }

            if( p == null )
            {
                e.következő = new ListaElem();
                e.következő.szuperhős = sz;
                e.következő.következő = null;
                return;
            }

            ListaElem új = new ListaElem();
            új.szuperhős = sz;
            
            if (e == null)
            {
                új.következő = fej;
                fej = új;
                return;
            }

            új.következő = a;
            e.következő = új;
        }

        public delegate void Bejáró(Szuperhős sz);

        public void Bejárás( Bejáró b )
        {
            ListaElem p = fej;
            while (p != null)
            {
                b?.Invoke(p.szuperhős);
                p = p.következő;
            }
        }

        public void Keresés( string név )
        {
            ListaElem p = fej;
            while (p != null)
            {
                if (név == p.szuperhős.Név)
                    return;
                p = p.következő;
            }
            
            throw new NincsIlyenElemKivétel();
        }

        public void Törlés( Szuperhős sz )
        {
            ListaElem p = fej;
            ListaElem e = null;

            while (p != null && p.szuperhős != sz )
            {
                e = p;
                p = p.következő;
            }

            if( p != null )
            {
                if (e == null)
                    fej = p.következő;
                else
                    e.következő = p.következő;
            }
        }

        public void Törlés( string név)
        {
            ListaElem p = fej;
            ListaElem e = null;

            while (p != null && p.szuperhős.Név != név)
            {
                e = p;
                p = p.következő;
            }

            if (p != null)
            {
                if (e == null)
                    fej = p.következő;
                else
                    e.következő = p.következő;
            }
        }

        public void Törlés(object o)
        {
            ListaElem p = fej;
            ListaElem e = null;
            Szuperhős sz = o as Szuperhős;

            while (p != null && 
                p.szuperhős.Név != sz.Név && 
                p.szuperhős.Mutáns != sz.Mutáns &&
                p.szuperhős.Erő != sz.Erő &&
                p.szuperhős.Gyorsaság != sz.Gyorsaság &&
                p.szuperhős.Oldal != sz.Oldal )
            {
                e = p;
                p = p.következő;
            }

            if (p != null)
            {
                if (e == null)
                    fej = p.következő;
                else
                    e.következő = p.következő;
            }
        }

        public LáncoltLista Szűrés( bool mutáns, Oldal oldal )
        {
            LáncoltLista szűrt = new LáncoltLista();
            ListaElem p = fej;
            
            while (p != null)
            {
                if (p.szuperhős.Mutáns == mutáns && p.szuperhős.Oldal == oldal)
                    szűrt.ElemBeszúrás(new Szuperhős(p.szuperhős.Név,p.szuperhős.Mutáns,p.szuperhős.Erő,p.szuperhős.Gyorsaság,p.szuperhős.Oldal));
                p = p.következő;
            }

            return szűrt;
        }

        public LáncoltLista Metszet( LáncoltLista másik )
        {
            LáncoltLista l = new LáncoltLista();
            ListaElem p = fej;
            ListaElem q = másik.fej;

            while( p!= null && q != null )
            {
                int i = string.Compare(p.szuperhős.Név, q.szuperhős.Név);
                if (i < 0)
                    p = p.következő;
                else
                if( i > 0)
                    q = q.következő;
                else
                {
                    l.ElemBeszúrás(new Szuperhős(p.szuperhős.Név, p.szuperhős.Mutáns, p.szuperhős.Erő, p.szuperhős.Gyorsaság, p.szuperhős.Oldal));
                    p = p.következő;
                    q = q.következő;
                }
            }

            return l;
        }

        public LáncoltLista Unió(LáncoltLista másik)
        {
            LáncoltLista l = new LáncoltLista();
            ListaElem p = fej;
            ListaElem q = másik.fej;

            while (p != null && q != null)
            {
                int i = string.Compare(p.szuperhős.Név, q.szuperhős.Név);
                if (i < 0)
                {
                    l.ElemBeszúrás(new Szuperhős(p.szuperhős.Név, p.szuperhős.Mutáns, p.szuperhős.Erő, p.szuperhős.Gyorsaság, p.szuperhős.Oldal));
                    p = p.következő;
                }
                else
                {
                    if (i > 0)
                    {
                        l.ElemBeszúrás(new Szuperhős(q.szuperhős.Név, q.szuperhős.Mutáns, q.szuperhős.Erő, q.szuperhős.Gyorsaság, q.szuperhős.Oldal));
                        q = q.következő;
                    }
                    else
                    {
                        l.ElemBeszúrás(new Szuperhős(p.szuperhős.Név, p.szuperhős.Mutáns, p.szuperhős.Erő, p.szuperhős.Gyorsaság, p.szuperhős.Oldal));
                        p = p.következő;
                        q = q.következő;
                    }
                }
            }

            return l;
        }

        public LáncoltLista Különbség(LáncoltLista másik)
        {
            LáncoltLista l = new LáncoltLista();
            ListaElem p = fej;
            ListaElem q = másik.fej;

            while (p != null && q != null)
            {
                int i = string.Compare(p.szuperhős.Név, q.szuperhős.Név);
                if (i < 0)
                {
                    l.ElemBeszúrás(new Szuperhős(p.szuperhős.Név, p.szuperhős.Mutáns, p.szuperhős.Erő, p.szuperhős.Gyorsaság, p.szuperhős.Oldal));
                    p = p.következő;
                }
                else
                if (i > 0)
                {
                    q = q.következő;
                }
                else
                {
                    p = p.következő;
                    q = q.következő;
                }
            }

            while (p != null)
            {
                l.ElemBeszúrás(new Szuperhős(p.szuperhős.Név, p.szuperhős.Mutáns, p.szuperhős.Erő, p.szuperhős.Gyorsaság, p.szuperhős.Oldal));
                p = p.következő;
            }

            return l;
        }

    }


    class Program
    {
        static void Main(string[] args)
        {
            LáncoltLista lista = new LáncoltLista();

            lista.ElemBeszúrás(new Szuperhős("Szupermen", true, 20, 20, Oldal.jó));
            lista.ElemBeszúrás(new Szuperhős("Batman", false, 18, 10, Oldal.jó));
            lista.ElemBeszúrás(new Szuperhős("Akvamen", true, 19, 11, Oldal.jó));
            lista.ElemBeszúrás(new Szuperhős("Pingvin", true, 9, 8, Oldal.gonosz));
            lista.ElemBeszúrás(new Szuperhős("Joker", false, 7, 8, Oldal.gonosz));
            lista.ElemBeszúrás(new Szuperhős("Pókmajom", true, 18, 11, Oldal.jó));
            lista.ElemBeszúrás(new Szuperhős("Lois", false, 3, 3, Oldal.civil));

            try
            {
                lista.ElemBeszúrás(new Szuperhős("Lois", false, 3, 3, Oldal.civil));
            }
            catch( VoltMárKivétel ex )
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                lista.Keresés("majommens");
            }
            catch( NincsIlyenElemKivétel ex )
            {
                Console.WriteLine(ex.Message);
            }

            lista.Törlés("Pingvin");

            lista.Bejárás(delegate (Szuperhős sz) { Console.WriteLine(sz.Név); });

            LáncoltLista a = lista.Szűrés(true, Oldal.jó);
            a.Bejárás(delegate (Szuperhős sz) { Console.WriteLine($"mutáns és jó: {sz.Név}"); });

            LáncoltLista m = new LáncoltLista();
            m.ElemBeszúrás(new Szuperhős("Joker", false, 7, 8, Oldal.gonosz));
            m.ElemBeszúrás(new Szuperhős("Pókmajom", true, 18, 11, Oldal.jó));
            m.ElemBeszúrás(new Szuperhős("Csodacsaj", true, 7, 8, Oldal.jó));
            m.ElemBeszúrás(new Szuperhős("Harlequinn", false, 7, 8, Oldal.gonosz));
            m.ElemBeszúrás(new Szuperhős("Méregcsók", true, 7, 8, Oldal.gonosz));
            m.ElemBeszúrás(new Szuperhős("Nick Fury Jr.", true, 7, 8, Oldal.jó));

            a = lista.Metszet(m);
            a.Bejárás(delegate (Szuperhős sz) { Console.WriteLine($"metszet: {sz.Név}"); });

            a = lista.Unió(m);
            a.Bejárás(delegate (Szuperhős sz) { Console.WriteLine($"unió: {sz.Név}"); });

            a = lista.Különbség(m);
            a.Bejárás(delegate (Szuperhős sz) { Console.WriteLine($"különbség: {sz.Név}"); });
        }
    }
}
