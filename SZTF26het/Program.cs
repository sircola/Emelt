using System;
using System.IO;

namespace SZTF26het
{
    class NincsMegoldasKivetel : Exception
    {
        public NincsMegoldasKivetel()
            : base("Nincs megoldása a feladatnak.")
        {
        }
    }

    delegate void AllapotFigyelo( int szint, object[] E );

    abstract class Backtrack
    {
        protected int N { get; set; }
        protected int[] M;
        protected object[,] R;

        public abstract bool Ft( int szint, object o );
        public abstract bool Fk( int szint, object o, object[] E );
        public event AllapotFigyelo Probalkozas;

        public object[] Kereses()
        {
            bool van = false;
            object[] E = new object[N];

            Probal( 0, ref van, E);

            if (van == false)
                throw new NincsMegoldasKivetel();

            return E;
        }

        public void Probal( int szint, ref bool van, object[] E  )
        {
            int i = -1;

            while( !van && i < M[szint]-1 )
            {
                ++i;
                if( Ft( szint, R[szint,i] ) )
                {
                    if (Fk(szint, R[szint, i], E))
                    {
                        E[szint] = R[szint, i];
                        Probalkozas?.Invoke(szint, E);
                        if (szint == N - 1)
                        {
                            van = true;
                        }
                        else
                        {
                            Probal(szint + 1, ref van, E);
                        }
                    }
                }
            }
        }
    }

    class Pozicio
    {
        public int Sor { get; }
        public int Oszlop { get;  }
        public bool Fix { get; }
        public object Ertek { get; set; }

        public Pozicio(int sor, int oszlop)
        {
            Sor = sor;
            Oszlop = oszlop;
            Fix = false;
            Ertek = null;
        }

        public Pozicio(int sor, int oszlop, object ertek)
        {
            Sor = sor;
            Oszlop = oszlop;
            Fix = true;
            Ertek = ertek;
        }

        public static bool Kizaroak( Pozicio a, Pozicio b )
        {
            if( a.Sor == b.Sor || a.Oszlop == b.Oszlop || 
                     ( a.Sor / 3 == b.Sor / 3 && a.Oszlop / 3 == b.Oszlop / 3) )
                return true;

            return false;
        }
    }

    class SudokuMegoldo : Backtrack
    {
        public Pozicio[,] tabla = new Pozicio[9, 9];
        public Pozicio[] fixMezok = new Pozicio[0];
        public Pozicio[] uresMezok = new Pozicio[0];

        public override bool Fk(int szint, object o, object[] E)
        {
            for (int i = 0; i < szint; i++)
            {
                if ( o.Equals( E[i] ) )
                    if (Pozicio.Kizaroak(uresMezok[i], uresMezok[szint]))
                        return false;
            }

            return true;
        }

        public override bool Ft(int szint, object o)
        {
            for (int i = 0; i < fixMezok.Length; i++)
            {
                if ( o.Equals( fixMezok[i].Ertek ) )
                    if (Pozicio.Kizaroak(uresMezok[szint], fixMezok[i]))
                        return false;
            }

            return true;
        }

        void Tablebetoltes( string fájlnév )
        {
            StreamReader sr = new StreamReader(fájlnév);

            for (int x = 0; x < 9; x++)
            {
                string s = sr.ReadLine();
                for (int y = 0; y < 9; y++)
                {
                    if (s[y] == '.')
                    {
                        Pozicio[] t = new Pozicio[uresMezok.Length + 1];
                        for (int i = 0; i < uresMezok.Length; i++)
                        {
                            t[i] = uresMezok[i];
                        }
                        tabla[x, y] = new Pozicio(x, y);
                        t[uresMezok.Length] = tabla[x, y];
                        uresMezok = t;
                    }
                    else
                    {
                        Pozicio[] t = new Pozicio[fixMezok.Length + 1];
                        for (int i = 0; i < fixMezok.Length; i++)
                        {
                            t[i] = fixMezok[i];
                        }
                        tabla[x, y] = new Pozicio(x, y, int.Parse(s[y].ToString()));
                        t[fixMezok.Length] = tabla[x, y];
                        fixMezok = t;
                    }
                }
            }
            sr.Close();

            N = uresMezok.Length;
            M = new int[N];

            for (int i = 0; i < N; i++)
            {
                M[i] = 9;
            }

            R = new object[N, 9];

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    R[i, j] = j+1;
                }
            }
        }

        public SudokuMegoldo( string fájlnév )
        {
            Tablebetoltes(fájlnév);
        }

        public void Megoldas()
        {
            try
            {
                object[] E = Kereses();

                for (int i = 0; i < E.Length; i++)
                {
                    int x = uresMezok[i].Sor;
                    int y = uresMezok[i].Oszlop;
                    tabla[x, y] = new Pozicio(x, y, E[i]);
                }
            }
            catch(NincsMegoldasKivetel ex) 
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Megjelenites()
        {
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    bool szines = false;

                    for (int i = 0; i < uresMezok.Length; i++)
                    {
                        if (uresMezok[i].Sor == x && uresMezok[i].Oszlop == y)
                            szines = true;
                    }
                    if( szines == false )
                        Console.ForegroundColor = ConsoleColor.White;
                    else
                        Console.ForegroundColor = ConsoleColor.Green;

                    Console.Write(tabla[x,y].Ertek!=null?tabla[x, y].Ertek:'.');
                }
                Console.WriteLine();
            }
        }
    }

    class Program
    {
        static void ÁllapotFigyelő( int szint, object[] E)
        {
            Console.WriteLine($"Szint {szint}: {E[szint]}");
        }

        static void Main(string[] args)
        {
            SudokuMegoldo sm = new SudokuMegoldo("SudokuTabla.txt");
            sm.Probalkozas += ÁllapotFigyelő;

            sm.Megoldas();

            Console.WriteLine("\nMegoldás:");
            sm.Megjelenites();
        }
    }
}
