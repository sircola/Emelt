using System;
using System.Collections.Generic;

namespace SZTF2HF
{
    class LáncoltLista<T> where T : IComparable
    {
        class ListaElem
        {
            public ListaElem következő;
            public T tartalom;
        }

        ListaElem fej;

        public T this[int i] {
            get {
                if (fej == null)
                    return default(T);
                ListaElem p = fej;
                int j = 0;
                while (fej != null && j != i)
                {
                    ++j;
                    p = p.következő;
                }
                if (p != null)
                    return p.tartalom;
                return default(T);
            }

            set {
                ListaElem új = new ListaElem();
                új.tartalom = value;
                if (fej == null && i == 0)
                {
                    új.következő = fej;
                    fej = új;
                }
                else
                {
                    ListaElem p = fej;
                    int j = 1;
                    while (p.következő != null && j < i)
                    {
                        p = p.következő;
                        ++j;
                    }
                    új.következő = p.következő;
                    p.következő = új;
                }
            }
        }
        public void ElejéreBeszúr( T elem )
        {
            ListaElem új = new ListaElem();
            új.tartalom = elem;
            új.következő = fej;
            fej = új;
        }

        public void VégéreBeszúr( T elem )
        {
            ListaElem új = new ListaElem();
            új.tartalom = elem;

            if (fej == null)
                fej = új;
            else
            {
                ListaElem p = fej;
                while (p.következő != null)
                    p = p.következő;
                p.következő = új;
            }
        }

        public void Beszúr(T elem)
        {
            if (fej == null)
            {
                ElejéreBeszúr(elem);
                return;
            }

            ListaElem p = fej;
            while (p != null)
            {
                if ( elem.CompareTo(p.tartalom) == 0 )
                    throw new ArgumentException("Volt már ilyen.");
                p = p.következő;
            }

            ListaElem a = null;
            ListaElem e = null;
            p = fej;

            while (p != null && a == null)
            {
                if (p.tartalom.CompareTo(elem) > 0)
                    a = p;
                else
                {
                    e = p;
                    p = p.következő;
                }
            }

            if (p == null)
            {
                e.következő = new ListaElem();
                e.következő.tartalom = elem;
                e.következő.következő = null;
                return;
            }

            ListaElem új = new ListaElem();
            új.tartalom = elem;

            if (e == null)
            {
                új.következő = fej;
                fej = új;
                return;
            }

            új.következő = a;
            e.következő = új;
        }

        public void Töröl( T elem )
        {
            ListaElem e = null;
            ListaElem p = fej;
            while( p != null && !p.tartalom.Equals(elem) )
            {
                e = p;
                p = p.következő;
            }
            if( p != null )
            {
                if (e == null)
                    fej = fej.következő;
                else
                    e.következő = p.következő;
            }
        }

        public void TeljesTörlés()
        {
            fej = null;
        }

        public delegate void Művelet(T elem);

        public void Bejár( Művelet M )
        {
            ListaElem p = fej;
            while( p != null )
            {
                M(p.tartalom);
                p = p.következő;
            }
        }
    }

    public enum KurzusKategória
    {
        Online,
        Offline
    }

    public enum VizsgaTípus
    {
        Szóbeli,
        Írásbeli
    }

    interface IKurzus : IComparable
    {
        string Kód { get;  }
        KurzusKategória Típus { get; }
        DateTime Nap { get; set; }
        DateTime ÓraKezdet { get; set; }
        DateTime ÓraVég { get; set; }
        bool Ütközik(List<IKurzus> l);
        Tantárgy Tantárgy { get; set; }
    }

    abstract class Tantárgy
    {
        public LáncoltLista<IKurzus> kurzusok = new LáncoltLista<IKurzus>();

        public string Név { get; }
        public int Kredit { get; } 
        public int Félév { get; }
        public int Kreditek { get; set; }

