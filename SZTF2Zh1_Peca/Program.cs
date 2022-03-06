using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SZTF2Zh1_Peca
{

    class KisHal : Hal
    {
        bool aranyHalE;

        public bool AranyHalE { get { return aranyHalE; } }

        public KisHal(double tomeg, bool szalkasE) : base(tomeg, szalkasE) //public mert " kívülről tegye olvashatóvá"
        {
            this.aranyHalE = false; //"Ha a konstruktorban paraméterként nem ad meg ennek értéket, akkor hamis legyen!"
        }

        public KisHal(bool aranyHalE, double tomeg, bool szalkasE) : base(tomeg, szalkasE)
        {
            this.aranyHalE = aranyHalE;
        }
    }

    class NagyHal : Hal
    {
        static Random rand = new Random(); // azért static mert: "Oldja meg példányfüggetlen véletlenszám generátorral!"

        public NagyHal(double tomeg) : base(tomeg, false)
        {

        }

        public override bool Kifog()
        {
            if (rand.Next(0, 100) < 50)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    abstract class Hal : IKifoghato //abstract mert: " közvetlenül nem példányosítható,"
    {
        //4. pont alapján kellenek alaptagok nem csak tulajdonságok! :" Az ős adattagjain túl ismert, hogy"
        public double tomeg; //az ősből csak azt örökli meg a leszármazott ami public-ra van állítva (defaultból private) (protected sem öröklődik!)
        public bool szalkasE; //az ősből csak azt örökli meg a leszármazott ami public-ra van állítva (defaultból private)
        public double Tomeg { get { return tomeg; } }
        public bool SzalkasE { get { return szalkasE; } }

        public Hal(double tomeg, bool szalkasE)
        {
            this.tomeg = tomeg;
            this.szalkasE = szalkasE;
        }

        public virtual bool Kifog() //virtual mert: "– később, származtatott osztályokban azonban ez a viselkedés felüldefiniálható legyen!"
        {
            return true;
        }
    }

    delegate void KifogvaEsemenyKezelo(IKifoghato kifoghatoElem);
    delegate void EluszottEsemenyKezelo(IKifoghato kifoghatoElem);
    class Horgasz
    {
        public event KifogvaEsemenyKezelo Kifogva;
        public event EluszottEsemenyKezelo Eluszott;
        IKifoghato[] zsak;

        public Horgasz(int zsakMerete)
        {
            this.zsak = new IKifoghato[zsakMerete];
        }

        public void Pecaz(IKifoghato kifoghatoElem)
        {
            int idx = 0;
            if (kifoghatoElem.Kifog())
            {
                if (Kifogva != null)
                {
                    Kifogva(kifoghatoElem);
                }

                while (idx < zsak.Length && zsak[idx] != null)
                {
                    idx++;
                }
                //ezen a ponton, ha idx>=zsak.length, akkor nem fér bele a zsakba, ha kisebb, akkor idx index helyre kell betenni
                if (idx >= zsak.Length)
                {
                    throw new ElemNemFerElKivetel("[KIVETEL] Megtelt a zsák!", kifoghatoElem);
                }
                else
                {
                    zsak[idx] = kifoghatoElem;
                }
            }
            else
            {
                if (Eluszott != null)
                {
                    Eluszott(kifoghatoElem);
                }
            }
        }

        private double OsszTomeg()
        {
            double aktualisTomeg = 0;

            for (int i = 0; i < zsak.Length; i++)
            {
                if (zsak[i] != null)
                {
                    aktualisTomeg += zsak[i].Tomeg;
                }
            }

            return aktualisTomeg;
        }

        private bool VanAranyHal()
        {
            for (int i = 0; i < zsak.Length; i++)
            {
                if ((zsak[i] is KisHal) && (zsak[i] as KisHal).AranyHalE)
                {
                    return true;
                }
            }
            return false;
        }

        private bool VanCsizma()
        {
            for (int i = 0; i < zsak.Length; i++)
            {
                if ((zsak[i] is Gumicsizma))
                {
                    return true;
                }
            }
            return false;
        }

        public bool AsszonyHaragszik()
        {

            if (OsszTomeg() == 0)
            {
                return true;
            }
            else if (VanAranyHal())
            {
                return false;
            }
            else if (VanCsizma() && OsszTomeg() < 10)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }

    class ElemNemFerElKivetel : System.Exception
    {
        IKifoghato elem;

        public ElemNemFerElKivetel(string uzenet, IKifoghato elem) : base(uzenet)
        {
            this.elem = elem;
        }
    }

    class Gumicsizma : IKifoghato //ez már példányosítható, nem ugy mint a Hal osztály:"A Gumicsizma példányok tömege..."
    {
        public double Tomeg { get { return 0.5; } }

        public bool Kifog()
        {
            return true;
        }
    }

    interface IKifoghato
    {
        double Tomeg { get; }

        bool Kifog();
    }

    class Program
    {
        static Random rndTeszt = new Random();
        static void Teszt1() //asszony haragszik mert üres a zsák
        {
            Horgasz horgasz = new Horgasz(3);
            Console.WriteLine("Asszony haragszik értéke üres zsáknál: " + horgasz.AsszonyHaragszik());
        }

        static void Teszt2() //asszony nem haragszik mert nincs csizma a zsákban, nincs aranyhal, de van egy kishal. ( A zsákmány súlya itt lényegtelen.)
        {
            Horgasz horgasz = new Horgasz(3);
            horgasz.Pecaz(new KisHal(0.1, false));
            Console.WriteLine("Asszony haragszik értéke nem üres zsáknál: " + horgasz.AsszonyHaragszik());
        }

        static void Teszt3() //nem haragszik, mert van aranyhal a zsákban
        {
            Horgasz horgasz = new Horgasz(3);
            horgasz.Pecaz(new KisHal(true, 0.2, true));
            Console.WriteLine("Asszony haragszik értéke üres zsáknál: " + horgasz.AsszonyHaragszik());
        }

        static void Teszt4() //haragszik, mert van csizma a zsákban és kevesebb mint 10 kg-os a nem aranyhal
        {
            Horgasz horgasz = new Horgasz(3);
            horgasz.Pecaz(new KisHal(false, 0.2, true));
            horgasz.Pecaz(new Gumicsizma());
            Console.WriteLine("Asszony haragszik értéke üres zsáknál: " + horgasz.AsszonyHaragszik());
        }

        static void Teszt5() //nem haragszik, mert bár van csizma a zsákban a két hal osszsulya >10 kg
        {
            Horgasz horgasz = new Horgasz(3);
            horgasz.Pecaz(new KisHal(false, 5, true));
            horgasz.Pecaz(new KisHal(false, 6, true));
            horgasz.Pecaz(new Gumicsizma());
            Console.WriteLine("Asszony haragszik értéke üres zsáknál: " + horgasz.AsszonyHaragszik());
        }

        static void Teszt6() //eseményekre feliratkozik, így kiírja a megfelelő üzenetet annak megfelelően hoyg sikeresen kifogta vagy nem!
                             //(csak a NagyHal lehet olyan amit nem sikerül kifogni 50% valszeggel) --> ha sokszor futtatod néha sikerül kifogni, néha nem
        {
            Horgasz horgasz = new Horgasz(3);
            horgasz.Kifogva += KiirSiker;
            horgasz.Eluszott += KiirKudarc;
            horgasz.Pecaz(new NagyHal(4));
            horgasz.Pecaz(new KisHal(false, 0.8, true));
            horgasz.Pecaz(new Gumicsizma());
        }

        static void Teszt7() //tele a zsák hiba lekezelése, progi továbbfutása, majd asszony mérges állapotának kiíratása
        {
            Horgasz horgasz = new Horgasz(2);
            try
            {
                horgasz.Pecaz(new NagyHal(4));
                horgasz.Pecaz(new KisHal(false, 0.8, true));
                horgasz.Pecaz(new Gumicsizma()); //itt kell hibát dobnia, mert a 3.nak már  nincs hely a zsákban!
            }
            catch (ElemNemFerElKivetel ex)
            {
                Console.WriteLine(ex.Message);
            }


            Console.WriteLine("Asszony haragszik értéke üres zsáknál: " + horgasz.AsszonyHaragszik());
        }
        static void Main(string[] args)
        {
            Teszt7();

            /*
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine(rndTeszt.Next(0, 100));
            }*/
            Console.ReadLine();
        }

        static void KiirSiker(IKifoghato kifoghatoElem)
        {
            Console.WriteLine($"[ESEMENY] a {kifoghatoElem.Tomeg} súlyú {kifoghatoElem.GetType()} kifogása sikeres volt!");
        }

        static void KiirKudarc(IKifoghato kifoghatoElem)
        {
            Console.WriteLine($"[ESEMENY] a {kifoghatoElem.Tomeg} súlyú {kifoghatoElem.GetType()} kifogasa nem sikerült!");
        }
    }
}
