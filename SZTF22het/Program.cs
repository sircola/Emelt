using System;

namespace SZTF22het
{
    interface IJatekos
    {
        void Nyert();
        void Veszitett();
    }

    interface ITippelo : IJatekos
    {
        void JatekIndul(int alsoHatar, int felsoHatar );
        int KovetkezoTipp();
    }

    interface IOkosTippelo : ITippelo
    {
        void Kisebb();
        void Nagyobb();
    }

    class SzamKitalaloJatek : INaploz
    {
        static Random rng = new Random();
        const int MAX_VERSENYZO = 5;
        ITippelo[] versenyzok;
        public int versenyzoN { get; set; }
        public int alsoHatar { get; set; }
        public int felsoHatar { get; set; }
        public int cel { get; set; }

        public SzamKitalaloJatek(int a, int f)
        {
            versenyzok = new ITippelo[MAX_VERSENYZO];
            alsoHatar = a;
            felsoHatar = f;
            versenyzoN = 0;
        }

        public void VersenyzoFelvetele(ITippelo j)
        {
            if (versenyzoN == MAX_VERSENYZO)
                return;
            versenyzok[versenyzoN++] = j;
        }

        public void VersenyIndul()
        {
            cel = rng.Next(alsoHatar, felsoHatar);
            for (int i = 0; i < versenyzoN; i++)
            {
                versenyzok[i].JatekIndul(alsoHatar, felsoHatar);
            }
            naplobaIr("Verseny indul.\ncél: " + cel);
        }

        public bool MindenkiTippel()
        {
            ITippelo[] nyerők = new ITippelo[versenyzoN];
            int nyerődb = 0;
            ITippelo[] bukók = new ITippelo[versenyzoN];
            int bukódb = 0;

            for (int i = 0; i < versenyzoN; i++)
            {
                int tipp = versenyzok[i].KovetkezoTipp();

                if (tipp == cel)
                {
                    nyerők[nyerődb++] = versenyzok[i];
                    versenyzok[i].Nyert();
                    naplobaIr("ez kitatlálta: " + versenyzok[i].ToString());
                }
                else
                {
                    bukók[bukódb++] = versenyzok[i];
                    if( versenyzok[i] is IOkosTippelo)
                    {
                        if (tipp < cel)
                            (versenyzok[i] as IOkosTippelo).Nagyobb();
                        else
                            (versenyzok[i] as IOkosTippelo).Kisebb();
                    }
                }
            }

            if (nyerődb > 0)
            {
                for (int i = 0; i < bukódb; i++)
                {
                    bukók[i].Veszitett();
                }
            }

            return nyerődb>0 ? true: false;
        }

        public virtual void Jatek()
        {
            VersenyIndul();
            while (MindenkiTippel() == false);
        }

        public virtual void Statisztika(int korokSzama)
        {
            for (int i = 0; i < korokSzama; i++)
            {
                Jatek();
            }

            for (int i = 0; i < versenyzoN; i++)
            {
                if(versenyzok[i] is IStatisztikatSzolgaltat)
                {
                    naplobaIr($"{i}. játékos ({versenyzok[i].ToString()}), NY: {(versenyzok[i] as IStatisztikatSzolgaltat).HanyszorNyert}  V: {(versenyzok[i] as IStatisztikatSzolgaltat).HanyszorVesztett}");
                }
            }
        }

        public void naplobaIr(string s)
        {
            Console.WriteLine(s);
        }
    }

    abstract class GepiJatekos : ITippelo, IStatisztikatSzolgaltat
    {
        public int alsoHatar { get; set; }
        public int felsoHatar { get; set; }
        public int nyertDB { get; set; } = 0;
        public int veszitettDB { get; set; } = 0;

        public int HanyszorNyert {
            get { return nyertDB; }
        }

        public int HanyszorVesztett {
            get { return veszitettDB; }
        }

        public virtual void JatekIndul(int alsoHatar, int felsoHatar)
        {
            this.alsoHatar = alsoHatar;
            this.felsoHatar = felsoHatar;
        }

        public abstract int KovetkezoTipp();