        protected Tantárgy(string név, int kredit, int félév, int kreditek = 0)
        {
            Név = név;
            Kredit = kredit;
            Félév = félév;
            Kreditek = kreditek;
        }
    }

    class Gyakorlat : IKurzus
    {
        public string Kód { get; set; }
        public KurzusKategória Típus { get; } = KurzusKategória.Offline;
        public DateTime ÓraKezdet { get; set; }
        public DateTime ÓraVég { get; set; }
        public Tantárgy Tantárgy { get; set; }
        public DateTime Nap { get; set; }

        public int CompareTo(object obj)
        {
            IKurzus másik = obj as IKurzus;
            if (másik.Nap.Day != Nap.Day)
                return Nap.Day - másik.Nap.Day;
            if (Nap.Hour < másik.Nap.Hour)
            {
                if (Nap.Hour + Nap.Minute <= másik.Nap.Hour)
                    return 1;
                else
                    return 0;
            }
            if (másik.Nap.Hour + másik.Nap.Minute <= Nap.Hour)
                return -1;
            return 0;
        }

        public bool Ütközik(List<IKurzus> l)
        {
            foreach (IKurzus i in l)
            {
                return CompareTo(i) == 0 ? true : false;
            }
            return false;
        }
    }

    class Labor : Gyakorlat
    {
    }

    class Előadás : IKurzus
    {
        public VizsgaTípus vizsgatípus { get; set; }
        public string Kód { get; set; }
        public KurzusKategória Típus { get; }
        public DateTime ÓraKezdet { get; set; }
        public DateTime ÓraVég { get; set; }
        public Tantárgy Tantárgy { get; set; }
        public DateTime Nap { get; set; }

        public int CompareTo(object obj)
        {
            IKurzus másik = obj as IKurzus;
            if (másik.Nap.Day != Nap.Day)
                return Nap.Day - másik.Nap.Day;
            if (Nap.Hour < másik.Nap.Hour)
            {
                if (Nap.Hour + Nap.Minute <= másik.Nap.Hour)
                    return 1;
                else
                    return 0;
            }
            if (másik.Nap.Hour + másik.Nap.Minute <= Nap.Hour)
                return -1;
            return 0;
        }

        public bool Ütközik(List<IKurzus> l)
        {
            foreach (IKurzus i in l)
            {
                return CompareTo(i)==0?true:false;
            }
            return false;
        }
    }

    class VizsgaKurzus : IKurzus
    {
        public string Kód { get; set; }
        public KurzusKategória Típus { get; }
        public DateTime ÓraKezdet { get; set; }
        public DateTime ÓraVég { get; set; }
        public Tantárgy Tantárgy { get; set; }
        public DateTime Nap { get; set; }

        public int CompareTo(object obj)
        {
            return -1;
        }

        public bool Ütközik(List<IKurzus> l)
        {
            return false;
        }
    }

