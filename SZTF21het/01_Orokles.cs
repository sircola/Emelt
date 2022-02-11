using System;

namespace Orokles
{
    class Program
    {
        static void Main(string[] args)
        {
            Ember e = new Hallgato("", "", "");
            Hallgato h = new Hallgato("vezetéknév keresztnév", "123456");
            Kutató k = new Kutató("vezetéknév keresztnév");
            Professzor p = new Professzor("vezetéknév keresztnév", 0, 100000);

            //Kutató nemjó = new Hallgato("", ""); // csak alacsonyabb szintűbe lehet tenni

            string s = ((Ember)k).Név; // nincs értelme

            //e.Tanul(); //...nincs ilyen ebben a formában
            ((Hallgato)e).Tanul();
            ((Professzor)e).Tanul(); // kivételt fog dobni --> as/is operátor...!
            h.Tanul();
            k.Tanul();
            p.Tanul();

            if (e is Hallgato)
                (e as Hallgato).Tanul();
			
            Console.ReadLine();
        }
    }

    class Ember // ez legyen abstract
    {
        static Random rnd = new Random();

        string[] név;
        public string Név { get { return string.Join(" ", név); } }
        public ConsoleColor Hajszín { get; } = (ConsoleColor)rnd.Next(1, 16);

        public Ember(string vezetéknév, string keresztnév)
        {
            név = new string[] { vezetéknév, keresztnév };
        }

        //public abstract void Eszik(); // absztrakt kell h legyen az osztály
    }

    class Hallgato : Ember // Ősnek nincs paraméter nélküli konstruktora -> kötelező itt is csinálni egyet
    {
        public string NeptunKód { get; }
        public int KreditekSzáma { get; }

        public Hallgato(string vezetéknév, string keresztnév, string neptunkód) : base(vezetéknév, keresztnév) // (1) kötelező az ős konstruktorát meghívni
        {
            NeptunKód = neptunkód;
            KreditekSzáma = 0;
        }

        public Hallgato(string vezetéknév, string keresztnév, string neptunkód, int kreditszám) : base(vezetéknév, keresztnév) // (2) továbbra is az ős konstruktorát hívjuk meg
        {
            NeptunKód = neptunkód;
            KreditekSzáma = kreditszám;
        }

        public Hallgato(string név, string neptunkód): this(név.Split(' ')[0], név.Split(' ')[1], neptunkód, 0) // (3) nem az ős hanem a saját konstruktorunkat hívjuk meg...
        { }

        public void Tanul()
        {
            Console.WriteLine("Tanul!");
        }
        public virtual void Dolgozik()
        {
            Console.WriteLine("Még nem!");
        }
    }

    class Kutató : Hallgato
    {
        public int PublikációkSzáma { get; protected set; } = 0;

        public Kutató(string név) : base(név, "") { }

        public void Tanul() // lecseréltük a Tanul() metódust, ami véletlenül azonos az ős Tanul() metódusával.
        {
            Console.WriteLine("Még tanul!");
        }

        public override void Dolgozik() // new vagy override
        {
            Console.WriteLine("Dolgozik!");
        }
    }

    class Professzor : Kutató // sealed 
    {
        public double Fizetés { get; private set; }

        public Professzor(string név, int publikációk, double fizetés) : base(név)
        {
            PublikációkSzáma = publikációk; // readonly... legyen protected set
            Fizetés = fizetés;
        }

        public new void Tanul()
        {
            Console.WriteLine("Már nem!");
        }

        public override void Dolgozik() // override megint
        {
            Console.WriteLine("Dolgozik!");
        }
    }

}
