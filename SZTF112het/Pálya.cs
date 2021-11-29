using System;
using System.IO;

namespace Pálya
{
    enum AllatFaj
    {
        kutya, macska, tigris, puma, oroszlan, parduc
    }

    class Allat
    {
        public AllatFaj Faj { get; }

        public int Suly { get; }

        public int X { get; set; }

        public int Y { get; set; }

        public bool Halott { get; set; }

        public int Boldogsag { get; set; }

        public Allat(AllatFaj faj, int suly, int x, int y, int boldogsag)
        {
            Faj = faj;
            Suly = suly;
            X = x;
            Y = y;
            Boldogsag = boldogsag;
        }
    }



    class Palya
    {
        private Allat[] allatok;

        private Random r;

        public Palya()
        {
            AdatFeltoltes();
            r = new Random();
        }

        private void AdatFeltoltes()
        {
            string[] adatok = File.ReadAllLines(@".\bemenet\allatok.txt");
            allatok = new Allat[adatok.Length];
            string[] seged;
            for (int i = 0; i < adatok.Length; i++)
            {
                seged = adatok[i].Split(':', '-');
                allatok[i] = new Allat((AllatFaj)Enum.Parse(typeof(AllatFaj), seged[0]),
                                        int.Parse(seged[1]), int.Parse(seged[1]), int.Parse(seged[2]), int.Parse(seged[3]));
            }
        }

        private Allat Vizsgal(int x, int y)
        {
            int i = 0;
            while (i < allatok.Length && allatok[i].X != x && allatok[i].Y != y)
            {
                i++;
            }

            bool t = i < allatok.Length;
            if (t)
            {
                return allatok[i];
            }
            else
            {
                return null;
            }
        }

        private void Utkozes(Allat a1, Allat a2)
        {
            if (!a1.Halott && !a2.Halott)
            {
                if (a1.Boldogsag < 6 || a2.Boldogsag < 6)
                {
                    if (a1.Suly > a2.Suly)
                    {
                        a2.Halott = true;
                    }
                    else
                    {
                        a1.Halott = true;
                    }
                }
            }
        }

        private void Mozgat(Allat a)
        {
            int irany = r.Next(4);
            switch (irany)
            {
                case 0: //fel
                    if (a.Y - 1 >= 0)
                    {
                        Allat a2 = Vizsgal(a.X, a.Y - 1);
                        if (a2 != null)
                        {
                            a.Y--;
                            Utkozes(a, a2);
                        }
                        else
                        {
                            a.Y--;
                        }
                    }

                    break;
                case 1: //le
                    if (a.Y + 1 < 5)
                    {
                        Allat a2 = Vizsgal(a.X, a.Y + 1);
                        if (a2 != null)
                        {
                            a.Y++;
                            Utkozes(a, a2);
                        }
                        else
                        {
                            a.Y++;
                        }
                    }

                    break;
                case 2: //jobbra
                    if (a.X < 5)
                    {
                        Allat a2 = Vizsgal(a.X + 1, a.Y);
                        if (a2 != null)
                        {
                            a.X++;
                            Utkozes(a, a2);
                        }
                        else
                        {
                            a.X++;
                        }
                    }

                    break;
                case 3: //balra
                    if (a.X >= 0)
                    {
                        Allat a2 = Vizsgal(a.X - 1, a.Y);
                        if (a2 != null)
                        {
                            a.X--;
                            Utkozes(a, a2);
                        }
                        else
                        {
                            a.X--;
                        }
                    }

                    break;
                default:
                    break;
            }
        }

        private void Kiiras()
        {
            StreamWriter sw;
            if (!Directory.Exists("stat"))
            {
                Directory.CreateDirectory("stat");
                sw = new StreamWriter(@".\stat\statisztika.txt");
                foreach (Allat a in allatok)
                {
                    string seged = a.Halott ? "Halott" : "El";
                    sw.WriteLine($"Faj:{a.Faj.ToString()}, Suly:{a.Suly}, Boldogsag:{a.Boldogsag}, {seged}");
                }
            }
            else
            {
                sw = new StreamWriter(@".\stat\statisztika.txt");
                foreach (Allat a in allatok)
                {
                    string seged = a.Halott ? "Halott" : "El";
                    sw.WriteLine($"Faj:{a.Faj.ToString()}, Suly:{a.Suly}, Boldogsag:{a.Boldogsag}, {seged}");
                }
            }
            sw.Close();
        }

        public void Szimulal(int kor)
        {
            for (int i = 0; i < kor; i++)
            {
                foreach (Allat a in allatok)
                {
                    if (!a.Halott)
                    {
                        Mozgat(a);
                    }
                }
            }
            Kiiras();
        }
    }



    class Program
    {
        static void Main(string[] args)
        {
            Palya p = new Palya();
            p.Szimulal(100 );
        }
    }

    

}

