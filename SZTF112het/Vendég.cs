using System;
using System.IO;

namespace Vendég
{

    enum EletkorKedvezmeny
    {
        diak, nyugdijas, felnott, pedagogus, csecsemo
    }

    enum SzerzodesesKedvezmeny
    {
        balatoni_tura, nyugdijas_klub, nincs
    }

    enum FizetesiMod
    {
        keszpenz, bankkartya, szep_kartya
    }


    class Vendeg
    {
        public string Nev { get; set; }

        public EletkorKedvezmeny KorKedvezmeny { get; set; }

        public SzerzodesesKedvezmeny Szerzodes { get; set; }

        public FizetesiMod Fizetes { get; set; }


        public string FajlSor {
            get {
                return $"{Nev}#{KorKedvezmeny}#{Szerzodes}#{Fizetes}";
            }
        }

        public Vendeg()
        {
        }


        public Vendeg(string sor) //fájlból való beolvasáshoz
        {
            string[] darabok = sor.Split('#');
            Nev = darabok[0];
            KorKedvezmeny = (EletkorKedvezmeny)Enum.Parse(typeof(EletkorKedvezmeny), darabok[1]);

            Szerzodes = (SzerzodesesKedvezmeny)Enum.Parse(typeof(SzerzodesesKedvezmeny), darabok[2]);
            Fizetes = (FizetesiMod)Enum.Parse(typeof(FizetesiMod), darabok[3]);
        }
    }

    class Vendegkezelo
    {
        public void UjVendeg(Vendeg vendeg)
        { //kiiírni a vendéget a fájl egy sorába
            File.AppendAllText("data.txt", vendeg.FajlSor + "\r\n");
        }

        public Vendeg[] Listaz()
        {
            string[] sorok = File.ReadAllLines("data.txt");
            Vendeg[] vendegek = new Vendeg[sorok.Length];
            for (int i = 0; i < sorok.Length; i++)
            {
                vendegek[i] = new Vendeg(sorok[i]);
            }
            return vendegek;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("1: Új vendég");
            Console.WriteLine("2: Listázás");
            int val = int.Parse(Console.ReadLine());

            switch (val)
            {
                case 1:
                    Uj();
                    break;
                case 2:
                    Listaz();
                    break;
                default:
                    Console.WriteLine("ilyen opció nincs");
                    break;
            }
            Console.ReadLine();
        }

        static void Listaz()
        {
            Vendegkezelo vk = new Vendegkezelo();
            Vendeg[] vendegek = vk.Listaz();
            for (int i = 0; i < vendegek.Length; i++)
            {
                Console.WriteLine($"{vendegek[i].Nev} {vendegek[i].KorKedvezmeny}" +
                $"{ vendegek[i].Szerzodes} { vendegek[i].Fizetes}");
            }
        }

        static void Uj()
        {
            Vendeg v = new Vendeg();
            Console.WriteLine("Add meg a nevet!");
            v.Nev = Console.ReadLine();

            Console.WriteLine("Eletkorkedvezmeny: ");
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"{i}: {(EletkorKedvezmeny)i}");

            }
            v.KorKedvezmeny = (EletkorKedvezmeny)int.Parse(Console.ReadLine());



            Console.WriteLine("Tagsagi kedvezmenyek: ");
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"{i}: {(SzerzodesesKedvezmeny)i}");
            }
            v.Szerzodes = (SzerzodesesKedvezmeny)int.Parse(Console.ReadLine());


            Console.WriteLine("Fizetési mód: ");
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"{i}: {(FizetesiMod)i}");

            }
            v.Fizetes = (FizetesiMod)int.Parse(Console.ReadLine());

            Vendegkezelo vk = new Vendegkezelo();
            vk.UjVendeg(v);
        }
    }
}