using System;
namespace _03_Esemenykezeles
{
    public delegate void SzámVáltozott(int szamErtek); // (1) - képviselő típus létrehozása
    class Szamolo
    {
        public event SzámVáltozott figyelo; // (2) - az osztály közzéteszi az eseményt

        int szam;
        public int Szam
        {
            get { return szam; }
            set
            {
                szam = value;
                if (figyelo != null)
                    figyelo(szam); // (4) Amikor a közzétevő osztályban kiváltódik az esemény, a képviselők segítségével értesíti a feliratkozott osztályokat
            }
        }
    }

    class Event
    {
        static void Figyelo(int szam)
        {
            Console.WriteLine($"Változás: {szam}");
        }

        public static void Test()
        {
            Szamolo teszt = new Szamolo();
            teszt.figyelo += Figyelo; // (3) Az esemény iránt érdeklődő osztályok saját metódusaik átadásával feliratkoznak az eseményre
            teszt.Szam = 10;
            //teszt.figyelo -= Figyelo;
            //teszt.figyelo = null; // nem lehet törölni, csak pontos fv iratkoztatható le róla
            teszt.Szam = 20;
        }
    }
}
