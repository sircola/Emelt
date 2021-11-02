using System;

namespace SZTF19het
{
    class Ház
    {
        public string szín;
        int hőfok;

        public void Fűtés()
        {
            ++hőfok;
        }

        public Ház(int hőfok)
        {
            this.hőfok = hőfok;
        }

        public Ház()
        {

        }
    }

    class Háromszög
    {
        // adatmezők
        public double a, b, c;

        public Háromszög(): this(10,20,30)  // előbb a másk ctor fut le
        {
            var rng = new Random();
            do
            {
                a = rng.Next(101);
                b = rng.Next(101);
                c = rng.Next(101);
            } while (SzerkeszthetőE());
        }

        public Háromszög( double a, double b, double c )
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }

        private bool SzerkeszthetőE()
        {
            return (a + b > c) && (a + c > b) && (b + c > a);
        }

        public double Kerület()
        {
            return a + b + c;
        }

        public double Terület()
        {
            double s = Kerület()/2;
            return Math.Sqrt(s * (s - a) * (s - b) * (s - c));
        }
    }

    class Hallgató
    {
        public string név, neptunkód;
        int életkor;

        private string RandomNeptun()
        {
            string nk = "";
            var rng = new Random(); 

            for (int i = 0; i < 6; i++)
            {
                if (rng.Next(100) < 30)
                    nk += rng.Next(10);
                else
                    nk += (char)rng.Next('A', 'Z' + 1);
            }

            return nk;
        }

        private string RandomNév()
        {
            var rng = new Random();
            int hossz = rng.Next(21);

            string nk = "" + (char)rng.Next('A', 'Z' + 1);

            for (int i = 1; i < hossz; i++)
            {
                    nk += (char)rng.Next('a', 'z' + 1);
            }

            return nk;
        }

        public Hallgató( string név, string neptunkód, int életkor)
        {
            var rng = new Random();
            this.név = RandomNév();
            this.neptunkód = RandomNeptun();
            this.életkor = rng.Next(101);
        }

        public override string ToString()
        {
            return $"{név} ({neptunkód}): {életkor}";
        }

        public static Hallgató Legidősebb( Hallgató[] hallgatók )
        {
            int max = 0;
            for (int i = 1; i < hallgatók.Length; i++)
                if (hallgatók[i].életkor > hallgatók[max].életkor)
                    max = i;
            return hallgatók[max];
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Ház ház = new Ház(20);
            Ház ház2 = new Ház(10);
            ház.Fűtés();
            ház.Fűtés();

            Ház[] házak = new Ház[5];
            for (int i = 0; i < házak.Length; i++)
            {
                házak[i] = new Ház(1);
                házak[i].Fűtés();
            }

            Háromszög A = new Háromszög(6,6,6);
            Console.WriteLine("A: " + A.a + " B: " + A.b + " C: " + A.c);
            Console.WriteLine("kerület: " + A.Kerület());
            Console.WriteLine("terület: " + A.Terület());

            Háromszög B = new Háromszög();
            Console.WriteLine("A: " + B.a + " B: " + B.b + " C: " + B.c);
            Console.WriteLine("kerület: " + B.Kerület());
            Console.WriteLine("terület: " + B.Terület());

            Hallgató[] hallgatók = new Hallgató[10];
            for (int i = 0; i < hallgatók.Length; i++)
            {
                hallgatók[i] = new Hallgató("Walaki", "NCC1701", 33);
                Console.WriteLine(hallgatók[i].ToString());
            }

            // Console.WriteLine("legnagyobb életkor: " + new Hallgató("","",1).Legidősebb(hallgatók));
            Console.WriteLine("legnagyobb életkor: " + Hallgató.Legidősebb(hallgatók));

            Console.ReadLine();
        }
    }
}
