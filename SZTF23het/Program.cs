using System;


namespace SZTF23het
{
    public delegate void RendelesTeljesitesKezelo(string etelNeve);
    public delegate void HozzavaloSzuksegesKezelo(string hozzavalo);
    public delegate void HozzavaloElkeszultKezelo(string hozzavalo);
    public delegate void HozzavaloKevesKezelo(KorlatosSzakacs sz, string hozzavalo);

    class Etel
    {
        public string megnevezes { get; set; }
        public string[] hozzavalok { get; set; }

        public Etel(string n, string[] h) 
        {
            megnevezes = n;
            hozzavalok = h;
        }
    }

    class Sef
    {
        private Etel cel;
        private int szuksegesHozzavaloSzam;

        Etel[] receptek = new Etel[] 
        {
            new Etel("poharviz", new string[] { "viz" } ),
            new Etel("leves", new string[] { "repa", "hus", "krumpli", "viz" } ),
            new Etel("rantothus", new string[] { "hus", "krumpli" } ),
            new Etel("fozelek", new string[] { "viz", "repa" } )
        };

        public event RendelesTeljesitesKezelo RendelesTeljesitve;
        public event RendelesTeljesitesKezelo RendelesNemTeljesitheto;
        public event HozzavaloSzuksegesKezelo HozzavaloSzukseges;

        public void Megrendeles(string etelNeve)
        {
            int j = -1;
            for (int i = 0; i < receptek.Length; i++)
            {
                if (receptek[i].megnevezes == etelNeve)
                    j = i;
            }

            if (j == -1)
                RendelesNemTeljesitheto?.Invoke(etelNeve);
            else
                Elkeszites(receptek[j]);

        }

        public void Elkeszites(Etel recept)
        {
            cel = recept;
            szuksegesHozzavaloSzam = recept.hozzavalok.Length;

            for (int i = 0; i < szuksegesHozzavaloSzam; i++)
            {
                HozzavaloSzukseges?.Invoke(cel.hozzavalok[i]);
            }
        }

        public void SzakacsElkeszult(string hozzavalo)
        {
            --szuksegesHozzavaloSzam;

            if (szuksegesHozzavaloSzam == 0)
                RendelesTeljesitve?.Invoke(cel.megnevezes);
        }

        public void Felvesz(Szakacs szakacs)
        {
            if( szakacs is KorlatosSzakacs)
            {
                (szakacs as KorlatosSzakacs).HozzavaloNemKeszithetoEl += SzakacsHibatJelez;
            }

            HozzavaloSzukseges += szakacs.SefKerValamit;
            szakacs.HozzavaloElkeszult += SzakacsElkeszult;
        }

        public void SzakacsHibatJelez(string hozzavalo)
        {
            Console.WriteLine($"Séf: Szakács hibát jelzett '{hozzavalo}'");
            RendelesNemTeljesitheto?.Invoke(cel.megnevezes);
        }
    }

    public class Szakacs
    {
        public string nev { get;  }
        public string specialitas { get; set; }

        public Szakacs(string n, string s)
        {
            nev = n;
            specialitas = s;
        }

        public HozzavaloElkeszultKezelo HozzavaloElkeszult;

        public void SefKerValamit(string hozzavalo)
        {
            if (hozzavalo == specialitas)
                Foz();
        }

        public virtual void Foz()
        {
            HozzavaloElkeszult?.Invoke(specialitas);
        }
     }

    public class KorlatosSzakacs : Szakacs
    {
        public int mennyiseg { get; set; }

        public event HozzavaloElkeszultKezelo HozzavaloNemKeszithetoEl;
        public event HozzavaloKevesKezelo HozzavaloKeves;

        public KorlatosSzakacs(string nev, string specialitas, int mennyiseg)
            : base(nev, specialitas)
        {
            this.mennyiseg = mennyiseg;
        }

        public override void Foz()
        {
            if (mennyiseg == 0)
            {
                HozzavaloNemKeszithetoEl?.Invoke(specialitas);
                return;
            }

            --mennyiseg;

            if (mennyiseg < 2)
                HozzavaloKeves?.Invoke(this,specialitas);

            base.Foz();
        }
    }

    class Raktar
    {
        public int mennyiseg { get; set; }
        public string hozzavalo { get; set; }

        public Raktar(string h, int m)
        {
            mennyiseg = m;
            hozzavalo = h;
        }

        public void Kiad(KorlatosSzakacs sz, string s)
        {
            if (mennyiseg == 0)
                return;

            if (hozzavalo != sz.specialitas)
                return;

            ++sz.mennyiseg;

            --mennyiseg;

            Console.WriteLine($"Raktár: '{sz.nev}' segítséget kér - hozzávaló: '{sz.specialitas}'");

        }
    }

    class Program
    {
        static void RendelesTeljesitve(string s)
        {
            Console.WriteLine($"* Sikeres rendelés {s}");
        }

        static void RendelesNemTeljesitheto(string s)
        {
            Console.WriteLine($"* Sikertelen rendelés {s}");
        }

        static void Main(string[] args)
        {
            Sef sef = new Sef();
            sef.RendelesTeljesitve += new RendelesTeljesitesKezelo(RendelesTeljesitve);
            sef.RendelesNemTeljesitheto += RendelesNemTeljesitheto;
            Szakacs szakacs = new Szakacs("Béla", "viz");
            KorlatosSzakacs korlatosszakacs = new KorlatosSzakacs("Róbert", "repa", 2);
            Raktar raktar = new Raktar("repa", 2);
            korlatosszakacs.HozzavaloKeves += raktar.Kiad;
            sef.Felvesz(szakacs);
            sef.Felvesz(korlatosszakacs);
            sef.Megrendeles("poharviz");
            sef.Megrendeles("kecskesajt");
            sef.Megrendeles("fozelek");
            sef.Megrendeles("fozelek");
            sef.Megrendeles("fozelek");
            sef.Megrendeles("fozelek");
            sef.Megrendeles("fozelek");

            //Console.ReadLine();
        }
    }
}
