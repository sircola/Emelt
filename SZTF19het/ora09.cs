using System;

namespace Ora9
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 1. feladat - Háromszög
            Háromszög háromszög = new Háromszög();
            Háromszög háromszög2 = new Háromszög(10, 3, 8);

            Console.WriteLine(háromszög.Kerület());
            Console.WriteLine(háromszög.Terület());
            Console.WriteLine(háromszög2.Kerület());
            Console.WriteLine(háromszög2.Terület());

            Console.ReadLine();
            #endregion

            #region 2. feladat - négyzet
            Négyzet[] négyzetek = new Négyzet[10]; // {new Négyzet(), new Négyzet(), ...}
            for (int i = 0; i < négyzetek.Length; i++)
                négyzetek[i] = new Négyzet();

            for (int i = 0; i < négyzetek.Length; i++)
                négyzetek[i].Rajzol();

            Console.ReadLine();
            #endregion

            #region 3. feladat - Hallgatók
            Hallgató[] hallgatók = new Hallgató[]
            {
                new Hallgató("Béla", "000001", 22),
                new Hallgató("Gizi", "000002", 32),
                new Hallgató("Feri", "000003", 24),
                new Hallgató("Éva", "000004", 29),
            };

            for (int i = 0; i < hallgatók.Length; i++)
                Console.WriteLine(hallgatók[i]);

            Console.WriteLine(hallgatók[0].Legidősebb(hallgatók));
            Hallgató h = new Hallgató("", "", 9999999);
            Console.WriteLine(h.Legidősebb(hallgatók));
            Console.WriteLine(new Hallgató("", "", 0).Legidősebb(hallgatók));
            
            Console.ReadLine();
            #endregion

            #region 4. feladat - Snake
            new StarLikeSnake('?', 10).Game();
            #endregion

            Console.ReadLine();
        }
    }

    #region 1. feladat - Háromszög
    // Készítsen egy háromszög osztáy, amely egy háromszöghöz tárolja a három oldal hosszúságát, és legyen képes a kerület és terület számítására.
    // Kétféleképpen lehessen létrehozni egy háromszöget: a három oldal megadásával, illetve véletlenszerűen generálva a három oldalt, ezesetben
    // viszont a háromszögnek szerkeszthetőnek kell lennie!
    class Háromszög
    {
        double a, b, c;

        public Háromszög()
        {
            Random rnd = new Random();
            do
            {
                a = rnd.Next(101);
                b = rnd.Next(101);
                c = rnd.Next(101);
            } while (!SzerkeszthetőE());
        }
        public Háromszög(double a, double bOldal, double c)
        {
            this.a = a;
            b = bOldal;
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
            double s = Kerület() / 2;
            return Math.Sqrt(s * (s - a) * (s - b) * (s - c));
        }
    }
    #endregion

    #region 2. feladat - négyzet
    // 1. A Négyzet rendelkezzen az alábbi adattagokkal: x, y, szelesseg, magassag
    // 2. Mind a négy adattag véletlenszámként jöjjön létre a konstruktorban!
    // 3. Legyen a Négyzetnek egy Rajzol() metódusa, amely képes a konzol megfelelő pozíciójában a megadott szélességgel és magassággal
    //      kirajzolni # karakterekből a négyzetet!
    // 4. Hozzon létre a Main-ben egy 10 elemű Négyzet tömböt!
    // 5. Példányosítson a megfelelő tömbindexekre 1-1 Négyzetet és hívja meg a Rajzol() metódust minden példányon!
    class Négyzet
    {
        static Random rnd = new Random();
        int x, y, oldalhossz;

        public Négyzet()
        {
            x = rnd.Next(35);
            y = rnd.Next(35);
            oldalhossz = rnd.Next(5, 15);
        }
        
        public void Rajzol()
        {
            Console.ForegroundColor = (ConsoleColor)rnd.Next(16);
            for (int i = 0; i < oldalhossz; i++)
            {
                Console.SetCursorPosition(x, y + i);
                for (int j = 0; j < oldalhossz; j++)
                {
                    if (i == 0 || i == oldalhossz - 1)
                        Console.Write("#");
                    else if (j == 0 || j == oldalhossz - 1)
                    {
                        Console.SetCursorPosition(x + j, y + i);
                        Console.Write("#");
                    }
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
    #endregion

    #region 3. feladat - Hallgatók
    // Hozzon létre egy hallgató osztályt, adattagjai: név, neptunkód, életkor, amiket a konstruktorban kötelezően meg kell adni.
    // Valósítsa meg az osztály tartalmának formázott kiíratását!
    // Legyen lehetőség a legidősebb hallgató lekérdezésére!
    class Hallgató
    {
        string név, neptunkód;
        int életkor;

        public Hallgató(string név, string neptunkód, int életkor)
        {
            this.név = név;
            this.neptunkód = neptunkód;
            this.életkor = életkor;
        }

        public string Legidősebb(Hallgató[] hallgatók) // elég szerencsétlen megvalósítás ne csináljunk ilyet, de működik...
        {
            int max = 0;
            for (int i = 0; i < hallgatók.Length; i++)
                if (hallgatók[i].életkor > hallgatók[max].életkor)
                    max = i;
            return hallgatók[max].ToString();
        }

        public override string ToString()
        {
            return $"Név: {név}, Neptunkód: {neptunkód}, Életkor: {életkor}";
        }
    }
    #endregion

    #region 4. feladat - Snake
    class StarLikeSnake
    {
        int delayMS;
        char ch;
        bool alive = true;
        ConsoleColor originalColor = Console.ForegroundColor;
        int maxWidth = 76, maxHeight = 26;
        int x = 38, y = 13; // start position
        int dx = 1, dy = 0; // direction

        public StarLikeSnake()
        {
            ch = '*';
            delayMS = 50;
        }
        public StarLikeSnake(int delayMS)
        {
            ch = '*';
            this.delayMS = delayMS;
        }
        public StarLikeSnake(char ch, int delayMS)
        {
            this.ch = ch;
            this.delayMS = delayMS;
        }

        public void Game()
        {
            Console.Clear();
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Magenta;
            DrawBorder();

            ConsoleKeyInfo consoleKey;
            do // until escape
            {
                Console.SetCursorPosition(x, y);


                if (Console.KeyAvailable) // see if a key has been pressed
                {

                    consoleKey = Console.ReadKey(true); // get key and use it to set options
                    if (consoleKey.Key == ConsoleKey.C)
                    {
                        Console.Clear();
                        DrawBorder();
                    }
                    else if (consoleKey.Key == ConsoleKey.UpArrow) //UP
                    {
                        dx = 0;
                        dy = -1;
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else if (consoleKey.Key == ConsoleKey.DownArrow) // DOWN
                    {
                        dx = 0;
                        dy = 1;
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }
                    else if (consoleKey.Key == ConsoleKey.LeftArrow) //LEFT
                    {
                        dx = -1;
                        dy = 0;
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else if (consoleKey.Key == ConsoleKey.RightArrow) //RIGHT
                    {
                        dx = 1;
                        dy = 0;
                        Console.ForegroundColor = ConsoleColor.Magenta;
                    }
                    else if (consoleKey.Key == ConsoleKey.Escape) //END
                    {
                        alive = false;
                    }
                }

                // calculate the new position and write the character in the new position
                x += dx;
                if (x > maxWidth - 1)
                    x = 1;
                if (x < 1)
                    x = maxWidth - 1;

                y += dy;
                if (y > maxHeight - 1)
                    y = 1;
                if (y < 1)
                    y = maxHeight - 1;

                Console.SetCursorPosition(x, y);
                Console.Write(ch);

                System.Threading.Thread.Sleep(delayMS);  // pause
            } while (alive);
            Console.CursorVisible = true;
        }
        private void DrawBorder()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            for (int i = 0; i <= maxHeight; i++)
            {
                for (int j = 0; j <= maxWidth; j++)
                {
                    if (i == 0 || i == maxHeight)
                    {
                        Console.Write("*");
                    }
                    else if (j == 0 || j == maxWidth)
                    {
                        Console.SetCursorPosition(j, i);
                        Console.Write("*");
                    }
                }
                Console.WriteLine();
            }
            DrawTitle();
        }
        private void DrawTitle()
        {
            Console.SetCursorPosition(8, 0);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Arrows move up/down/right/left. 'c' clear screen 'esc' quit.");
            Console.ForegroundColor = originalColor;
        }
    }
    #endregion

    #region További feladat
    // 1. Készítsen Kör osztályt, amely egy kört a középpontja koordinátáival és a sugárral reprezentál.
    //      Egészítse ki a Kör osztályt egy metódussal, amely megállapítja, hogy egy adott pont a kör középpontjától pontosan mekkora távolságra van.
    // 2. Készítsen a Körhöz egy új konstruktort, amely megadott sugárral, de véletlenszerű középponttal hozza létre a példányt.
    // 3. Az osztályban legyen metódus, ami megállapítja, hogy egy adott pont benne van-e a körben vagy sem.
    // 4. Készítsen példányt a tesztelésre: kérje be a felhasználótól egy kör adatait, majd kérjen be pontokat, és mondja meg, ezek benne vannak-e a körben vagy sem.
    // 5. Módosítsa a Kör osztályt Céltábla osztállyá úgy, hogy legyen benne metódus, amely egy adott pontnak a középponttól való távolsága szerint különböző pontszámokat ad!
    //      Ezután készítsen céllövő játékot: hozzon létre egy véletlenszerű középpontú céltáblát, majd kérjen be a felhasználótól 15 lövést (pontot),
    //      és számolja, hány pontnyi találata van a felhasználónak a 15 lövés után. Minden lövés után segítségül közölje, mekkora volt a lövés távolsága a céltáblától.
    #endregion
}