        public void Nyert()
        {
            ++nyertDB;
        }

        public void Veszitett()
        {
            ++veszitettDB;
        }
    }

    class VeletlenTippelo : GepiJatekos
    {
        static Random rng = new Random();
        public override int KovetkezoTipp()
        {
            return rng.Next(alsoHatar,felsoHatar+1);
        }
    }

    class BejaroTippelo : GepiJatekos
    {
        public int aktualis { get; set; }
        public override int KovetkezoTipp()
        {
            return aktualis++;
        }

        public override void JatekIndul(int a, int f)
        {
            aktualis = a;
            base.JatekIndul(a, f);
        }
    }

    class LogaritmikusKereso : GepiJatekos, IOkosTippelo
    {
        public int tipp { get; set; }
        public void Kisebb()
        {
            felsoHatar = tipp-1;
        }

        public override int KovetkezoTipp()
        {
            tipp = (felsoHatar + alsoHatar) / 2;
            return tipp;
        }

        public void Nagyobb()
        {
            alsoHatar = tipp+1;
        }
    }

    class EmberiJatekos : IOkosTippelo
    {
        public void JatekIndul(int alsoHatar, int felsoHatar)
        {
            Console.WriteLine($"Játék indul {alsoHatar} és {felsoHatar} között.");
        }

        public void Kisebb()
        {
            Console.WriteLine("Kisebb.");
        }

        public int KovetkezoTipp()
        {
            Console.Write("tipped? ");
            return int.Parse(Console.ReadLine());
        }

        public void Nagyobb()
        {
            Console.WriteLine("Nagyobb.");
        }

        public void Nyert()
        {
            Console.WriteLine("Nyertél.");
        }

        public void Veszitett()
        {
            Console.WriteLine("Nem talált.");
        }
    }

    interface IStatisztikatSzolgaltat
    {
        int HanyszorNyert { get; }
        int HanyszorVesztett { get;  }

    }

    class SzamKitalaloJatekKaszino : SzamKitalaloJatek, IStatisztikatSzolgaltat
    {
        public int kaszinoNyert { get; set; }
        public int kaszinoVesztett { get; set; }
        public int korokSzam { get; set; }

        public int HanyszorNyert {
            get { return kaszinoNyert; }
        }

        public int HanyszorVesztett {
            get { return kaszinoVesztett; }
        }

        public SzamKitalaloJatekKaszino(int a,int f, int k) : base(a,f)
        {
            korokSzam = k;
            kaszinoNyert = 0;
            kaszinoVesztett = 0;
        }

        public override void Jatek()
        {
            VersenyIndul();

            bool vanNyertes = false;
            for (int i = 0; i < korokSzam && vanNyertes == false; i++)
            {
                vanNyertes = MindenkiTippel();
            }
            if (vanNyertes == true)
                ++kaszinoVesztett;
            else
                ++kaszinoNyert;
        }

        public override void Statisztika( int k )
        {
            base.Statisztika(k);

            Console.WriteLine($"Kaszino, NY: {HanyszorNyert} V: {HanyszorVesztett} ");
        }
    }

    interface INaploz
    {
        void naplobaIr(String s);
    }

    class Program
    {
        static void Main(string[] args)
        {
            SzamKitalaloJatekKaszino k = new SzamKitalaloJatekKaszino(1, 100, 6);
            SzamKitalaloJatek sz = new SzamKitalaloJatek(10, 20);
            VeletlenTippelo v = new VeletlenTippelo();
            BejaroTippelo b = new BejaroTippelo();
            LogaritmikusKereso l = new LogaritmikusKereso();
            // EmberiJatekos e = new EmberiJatekos();

            sz.VersenyzoFelvetele(v);
            sz.VersenyzoFelvetele(b);
            sz.VersenyzoFelvetele(l);
            // sz.VersenyzoFelvetele(e);

            k.VersenyzoFelvetele(v);
            k.VersenyzoFelvetele(b);
            k.VersenyzoFelvetele(l);

            // sz.Jatek();
            // sz.Statisztika(1000);
            k.Statisztika(1000);

            // Console.ReadKey();
        }
    }
}
