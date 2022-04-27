using System;

namespace _03_Esemenykezeles
{
    class Event_Telefon
    {
        public static void Test()
        {
            Telefon egyik = new Telefon("06205554422");
            Telefon masik = new Telefon("06208765432");

            egyik.BejovoHivas += BejovoHivas; // Mindkét telefonpéldány eseményeire feliratkozom a megfelelő metódusokkal.
            egyik.KimenoHivas += KimenoHivas; // Egy-egy eseményre akár több metódussal is feliratkozhatok a += operátor segítségével.
                                              // Ilyenkor az esemény bekövetkeztekor mindkét metódus lefut.
            masik.BejovoHivas += BejovoHivas;
            masik.KimenoHivas += KimenoHivas;

            egyik.EgyenlegFeltolt(1000); // 500 Ft egy hívás, ezért jelen példánkban két hívásra elegendő összeggel töltjük fel az egyenlegünket.
            masik.EgyenlegFeltolt(1000);

            egyik.HivasKezdemenyezes(masik);  // Az egyik Telefon példánnyal felhívjuk a másik Telefon példányt. Tehát meghívjuk a HivasKezdemenyezes metódusunkat.
            Console.ReadLine();
            masik.HivasKezdemenyezes(egyik);
            Console.ReadLine();

            egyik.HivasKezdemenyezes(masik);
            egyik.HivasKezdemenyezes(masik); // Mivel ez már a harmadik hívás, nincs elég pénz az egyenlegünkön, így a program a "Nincs elég fedezet!" szöveget fogja kiírni.
            Console.ReadLine();
            Console.Clear();
        }

        static void KimenoHivas(Telefon kuldo, string cel_telefonszam)
        {
            Console.WriteLine("Kimenő hívás: " + kuldo.Telefonszam + " szám hívja: " + cel_telefonszam);
        }
        static void BejovoHivas(Telefon kuldo, string forras_telefonszam)
        {
            Console.WriteLine("Bejövő hívás: " + kuldo.Telefonszam + " számot hívja: " + forras_telefonszam);
        }
    }

    delegate void HivasNaplo(Telefon telefon, string telefonszam); // Hozzunk létre egy delegáltat amelynek segítségével végül létrehozhatjuk az eseményeket.
    class Telefon
    {
        public event HivasNaplo BejovoHivas; // Hozzuk létre az eseményeket. Az esemény létrehozása hasonló egy delegált példányosításához,
        public event HivasNaplo KimenoHivas; // azonban közvetlenül módosítani a feliratkozókat nem lehet.

        int egyenleg;

        public string Telefonszam { get; private set; }

        public Telefon(string szam)
        {
            Telefonszam = szam;
        }

        public void EgyenlegFeltolt(int osszeg)
        {
            egyenleg += osszeg;
        }

        public void HivasFogadas(Telefon forras)
        {
            if (BejovoHivas != null)
                BejovoHivas(this, forras.Telefonszam);

            //BejovoHivas?.Invoke(this, forras.Telefonszam);
        }

        public void HivasKezdemenyezes(Telefon cel)
        {
            if (egyenleg < 500)
            {
                Console.WriteLine("Nincs elég fedezet!");
                return;
            }
            if (KimenoHivas != null) // Ha van feliratkozott esemény
            {
                egyenleg -= 500;
                KimenoHivas(this, cel.Telefonszam); // Meghívjuk az eseményre feliratkozott metódusokat a megadott paraméterekkel.
                cel.HivasFogadas(this); // Meghívjuk a cél telefon hívásfogadását
            }
        }
    }

}
