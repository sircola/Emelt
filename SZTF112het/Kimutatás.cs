using System;
using System.IO;

namespace Kimutatás
{
    enum Stilus
    {
        kondi, trx, crossfit, streetworkout, calisthenics
    }

    class Szemely
    {
        public string Nev { get; private set; }
        public int Suly { get; private set; }
        public Stilus Stilus { get; private set; }
        public int Nap { get; set; }
        public DateTime[] Mikor { get; private set; }
        public DateTime[] Meddig { get; private set; }
        public TimeSpan[] Mennyit {
            get {
                TimeSpan[] ts = new TimeSpan[Mikor.Length];
                for (int i = 0; i < Mikor.Length; i++)
                {
                    ts[i] = Meddig[i] - Mikor[i];
                }

                return ts;
            }
        }
        public Szemely(string nev, int suly, Stilus stilus, int nap, DateTime[] mikor, DateTime[] meddig)
        {
            Nev = nev;
            Suly = suly;
            Stilus = stilus;
            Nap = nap;
            Mikor = mikor;
            Meddig = meddig;
        }
    }

    class Kimutatas
    {
        private Szemely[] szemelyek;
        public Kimutatas()
        {
            AdatFeltoltes();
        }
        private void AdatFeltoltes()
        {
            //string path = Directory.GetCurrentDirectory() + "\\bemenet";
            string[] fajlnevek = Directory.GetFiles("bemenet", "*.txt");
            szemelyek = new Szemely[fajlnevek.Length];
            for (int i = 0; i < fajlnevek.Length; i++)
            {
                string nev = fajlnevek[i].Split('\\', '.')[1];
                string[] adatbeolvasas = File.ReadAllLines(fajlnevek[i]);
                DateTime[] mikor = new DateTime[adatbeolvasas.Length - 3];
                DateTime[] meddig = new DateTime[adatbeolvasas.Length - 3];
                for (int j = 0; j < mikor.Length; j++)
                {
                    string[] seged = adatbeolvasas[3 + j].Split('-');
                    string mikorr = seged[0].Replace('.', '/').Replace('_', ' ');
                    string meddigg = seged[1].Replace('.', '/').Replace('_', ' ');
                    mikor[j] = DateTime.Parse(mikorr);
                    meddig[j] = DateTime.Parse(meddigg);
                }
                szemelyek[i] = new Szemely(nev, int.Parse(adatbeolvasas[0]), (Stilus)Enum.Parse(typeof(Stilus), adatbeolvasas[1]),int.Parse(adatbeolvasas[2]), mikor, meddig);
            }
        }

        private bool Elhizva(Szemely sz)
        {
            return sz.Suly >= 80;
        }

        private string Baratok(Szemely sz)
        {
            string nevek = "";
            for (int i = 0; i < szemelyek.Length; i++)
            {
                if (szemelyek[i] != sz && Ugyanakkor_edzenek(sz, szemelyek[i]))
                {
                    nevek += i < szemelyek.Length - 1 ? $"{szemelyek[i].Nev}, " : szemelyek[i].Nev;
                }
            }
            return nevek;
        }

        private bool Ugyanakkor_edzenek(Szemely sz1, Szemely sz2)
        {
            for (int i = 0; i < sz1.Mikor.Length; i++)
            {
                for (int j = 0; j < sz2.Mikor.Length; j++)
                {
                    if (sz1.Mikor[i].Date == sz2.Mikor[j].Date && sz1.Mikor[i].ToShortTimeString() == sz2.Mikor[i].ToShortTimeString())
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool Elvono(Szemely sz)
        {
            return sz.Nap == 7;
        }

        private bool ElvonoraKuldes(Szemely sz)
        {
            if ((sz.Stilus == Stilus.calisthenics || sz.Stilus == Stilus.streetworkout) && Elvono(sz))
            {
                return true;
            }
            return false;
        }

        private TimeSpan AtlagEdzedIdo(Szemely sz)
        {
            TimeSpan osszido = new TimeSpan(0, 0, 0);
            TimeSpan[] edzesidok = sz.Mennyit;
            for (int i = 0; i < edzesidok.Length; i++)
            {
                osszido += edzesidok[i];
            }

            double sec = osszido.TotalSeconds;
            double atlag = sec / edzesidok.Length;
            double ora = atlag / 3600;
            double perc = atlag / 60 - (int)ora * 60;
            double mp = atlag - (int)perc * 60 - (int)ora * 3600;
            TimeSpan s = new TimeSpan((int)ora, (int)perc, (int)mp);
            return s;
        }

        public void StatisztikaKeszites()
        {
            if (!Directory.Exists("stat"))
            {
                Directory.CreateDirectory("stat");
                for (int i = 0; i < szemelyek.Length; i++)
                {
                    string ki = szemelyek[i].Suly + ", " + szemelyek[i].Stilus.ToString();
                    ki += ElvonoraKuldes(szemelyek[i]) ? ", Elvonora Kuldve" : ", nincs Elvonora Kuldve";
                    ki += $", {AtlagEdzedIdo(szemelyek[i]).ToString()}";
                    ki += $"\nBarátok:\n{Baratok(szemelyek[i])}";
                    File.WriteAllText($@"stat\{szemelyek[i].Nev}.txt", ki);
                }
            }
            else
            {
                for (int i = 0; i < szemelyek.Length; i++)
                {
                    string ki = szemelyek[i].Suly + ", " + szemelyek[i].Stilus.ToString();
                    ki += ElvonoraKuldes(szemelyek[i]) ? ", Elvonora Kuldve" : ", nincs Elvonora Kuldve";
                    ki += $", {AtlagEdzedIdo(szemelyek[i]).ToString()}";
                    ki += $"\nBarátok:\n{Baratok(szemelyek[i])}";
                    File.WriteAllText($@"stat\{szemelyek[i].Nev}.txt", ki);
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Kimutatas k = new Kimutatas();
            k.StatisztikaKeszites();
        }
    }

}