    class SZTF1 : Tantárgy
    {
        public SZTF1()
            : base("SZTF1",6,1)
        {
            kurzusok.VégéreBeszúr(new Előadás()
            {
                Kód = "SF1_EA",
                vizsgatípus = VizsgaTípus.Szóbeli,
                Nap = new DateTime(2022,1,1),
                ÓraKezdet = new DateTime(2022,1,1,9,0,0),
                ÓraVég = new DateTime(2022,1,1,11,0,0)
            });

            kurzusok.VégéreBeszúr(new Labor()
            {
                Kód = "SF1_LA_01",
                Nap = new DateTime(2022, 1, 2),
                ÓraKezdet = new DateTime(2022, 1, 1, 15, 0, 0),
                ÓraVég = new DateTime(2022, 1, 1, 16, 30, 0)
            });
            kurzusok.VégéreBeszúr(new Labor()
            {
                Kód = "SF1_LA_02",
                Nap = new DateTime(2022, 1, 2),
                ÓraKezdet = new DateTime(2022, 1, 1, 17, 0, 0),
                ÓraVég = new DateTime(2022, 1, 1, 18, 30, 0)
            });

            kurzusok.VégéreBeszúr(new Labor()
            {
                Kód = "SF1_LA_03",
                Nap = new DateTime(2022, 1, 3),
                ÓraKezdet = new DateTime(2022, 1, 1, 9, 0, 0),
                ÓraVég = new DateTime(2022, 1, 1, 10, 30, 0)
            });
            kurzusok.VégéreBeszúr(new Labor()
            {
                Kód = "SF1_LA_04",
                Nap = new DateTime(2022, 1, 3),
                ÓraKezdet = new DateTime(2022, 1, 1, 11, 0, 0),
                ÓraVég = new DateTime(2022, 1, 1, 12, 30, 0)
            });

            kurzusok.VégéreBeszúr(new Labor()
            {
                Kód = "SF1_LA_05",
                Nap = new DateTime(2022, 1, 4),
                ÓraKezdet = new DateTime(2022, 1, 1, 13, 0, 0),
                ÓraVég = new DateTime(2022, 1, 1, 14, 30, 0)
            });
            kurzusok.VégéreBeszúr(new Labor()
            {
                Kód = "SF1_LA_06",
                Nap = new DateTime(2022, 1, 1),
                ÓraKezdet = new DateTime(2022, 1, 1, 11, 0, 0),
                ÓraVég = new DateTime(2022, 1, 1, 12, 30, 0)
            });
        }
    }

    class Analízis : Tantárgy
    {
        public Analízis()
            : base("Analízis I.", 6, 1)
        {
            kurzusok.VégéreBeszúr(new Előadás()
            {
                Kód = "AN1_EA",
                vizsgatípus = VizsgaTípus.Írásbeli,
                Nap = new DateTime(2022, 1, 2),
                ÓraKezdet = new DateTime(1, 1, 2, 9, 0, 0),
                ÓraVég = new DateTime(1, 1, 2, 11, 0, 0)
            });

            kurzusok.VégéreBeszúr(new Gyakorlat()
            {
                Kód = "AN1_GY_01",
                Nap = new DateTime(2022, 1, 2),
                ÓraKezdet = new DateTime(1, 1, 1, 11, 0, 0),
                ÓraVég = new DateTime(1, 1, 1, 12, 0, 0)
            });
            kurzusok.VégéreBeszúr(new Gyakorlat()
            {
                Kód = "AN1_GY_02",
                Nap = new DateTime(2022, 1, 2),
                ÓraKezdet = new DateTime(2022, 1, 1, 12, 0, 0),
                ÓraVég = new DateTime(2022, 1, 1, 13, 0, 0)
            });

            kurzusok.VégéreBeszúr(new Gyakorlat()
            {
                Kód = "AN1_GY_03",
                Nap = new DateTime(2022, 1, 2),
                ÓraKezdet = new DateTime(2022, 1, 1, 13, 0, 0),
                ÓraVég = new DateTime(2022, 1, 1, 14, 0, 0)
            });
        }
    }

    class AzonosKurzusKivétel : Exception
    {
        public object BeszúrandóElem { get; }
        public object ÜtközőElem { get;  }

        public AzonosKurzusKivétel(object ütköző, object beszúrandó )
        {
            ÜtközőElem = ütköző;
            BeszúrandóElem = beszúrandó;
        }
    }


    abstract class Backtrack
    {
        protected int N { get; set; }
        protected int[] M;
        protected object[,] R;

        public abstract bool Ft(int szint, object o);
        public abstract bool Fk(int szint, object o, object[] E);

        public object[] Kereses()
        {
            bool van = false;
            object[] E = new object[N];

            Probal(0, ref van, E);

            if (van == false)
                throw new ArgumentException("Nincs megoldás.");

            return E;
        }

