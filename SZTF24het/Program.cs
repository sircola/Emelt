using System;


namespace SZTF24het
{
    public enum UrhajoKategoria
    {
        Yacht,
        Korvett,
        Fregatt,
        Rombolo,
        Teher,
        Allomas
    }

    interface IKomponens
    {
        int Teljesitmeny { get; set; }
        bool Allapot { get; set; }
        void Aktival();
        void Deaktival();
    }

    class Urhajo
    {
        public string nev { get; }
        public int uresTomeg { get; }
        public int aktualisTeljesitmeny { get; set;  }
        public UrhajoKategoria kategoria { get; }

        public IKomponens[] komponensek { get; set; }

        public Urhajo(string n, int u, UrhajoKategoria k)
        {
            if (u <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(uresTomeg), "[KIVETEL] Az üres tömeg nem lehet negatív!");
            }

            uresTomeg = u;

            if (n == null)
            {
                throw new ArgumentNullException(nameof(nev), "[KIVETEL] Value cannot be null.");
            }

            nev = n;

            switch (k)
            {
                case UrhajoKategoria.Yacht:
                    komponensek = new IKomponens[2];
                    break;
                case UrhajoKategoria.Korvett:
                    komponensek = new IKomponens[4];
                    break;
                case UrhajoKategoria.Fregatt:
                    komponensek = new IKomponens[6];
                    break;
                case UrhajoKategoria.Rombolo:
                    komponensek = new IKomponens[8];
                    break;
                case UrhajoKategoria.Teher:
                    komponensek = new IKomponens[8];
                    break;
                case UrhajoKategoria.Allomas:
                    komponensek = new IKomponens[20];
                    break;
            }

            Console.WriteLine($"{nev} letrehozva!");
        }

        public void KomponensFelszerel(IKomponens k)
        {
            int i = 0;
            while (i < komponensek.Length)
            {
                ++i;
            }

            if (i >= komponensek.Length)
            {
                throw new KomponensNemFerElKivetel("[KIVETEL] A komponens nem fér el!", k);
            }
            
            komponensek[i] = k;

            Console.WriteLine($"[Hozzaadas] {komponensek[i].GetType().Name} hozzaadva a(z) {nev} hajohoz");
        }

        public void KomponensLeszerel(int i)
        {

            if (komponensek[i] == null)
            {
                throw new KomponensNemTalalhatoKivetel("[KIVETEL] A törölni kívánt komponens nem található!");
            }
 
            komponensek[i] = null;
 
            Console.WriteLine($"[Leszereles] A(z) {i} indexu komponens leszerelve a(z) {nev} hajorol");
        }

        public void Padlogaz()
        {
            int aktualisTeljesitmenyEredeti = -aktualisTeljesitmeny;

            int i = 0;
            while (i < komponensek.Length && aktualisTeljesitmenyEredeti > 0)
            {
                if (komponensek[i] is Hajtomu && !komponensek[i].Allapot)
                {
                    (komponensek[i] as Hajtomu).Aktival();
                    aktualisTeljesitmeny += komponensek[i].Teljesitmeny;
                }
                ++i;
            }

            int hianyzoEnergia = aktualisTeljesitmeny;
            if (aktualisTeljesitmeny > 0)
            {
                for (int j = 0; j < i; j++)
                {
                    if (komponensek[j] is Hajtomu)
                    {
                        komponensek[j].Deaktival();
                    }
                }
                aktualisTeljesitmeny = aktualisTeljesitmenyEredeti;
                throw new NincsElegEnergiaKivetel(hianyzoEnergia);
            }
        }

        public void Beindit()
        {
            for (int i = 0; i < komponensek.Length; i++)
            {
                if (komponensek[i] is Reaktor)
                {
                    try
                    {
                        komponensek[i].Aktival();
                        aktualisTeljesitmeny += komponensek[i].Teljesitmeny;
                        Console.WriteLine($"[Beinditas] A(z) {nev} urhajo beinditva");
                    }
                    catch (InvalidOperationException e)
                    {

                        Console.WriteLine($"[BELSO KIVETEL] {e.Message}");
                    }
                    catch (NotSupportedException e)
                    {
                        KomponensLeszerel(i);
                    }

                }
            }

        }

