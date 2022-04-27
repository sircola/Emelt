using System;

namespace Kivetelkezeles
{
    #region Kivételek
    class SorMegteltKivetel : ApplicationException
    {
        public int SorMérete { get; }
        public SorMegteltKivetel(int sorMérete) { SorMérete = sorMérete; }
    }
    class SorUresKivetel : ApplicationException { }
    #endregion

    class Sor
    {
        //  | 1. | 2. | 3. | 4. | .. | <--X
        //  | 2. | 3. | 4. | .. | <--X
        //  | 3. | 4. | .. | <--X
        //  | 4. | .. | <--X
        //  | .. | <--X

        object[] elemek;

        int darab = 0; // aktuális darabszám
        int kovetkezo = 0; // a következő beletevendő elem indexe
        int utolso = 0; // az utolsó kiveendő elem indexe

        public bool Ures { get { return darab == 0; } }
        public bool Tele { get { return darab == elemek.Length; } }

        public Sor(int meret)
        {
            elemek = new object[meret];
        }

        public void Betesz(object elem)
        {
            if (darab < elemek.Length) //még nincsen tele
            {
                elemek[kovetkezo] = elem; //következő - utolsó helyre belerakjuk az elemet
                kovetkezo = (kovetkezo + 1) % elemek.Length; // léptetjük, de ha a tömb végén lennénk az elejére rakjuk
                darab++; // új darab belekerült
            }
            else
                throw new SorMegteltKivetel(elemek.Length);
        }

        public object Kivesz()
        {
            if (darab > 0)
            {
                object vissza = elemek[utolso]; // első helyről kiveszük az elemet
                utolso = (utolso + 1) % elemek.Length; // léptetjük, de ha a tömb végén lennénk az elejére rakjuk
                darab--; // régi darab kikerült belőle
                return vissza;
            }
            else
                throw new SorUresKivetel();
        }
    }

    class x_DemoKivetelkezeles3_sor
    {
        public static void Teszt()
        {
            Console.WriteLine("SOR");
            Sor sor = new Sor(3);
            sor.Betesz(1);
            sor.Betesz(2);
            sor.Betesz(3);

            Console.WriteLine(sor.Kivesz());
            Console.WriteLine(sor.Kivesz());
            Console.WriteLine(sor.Kivesz());

            sor.Betesz(4);
            Console.WriteLine(sor.Kivesz());
            sor.Betesz(5);
            Console.WriteLine(sor.Kivesz());
            sor.Betesz(6);
            Console.WriteLine(sor.Kivesz());

            try
            {
                sor.Kivesz();
            }
            catch (SorMegteltKivetel e)
            {
                Console.WriteLine(e);
                Console.WriteLine(e.SorMérete);
            }
            catch (SorUresKivetel e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