        public void Probal(int szint, ref bool van, object[] E)
        {
            int i = -1;

            while (!van && i < M[szint] - 1)
            {
                ++i;
                if (Ft(szint, R[szint, i]))
                {
                    if (Fk(szint, R[szint, i], E))
                    {
                        E[szint] = R[szint, i];
                        if (szint == N - 1)
                        {
                            van = true;
                        }
                        else
                        {
                            Probal(szint + 1, ref van, E);
                        }
                    }
                }
            }
        }
    }


    class Órarend : Backtrack
    {
        LáncoltLista<IKurzus> kurzusok = new LáncoltLista<IKurzus>();

        public void AzonosKurzusEllenőrzés(IKurzus beszúrandó )
        {
            kurzusok.Bejár(delegate (IKurzus k)
            {
                if (beszúrandó.Kód == k.Kód && beszúrandó.Típus == k.Típus)
                    throw new AzonosKurzusKivétel(k, beszúrandó);
            });
        }

        public void TantárgyListaHozzáadás( List<Tantárgy> l )
        {
            foreach (Tantárgy i in l)
            {
                TantárgyHozzádaás(i);
            }
        }

        public void TantárgyHozzádaás( Tantárgy t )
        {
            int i = 0;
            while (t.kurzusok[i] != null)
            {
                AzonosKurzusEllenőrzés( t.kurzusok[i] );
                kurzusok.Beszúr(t.kurzusok[i++]);
            }
        }

        public void KurzusHozzáadás( IKurzus k )
        {
            AzonosKurzusEllenőrzés( k);
            kurzusok.Beszúr(k);
        }

        public void KurzusListaHozzáadás( List<IKurzus> l )
        {
            foreach (IKurzus i in l)
            {
                KurzusHozzáadás(i);
            }
        }

        public void KiírLista()
        {
            kurzusok.Bejár( delegate (IKurzus k)
            {
                string[] hét = { "hétfő", "kedd", "szerda", "csütörtök", "péntek" };
                Console.WriteLine($"{k.Kód}: {hét[k.Nap.Day-1]} {k.ÓraKezdet.Hour}:{k.ÓraKezdet.Minute:00} - {k.ÓraVég.Hour}:{k.ÓraVég.Minute:00}");
            });
        }

        public void TörölKurzusKód( string s)
        {
            int i = 0;
            while (kurzusok[i] != null)
            {
                if (kurzusok[i].Kód == s)
                    kurzusok.Töröl(kurzusok[i]);
                else
                    ++i;
            }
        }

        public void TörölKurzus( KurzusKategória k )
        {
            int i = 0;
            while (kurzusok[i] != null)
            {
                if (kurzusok[i].Típus == k )
                    kurzusok.Töröl(kurzusok[i]);
                else
                    ++i;
            }
        }

        public void TörölKurzusNap( int nap )
        {
            int i = 0;
            while( kurzusok[i] != null )
            {
                if (kurzusok[i].Nap.Day == nap)
                    kurzusok.Töröl(kurzusok[i]);
                else
                    ++i;
            }
        }

        public void TörölKurzusHossz( int h )
        {
            int i = 0;
            while (kurzusok[i] != null)
            {
                if ((kurzusok[i].ÓraVég.Hour*60 + kurzusok[i].ÓraVég.Minute) - (kurzusok[i].ÓraKezdet.Hour * 60 + kurzusok[i].ÓraKezdet.Minute) == h)
                    kurzusok.Töröl(kurzusok[i]);
                else
                    ++i;
            }
        }

        public override bool Fk(int szint, object o, object[] E)
        {
            return true;
        }

        public override bool Ft(int szint, object o)
        {
            return true;
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            Órarend órarend = new Órarend();
            órarend.TantárgyHozzádaás(new Analízis());
            órarend.TantárgyHozzádaás(new SZTF1());

            // órarend.TörölKurzusNap(3);
            // órarend.TörölKurzus(KurzusKategória.Offline);
            // órarend.TörölKurzusKód("AN1_GY_02");
            // órarend.TörölKurzusHossz(120);
            órarend.KiírLista();

            Console.ReadLine();
        }
    }
}
