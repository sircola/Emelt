using System;

namespace SZTF110het
{
    class Hallgató
    {
        string[] vezetéknév, keresztnév;
        int kreditek;

        public string Név {
            get { return string.Join(" ", vezetéknév) + " " + string.Join(" ", keresztnév); }
            // private set { név = value; }
        }

        public Hallgató(string vezetéknév, string keresztnév)
        {
            this.vezetéknév = vezetéknév.Split(' ');
            this.keresztnév = keresztnév.Split(' ');
            // Tantárgyak = new string[2];
        }

        public int Kreditek {
            get { return kreditek; ; }
            set { kreditek = value * 2; }
        }

        public string[] Tantárgyak { get; set; } = new string[2];
    }

    class BankSzamla
    {
        string szamlaszam;
        string nev;
        double egyenleg;
        static Random rng = new Random();

        public BankSzamla(string szamlaszam, string nev, double egyenleg)
        {
            this.szamlaszam = szamlaszam;
            this.nev = nev;
            this.egyenleg = egyenleg;
        }

        static string rngSzamlaszam()
        {
            return "" + rng.Next(10000000, 99999999) + "-" + rng.Next(10000000, 99999999);
        }

        public BankSzamla(string nev, double egyenleg) : this(rngSzamlaszam(), nev, egyenleg)
        {
        }

        public BankSzamla(string szamlaszam, string nev) : this(szamlaszam, nev, 0.0)
        {
        }

        public BankSzamla(string nev, int szazalek) : this(nev, 0.0)
        {
            egyenleg = 1000 * szazalek;
        }

        public BankSzamla() : this("Anonymus", 2)
        {
        }

        public override string ToString()
        {
            return $"Név: " + this.nev + "\nSzámlaszám: " + this.szamlaszam + "\nEgynelg: " + this.egyenleg;
        }

        public bool Fizet(int osszeg)
        {
            if (this.egyenleg < osszeg)
                return false;

            this.egyenleg -= osszeg;

            return true;
        }

        void Novel(int osszeg)
        {
            this.egyenleg += osszeg;
        }

        void Kamatozik(double szazalek)
        {
            this.egyenleg *= (1.0 + szazalek);
        }
    }

    class BölényVadász
    {
        int n = 5;
        Bölény[] bölények = new Bölény[10];


        public BölényVadász()
        {
            for (int i = 0; i < bölények.Length; i++)
                bölények[i] = new Bölény();
        }

        public void Mozog()
        {
            for (int i = 0; i < bölények.Length; i++)
            {
                if (bölények[i].Él)
                    bölények[i].Lep();
            }
        }

        public void Lövés(int x, int y)
        {
            if (x < 0 || y < 0 || x >= n || y >= n)
                return;

            for (int i = 0; i < bölények.Length; i++)
                if (bölények[i].Él && bölények[i].Tavolsag(x, y) == 0)
                    bölények[i].Él = false;
        }

        public override string ToString()
        {
            string s = "";

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    char c = '-';
                    for (int k = 0; k < bölények.Length; k++)
                    {
                        if (bölények[k].Tavolsag(i, j) == 0)
                        {
                            if (bölények[k].Él)
                                c = '*';
                            else
                                c = '+';
                        }
                    }
                    s += c;
                }
                s += '\n';
            }

            return s;
        }
    }

    class Bölény
    {
        int x = 0, y = 0;
        static Random rng = new Random();

        public bool Él { get; set; } = true;

        public void Lep()
        {
            x = rng.Next(x, x == 4 ? 4 : x + 2); // (felétel)?igaz:hamis
            y = rng.Next(y, y == 4 ? 4 : y + 2);
        }

        public double Tavolsag(int i, int j)
        {
            return Math.Sqrt(Math.Pow(x - i, 2) + Math.Pow(y - j, 2));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Hallgató hallgató = new Hallgató("Kiss Horváth", "Béla János");
            Console.WriteLine(hallgató.Név);
            hallgató.Kreditek = 10;

            BankSzamla bankszámla = new BankSzamla();
            Console.WriteLine(bankszámla.ToString());

            BölényVadász játék = new BölényVadász();
            Console.WriteLine(játék);
            játék.Lövés(0, 0);
            játék.Mozog();
            Console.WriteLine(játék);
        }
    }
}
