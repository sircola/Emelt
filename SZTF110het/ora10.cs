using System;

namespace Ora10
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Property example
            Személy1 Béla = new Személy1("Kovács Béla");
            Console.WriteLine(Béla.név);  // név --> public
            Béla.név = "Tóth Éva";

            Személy2 Zeus = new Személy2("Adolph", "Blaine Charles David Earl Frederick Gerald Hubert Irvin John Kenneth Lloyd Martin Nero Oliver Paul Quincy Randolph Sherman Thomas Uncas Victor William Xerxes Yancy Zeus");
            Console.WriteLine(Zeus.név);
            Zeus.név = "Kovács Kristfóf";  // ?!? vezetéknév, keresztnév

            Személy3 Gizi = new Személy3("Szabó Gizike");
            Console.WriteLine(Gizi.Név);
            //Gizi.Név = "Pista";
            #endregion

            #region Property example 2
            Prop prop = new Prop() { A = 20, E = 70/*, F = 20*/};
            prop.A = 10;
            prop.I = 10;
            #endregion

            #region Bankszámla
            Bankszámla[] bankszámlák = new Bankszámla[]
            {
                new Bankszámla(),
                new Bankszámla("Béla"),
                new Bankszámla("Sanyi", 100),
                new Bankszámla("AAAAAAAA-BBBBBBBB", "Gizi"),
                new Bankszámla("XXXXXXXX-XXXXXXXX", "Kumár", 1000)
            };

            for (int i = 0; i < bankszámlák.Length; i++)
                Console.WriteLine(bankszámlák[i]);
            #endregion

            #region Bölényvadászat
            BölényVadászat mező = new BölényVadászat();
            int x = -1, y = -1;
            do
            {
                Console.Clear();
                mező.Mozog(); //Bölény mozgatás
                Console.WriteLine(mező.Térkép()); //Térkép kirajzolása
                Console.WriteLine(mező.Távolság(x, y)); //Bölények kiírása
                Console.WriteLine("X, majd Y?");
                x = int.Parse(Console.ReadLine());
                y = int.Parse(Console.ReadLine());
                mező.Lövés(x, y);
            } while (!mező.JátékVége());

            Console.Clear();
            Console.WriteLine("Győzelem!!! Minden bölény meghalt...\n");
            Console.WriteLine(mező.Térkép()); //Térkép kirajzolása
            Console.ReadLine();
            #endregion
        }
    }

    #region Property example
    class Személy1
    {
        public string név;

        public Személy1(string név)
        {
            this.név = név;
        }
    }
    class Személy2
    {
        string vezetéknév, keresztnév;
        public string név;

        public Személy2(string vezetéknév, string keresztnév)
        {
            this.vezetéknév = vezetéknév;
            this.keresztnév = keresztnév;
            név = vezetéknév + " " + keresztnév;
        }
    }
    class Személy3
    {
        string[] név;

        public string Név
        {
            get
            {
                return string.Join(" ", név);
            }
        }
        public string Vezetéknév
        {
            get
            {
                return név[0];
            }
        }

        public Személy3(string név)
        {
            this.név = név.Split(' ');
        }
    }
    #endregion

    #region Property example 2
    class Prop
    {
        //prop tab tab
        public int A { get; set; }
        public int B { get; }
        //public int C { set; }
        int D { get; set; }
        public int E { private get; set; }
        public int F { get; private set; }
        //public int G { private get; private set; }
        //private int H { public get; set; }

        int i;
        public int I
        {
            get
            {
                //return I;
                return i * 2;
            }
            set
            {
                i = value * 3;
            }
        }
    }
    #endregion

    #region Bankszámla
    // Bankszámlák kezelésére alkossunk egy objektum orientált programot.
    //
    // Minden egyes bankszámla legyen a BankSzamla osztály egy példánya. A BankSzamla osztály az alábbi(privát láthatóságú) adattagokkal rendelkezzen:
    //  - szamlaszam,
    //  - nev(számlatulajdonos neve),
    //  - egyenleg.
    //
    // Valósítsa meg az alábbi konstruktorokat és példányszintű metódusokat! (Minden metódust teszteljen a Main() metódusból!)
    //
    //  a) BankSzamla(string szamlaszam, string nev, double egyenleg)
    //      Három paraméteres konstruktor, amelyen keresztül kezdeti értékek adhatók az adattagoknak.
    //
    //  b) BankSzamla(string nev, double egyenleg)
    //      Kétparaméteres konstruktor, mely kezdeti értéket ad a nev és egyenleg adattagoknak. A számlaszám véletlenszerűen álljon elő.
    //      Formátuma: NNNNNNNN-NNNNNNNN(8-8 karakter), ahol N egy tetszőleges számjegyet jelöl.
    //
    //  c) Bankszamla(string szamlaszam, string nev)
    //      Kétparaméteres konstruktor, mely kezdeti értéket ad a szamlaszam és nev adattagoknak. Az egyenleg értéke legyen alapértelmezetten 0.0.
    //
    //  d) Bankszamla(string nev)
    //      Egyparaméteres konstruktor, mely kezdeti értéket ad a nev adattagnak.
    //      A szamlaszam véletlenszerűen álljon elő, a fentebb megadott mintának megfelelően, az egyenleg pedig 0.0 legyen.
    //
    //  e) Bankszamla()
    //      Paramétermentes konstruktor. A tulajdonos neve „Anonymus”, a számlaszámot és egyenleg értéket pedig az egyparaméteres konstruktornak megfelelően állítja be.
    //
    //  f) bool Fizet(int osszeg)
    //      Az egyenleg értéke csökken a paraméter értékével, ha elég pénz van a számlán.
    //      Ilyenkor a visszatérési érték true, ha viszont nincs elég pénz, akkor nem fizetünk és a visszatérési érték false.
    //
    //  g) void Novel(int osszeg)
    //      A számlára pénz érkezik, így az egyenleg értéke növekszik a paraméter értékével.
    //
    //  h) void Kamatozik(double szazalek)
    //      A számla egyenlege a paraméternek megfelelő százalékkal növekszik (pl. 2,5%).
    //
    //  i) string ToString()
    //      A metódus előállít egy stringet, aminek segítségével a számla aktuális adatai megjeleníthetők.
    class Bankszámla
    {
        static Random rnd = new Random();

        string számlaszám, név;
        double egyenleg;

        public Bankszámla(string számlaszám, string név, double egyenleg)
        {
            this.számlaszám = számlaszám;
            this.név = név;
            this.egyenleg = egyenleg;
        }
        public Bankszámla(string név, double egyenleg) : this(RandomSzámlaszám(), név, egyenleg) { }
        //public Bankszámla(string név, double egyenleg)
        //{
        //    //Bankszámla("", név, egyenleg);
        //    this.név = név;
        //    this.egyenleg = egyenleg;
        //    számlaszám = RandomSzámlaszám();
        //}
        public Bankszámla(string számlaszám, string név) : this(számlaszám, név, 0.0) { }
        public Bankszámla(string név) : this(név, 0.0) { }
        public Bankszámla() : this("Anonymus") { }

        static string RandomSzámlaszám()
        {
            //this.számlaszám = 10;
            return rnd.Next(10) + rnd.Next(1000000, 9999999) + "-" + rnd.Next(10) + rnd.Next(1000000, 9999999);
            string számlaszám = "";
            for (int i = 0; i < 2; i++)
            {
                if (i != 0)
                    számlaszám += "-";
                for (int j = 0; j < 8; j++)
                    számlaszám += rnd.Next(10);
            }
            return számlaszám;
        }

        public bool Fizet(int osszeg)
        {
            if (egyenleg >= osszeg)
            {
                egyenleg -= osszeg;
                return true;
            }
            return false;
        }
        public void Novel(int osszeg)
        {
            egyenleg += osszeg;
        }
        public void Kamatozik(double szazalek)
        {
            egyenleg *= szazalek / 100 + 1;
        }
        public override string ToString()
        {
            return $"Név: {név}, Számlaszám: {számlaszám}, Egyenleg: {egyenleg}";
        }
    }
    #endregion

    #region Bölényvadászat
    // Bölényvadász játékot készítünk. A bölénycsorda 10 bölényből áll, amelyek egy 5x5 ös játéktéren a 0,0 koordinátából indulnak, és az 4,4 be akarnak elérni.
    // Minden körben minden bölény véletlenszerűen lép egyet jobbra(X+1), lefelé(Y+1) vagy jobbra lefelé(X+1, Y+1), a pálya határain belül maradva.
    // A felhasználó minden körben adjon be egy lövést. Ha eltalált egy bölényt, az meghalt. A bölények győznek, ha bármelyik elér a célba, a felhasználó győz, ha minden bölény meghalt.
    //
    // Egy bölényt egy Bölény típusú objektummal reprezentáljon, amelyben legalább a következő tagok legyenek:
    //  - X,Y a bölény aktuális x és y koordinátája.
    //  - Lep() hatására a bölény lépjen egyet véletlenszerűen.
    //  - Egy konstruktor, amely létrehozza a bölényt, és egyszer lépteti, hogy ne a 0,0-n kezdjen.
    //  - Tavolsag() megadja az adott bölénynek a céltól való távolságát
    //
    //  A 10 darab bölényt tömbben helyezze el.
    //  Minden körben számolja meg és írja ki, hogy a játékos lövése hány bölényt talált el (ezeket a tömbből vegye ki), aztán léptesse a megmaradt bölényeket,
    //  majd írja ki a célhoz legközelebb lévő bölénynek a céltól való távolságát
    class BölényVadászat
    {
        int n = 5; //Játéktér mérete (0-4)
        Bölény[] bölények = new Bölény[10]; // nem a pálya van eltárolva, hanem a bölények, amik elhelyezkédést a pályán az X,Y koordinátája mondja meg

        public bool Cheat { get; set; } = false; // ha igaz akkor láthatóak a bölények a pályán

        public BölényVadászat()
        {
            for (int i = 0; i < bölények.Length; i++)
                bölények[i] = new Bölény();
        }

        public string Térkép()
        {
            string s = "  ";

            for (int x = 0; x < n; x++) s += x + " ";
            s += "\n";

            for (int y = 0; y < n; y++) //Sorok
            {
                s += y + " ";
                for (int x = 0; x < n; x++) //Oszlopok
                {
                    char one = '-';
                    for (int i = 0; i < bölények.Length; i++) //Bölények
                    {
                        if (bölények[i].Távolság(x, y) == 0) //HaVanbölényAz (x,y) pozícióban
                        {
                            if (!bölények[i].Él) one = '+';
                            else if (Cheat) one = '*';
                        }
                    }
                    s += one + " ";
                }
                s += "\n";
            }
            return s;
        }
        public void Lövés(int x, int y)
        {
            if (x < 0 || y < 0 || x >= n || y >= n) return; // pályán kívüli lövés

            for (int i = 0; i < bölények.Length; i++)
                if (bölények[i].Távolság(x, y) == 0)
                    bölények[i].Él = false;
        }
        public void Mozog()
        {
            for (int i = 0; i < bölények.Length; i++)
                if (bölények[i].Él) // csak az mozog amelyik él
                    bölények[i].Lép();
        }
        public string Távolság(int x, int y)
        {
            if (x < 0 || y < 0 || x >= n || y >= n) return ""; // pályán kívüli lövés

            string s = "Lövés: " + x + ";" + y + "\n";
            for (int i = 0; i < bölények.Length; i++)
            {
                s += "Bölény #" + i + ": ";
                if (bölények[i].Él)
                    s += bölények[i].Távolság(x, y).ToString("F") + "\n";
                else
                    s += "meghalt...\n";
            }
            return s;
        }
        public bool JátékVége()
        {
            // jelenleg csak a játékos tud nyerni..módosítsa a kódot, hogy a feladat szerint a bölények is nyerhessenek!0
            for (int i = 0; i < bölények.Length; i++)
                if (bölények[i].Él)
                    return false;
            return true;
        }
    }
    class Bölény
    {
        static Random rnd = new Random();

        int x = 0, y = 0; // 0-4

        public bool Él { get; set; } = true;

        public Bölény()
        {
            Lép();
        }

        public void Lép()
        {
            x = rnd.Next(x, x == 4 ? 4 : x + 2);    // ?:   (feltétel) ? true : false
            y = rnd.Next(y, y == 4 ? 4 : y + 2);
        }
        public double Távolság(int lo_x, int lo_y)
        {
            return Math.Sqrt(Math.Pow(x - lo_x, 2) + Math.Pow(y - lo_y, 2));
        }
    }
    #endregion
}