        public void Leallit()
        {
            for (int i = 0; i < komponensek.Length; i++)
            {
                try
                {
                    komponensek[i].Deaktival();
                }
                catch (Exception e)
                {
                    throw new NemDeaktivalhatoKivetel("[KIVETEL] Egy komponens nem deaktiválható!", e);
                }
            }
        }
    }

    class Hajtomu : IKomponens
    {
        public int toloero { get; set; }

        public Hajtomu(int t)
        {
            toloero = t;
        }

        public int Teljesitmeny { get; set; }
        public bool Allapot { get; set; }

        public void Aktival()
        {
            Teljesitmeny = toloero;
            Allapot = true;
        }

        public void Deaktival()
        {
            Teljesitmeny = 0;
            Allapot = false;
        }
    }
    class Reaktor : IKomponens
    {
        public int teljesitmeny { get; set; }

        public Reaktor(int t)
        {
            teljesitmeny = t;
        }

        public int Teljesitmeny { get; set; }
        public bool Allapot { get; set; }

        public void Aktival()
        {
            if (Allapot == true)
            {
                throw new InvalidOperationException();
            }

            if (teljesitmeny == 0)
            {
                throw new NotSupportedException();
            }

            Teljesitmeny = -teljesitmeny;
            Allapot = true;
        }

        public void Deaktival()
        {
            if (Allapot == false)
            {
                throw new InvalidOperationException();
            }

            Teljesitmeny = 0;
            Allapot = false;
        }
    }

    class KomponensNemTalalhatoKivetel : System.Exception
    {
        public KomponensNemTalalhatoKivetel() 
        { 
        }

        public KomponensNemTalalhatoKivetel(string s) 
            : base(s) 
        { 
        }
    }

    class NemDeaktivalhatoKivetel : System.Exception
    {
        public NemDeaktivalhatoKivetel(string s, Exception e) 
            : base(s, e)
        {
        }
    }

    class KomponensNemFerElKivetel : System.Exception
    {
        public IKomponens komponens { get; }

        public KomponensNemFerElKivetel(string s, IKomponens k) 
            : base(s)
        {
            komponens = k;
        }
    }

    class NincsElegEnergiaKivetel : System.Exception
    {
        public int hianyMerteke { get; }

        public NincsElegEnergiaKivetel(int h) 
            : base($"Nincs elég teljesítmény, {h} MW hiányzik")
        {
            hianyMerteke = h;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Urhajo startDest = new Urhajo("Star Destroyer #5530", 10, UrhajoKategoria.Rombolo);
            Urhajo serenity = new Urhajo("Serenity", 5, UrhajoKategoria.Yacht);
            Urhajo oldBessie = new Urhajo("Old Bessie", 5, UrhajoKategoria.Teher);
            Urhajo rarzorback = new Urhajo("Razorback", 0, UrhajoKategoria.Rombolo);

            Urhajo kicsi;
            try
            {
                kicsi = new Urhajo("negatív tömeg", -1, UrhajoKategoria.Yacht);

            }
            catch (ArgumentOutOfRangeException e)
            {

                Console.WriteLine(e.Message);
            }

            Urhajo névtelen;
            try
            {
                névtelen = new Urhajo(null, 5, UrhajoKategoria.Yacht);
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e.Message);
            }

            serenity.KomponensFelszerel(new Hajtomu(1270));
            serenity.KomponensFelszerel(new Hajtomu(10));

            oldBessie.KomponensFelszerel(new Hajtomu(120));

            rarzorback.KomponensFelszerel(new Hajtomu(23));
            rarzorback.KomponensFelszerel(new Reaktor(23));

            startDest.KomponensFelszerel(new Hajtomu(3));
            startDest.KomponensFelszerel(new Hajtomu(3));
            startDest.KomponensFelszerel(new Hajtomu(3));
            startDest.KomponensFelszerel(new Hajtomu(3));
            startDest.KomponensFelszerel(new Hajtomu(3));
            startDest.KomponensFelszerel(new Hajtomu(3));
            startDest.KomponensFelszerel(new Hajtomu(3));
            startDest.KomponensFelszerel(new Reaktor(-300));

            try
            {
                startDest.KomponensFelszerel(new Reaktor(3));
            }
            catch (KomponensNemFerElKivetel e)
            {
                Console.WriteLine(e.Message);
            }

            startDest.KomponensLeszerel(0);

            try
            {
                startDest.KomponensLeszerel(0);
            }
            catch (KomponensNemTalalhatoKivetel e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                serenity.Padlogaz();
            }
            catch (NincsElegEnergiaKivetel e)
            {
                Console.WriteLine(e.Message);
            }

            startDest.Beindit();
            startDest.Beindit();

            startDest.Padlogaz();

            startDest.Leallit();

            try
            {
                rarzorback.Leallit();
            }
            catch (NemDeaktivalhatoKivetel e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("[BELSO KIVETEL]: " + e.InnerException.Message);
            }

        }
    }
}
