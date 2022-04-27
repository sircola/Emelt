using System;

namespace Kivetelkezeles
{
    #region Saját kivételek
    class VeremKivetel : ApplicationException
    {
        public object BeszúrandóElem { get; }
        public object Verem { get; }

        public VeremKivetel(Verem verem, object beszúrandóElem)
        {
            Verem = verem;
            BeszúrandóElem = beszúrandóElem;
        }
    }
    class VeremMegteltKivetel : VeremKivetel
    {
        public VeremMegteltKivetel(Verem verem, object beszúrandóElem) : base(verem, beszúrandóElem) { }
    }
    class VeremUresKivetel : VeremKivetel
    {
        public VeremUresKivetel(Verem verem) : base(verem, null) { }
    }
    #endregion

    class Verem
    {
        //  | 1. | 2. | 3. | 4. | .. | <--X
        //  | 1. | 2. | 3. |    | .. | <--X
        //  | 1. | 2. |    |    | .. | <--X
        //  | 1. |    |    |    | .. | <--X
        //  |    |    |    |    | .. | <--X

        object[] elemek;

        int darab = 0;
        int kovetkezo = 0;
        int utolso = 0;

        public bool Ures { get { return darab == 0; } }
        public bool Tele { get { return darab == elemek.Length; } }

        public Verem(int meret)
        {
            elemek = new object[meret];
        }

        public void Betesz(object elem)
        {
            if (darab < elemek.Length)
            {
                elemek[kovetkezo] = elem;
                kovetkezo = kovetkezo + 1;
                utolso = kovetkezo - 1;
                darab++;
            }
            else
                throw new VeremMegteltKivetel(this, elemek.Length);
        }

        public object Kivesz()
        {
            if (darab > 0)
            {
                object vissza = elemek[utolso];
                kovetkezo = kovetkezo - 1;
                utolso = utolso - 1;
                darab--;
                return vissza;
            }
            else
                throw new VeremUresKivetel(this);
        }
    }


    class x_DemoKivetelkezeles2_verem
    {
        public static void Teszt()
        {
            Console.WriteLine("VEREM");
            Verem verem = new Verem(3);
            verem.Betesz(1);
            verem.Betesz(2);
            verem.Betesz(3);

            Console.WriteLine(verem.Kivesz());
            Console.WriteLine(verem.Kivesz());
            Console.WriteLine(verem.Kivesz());

            verem.Betesz(4);
            Console.WriteLine(verem.Kivesz());
            verem.Betesz(5);
            Console.WriteLine(verem.Kivesz());
            verem.Betesz(6);
            Console.WriteLine(verem.Kivesz());

            try
            {
                verem.Kivesz();
            }
            catch (VeremKivetel e)
            {
                Console.WriteLine(e);
                Console.WriteLine(e.Verem);
                Console.WriteLine(e.BeszúrandóElem);
            }
        }
    }
}
