using System;

namespace SZTF1KisZH4
{
    class Alkalmazott
    {
        public int ID { get; set; }
        public string Név { get; set; }
    }

    class Asztal
    {
        public bool Foglalt { get; private set; } = false;
        public int X { get; }
        public int Y { get; }
        public int Szint { get; }
        public Alkalmazott Alkalmazott { get; private set; }

        public Asztal(int X, int Y, int Szint)
        {
            this.X = X;
            this.Y = Y;
            this.Szint = Szint;
        }

        public void Lefoglal(Alkalmazott Alkalmazott)
        {
            Foglalt = true;
        }

        public override string ToString()
        {
            return Foglalt ? Convert.ToString(Alkalmazott.ID) : "-";
        }
    }

    class Iroda
    {
        public Asztal[] Asztal { get; }

        public Iroda()
        {
            Berendez();
        }

        private void Berendez()
        {
            Asztal = new Asztal[25];

            for (int i = 25; i < Asztal.Length; i++)
            {
                Asztal a = new Asztal()
            }
        }

        public bool AlkalazottFelvesz(Alkalmazott a)
        {
            return false;
        }

        public override string ToString()
        {
            string s = "";

            return s;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Alkalmazott[] a = new Alkalmazott[26];
            Iroda iroda = new Iroda();

            Console.WriteLine(iroda.ToString());
        }
    }
}
