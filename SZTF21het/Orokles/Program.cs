using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orokles
{
    class Jarmu
    {
        public char Azonosito { get; }

        public float X { get; set; }
        public float Y { get; set; }

        public Terkep Terkep { get; set; }

        public Jarmu(char azonosito, float x, float y, Terkep terkep)
        {
            this.Azonosito = azonosito;
            this.X = x;
            this.Y = y;
            this.Terkep = terkep;
        }

        public virtual bool IdeLephet(float x, float y)
        {
            return this.Terkep.TerkepenBeluliPozicio(x, x);
        }
    }

    class TerkepEsJarmuRajzolo : TerkepRajzolo
    {
        public Jarmu[] jarmuvek;
        public int jarmuvekN { get; set; }

        public void JarmuFelvetel(Jarmu jarmu)
        {
            if (jarmuvekN + 1 > jarmuvek.Length)
                return;

            jarmuvek[jarmuvekN++] = jarmu;
        }

        public TerkepEsJarmuRajzolo(Terkep terkep, int max)
            : base(terkep)
        {
            this.jarmuvekN = 0;
            this.jarmuvek = new Jarmu[max];
        }

        protected override char MiVanItt(int x, int y)
        {
            for (int i = 0; i < jarmuvekN; i++)
            {
                if (jarmuvek[i].X == x && jarmuvek[i].Y == y)
                    return jarmuvek[i].Azonosito;
            }

            return base.MiVanItt(x, y);
        }
    }

    abstract class MozgoJarmu : Jarmu 
    {
        public float iranyX { get; set; }
        public float iranyY { get; set; }

        public void UjIranyVektor( float x, float y )
        {
            this.iranyX = x;
            this.iranyY = y;
        }

        public MozgoJarmu(char azonosito, float x, float y, Terkep terkep) 
            : base(azonosito, x, y, terkep)
        {
        }

        public abstract void Mozog();
    }

    class Helikopter : MozgoJarmu
    {
        public float sebeség { get; set; } = 1;

        public void Gyorsit()
        {
            this.sebeség += 0.1f;
        }

        public void Lassit()
        {
            this.sebeség -= 0.1f;
        }

        public override void Mozog()
        {
            float ujX = X + iranyX * sebeség;
            float ujY = Y + iranyY * sebeség;

            if( base.IdeLephet(ujX,ujY))
            {
                X = ujX;
                Y = ujY;
            }
        }

        public Helikopter(float x, float y, Terkep terkep) 
            : base('H', x, y, terkep)
        {
        }
}

    class Auto : MozgoJarmu
    {
        public float sebeség { get; set; } = 1;

        public void Gyorsit()
        {
            this.sebeség += 0.1f;
        }

        public void Lassit()
        {
            this.sebeség -= 0.1f;
        }

        public override bool IdeLephet(float x, float y)
        {
            if (Terkep.TerkepenBeluliPozicio(x, x) == false)
                return false;

            if (Terkep.Magassag(x, y) > 0 && Terkep.Magassag(x,y)<1)
                return true;

            return false;
        }

        public override void Mozog()
        {
            float ujX = X + iranyX * sebeség;
            float ujY = Y + iranyY * sebeség;

            if (base.IdeLephet(ujX, ujY) == false)
                return;

            float d = Terkep.Magassag(X, Y) - Terkep.Magassag(ujX, ujY);

            if (d < 0)
                Lassit();
            if (d > 0)
                Gyorsit();

            X = ujX;
            Y = ujY;
        }

        public Auto(float x, float y, Terkep terkep)
            : base('A', x, y, terkep)
        {
        }
    }

    sealed class Tank : Auto
    {
        public int uzemanyag { get; set; }

        public Tank(int uzemanyag,float x, float y, Terkep terkep)
            : base(x,y,terkep)
        {
            this.uzemanyag = uzemanyag;
        }

        public override bool IdeLephet(float x, float y)
        {
            return this.Terkep.TerkepenBeluliPozicio(x, x);
        }

        public override void Mozog()
        {
            float ujX = X + iranyX * sebeség;
            float ujY = Y + iranyY * sebeség;

            if (base.IdeLephet(ujX, ujY) == false)
                return;

            if (uzemanyag < 10)
                return;
            
            uzemanyag -= 10;

            X = ujX;
            Y = ujY;
        }
    }


    class Szimulacio : TerkepEsJarmuRajzolo
    {
        public void EgyIdoEgysegEltelt()
        {
            for (int i = 0; i < jarmuvekN; i++)
            {
                if (jarmuvek[i] is MozgoJarmu)
                    (jarmuvek[i] as MozgoJarmu).Mozog();
            }
        }

        public void Fut()
        {
            while (true)
            {
                EgyIdoEgysegEltelt();
                Kirajzol();
                System.Threading.Thread.Sleep(500);
            }
        }

        public Szimulacio(Terkep terkep, int max) 
            : base(terkep,max)
        {
        }
    }


    class Program
    {
        static void Teszt1()
        {
            Terkep terkep = new Terkep(80, 25);
            TerkepRajzolo rajzolo = new TerkepRajzolo(terkep);
            rajzolo.Kirajzol();
            Random rng = new Random();
            TerkepEsJarmuRajzolo t = new TerkepEsJarmuRajzolo(terkep,10);
            for (int i = 0; i < 10; i++)
            {
                Jarmu j = new Jarmu((char)('A'+i), rng.Next(terkep.MeretX), rng.Next(terkep.MeretY), terkep);
                t.JarmuFelvetel(j);
            }
            t.Kirajzol();

            Szimulacio s = new Szimulacio(terkep, 10);
            for ( int i = 0; i < 3; i++)
            {
                Helikopter h = new Helikopter( rng.Next(terkep.MeretX), rng.Next(terkep.MeretY), terkep);
                double a = rng.Next(terkep.MeretX);
                double b = rng.Next(terkep.MeretY);
                double d = Math.Sqrt(Math.Pow(a - h.X, 2) + Math.Pow(b - h.Y, 2));
                h.UjIranyVektor((float)((a-h.X) / d), (float)((b-h.Y) / d));
                s.JarmuFelvetel(h);
            }
            for (int i = 0; i < 3; i++)
            {
                Auto o = new Auto(rng.Next(terkep.MeretX), rng.Next(terkep.MeretY), terkep);
                double a = rng.Next(terkep.MeretX);
                double b = rng.Next(terkep.MeretY);
                double d = Math.Sqrt(Math.Pow(a - o.X, 2) + Math.Pow(b - o.Y, 2));
                o.UjIranyVektor((float)((a - o.X) / d), (float)((b - o.Y) / d));
                s.JarmuFelvetel(o);
            }
            for (int i = 0; i < 3; i++)
            {
                Tank tnk = new Tank(rng.Next(100),rng.Next(terkep.MeretX), rng.Next(terkep.MeretY), terkep);
                double a = rng.Next(terkep.MeretX);
                double b = rng.Next(terkep.MeretY);
                double d = Math.Sqrt(Math.Pow(a - tnk.X, 2) + Math.Pow(b - tnk.Y, 2));
                tnk.UjIranyVektor((float)((a - tnk.X) / d), (float)((b - tnk.Y) / d));
                s.JarmuFelvetel(tnk);
            }
            s.Fut();


            Console.ReadLine();
        }

        static void Main(string[] args)
        {
            Teszt1();
        }
    }
}
