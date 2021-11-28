using System;

public class Termek
{
    public string Nev { get; }
    public int Mennyiseg { get; }
    public int Ar { get; }

    public Termek(string nev, int mennyiseg, int ar)
    {
        Nev = nev;
        Mennyiseg = mennyiseg;
        Ar = ar;
    }

    public override string ToString()
    {
        return $"{Nev} ({Mennyiseg}) - [{Ar}] : {{{Mennyiseg * Ar}}}";
    }
}

public class Arus
{
    public string Nev { get; }
    public Termek[] Termekek { get; private set; }

    public Arus(string nev)
    {
        Nev = nev;
        Termekek = new Termek[0];
    }

    public void AruAtVetel(Termek termek)
    {
        Termek[] tmp = new Termek[Termekek.Length + 1];
        for (int i = 0; i < Termekek.Length; i++)
        {
            tmp[i] = Termekek[i];
        }
        tmp[Termekek.Length] = termek;
        Termekek = tmp;
    }

    public override string ToString()
    {
        string tmp = $"{Nev} \n";
        for (int i = 0; i < Termekek.Length; i++)
        {
            tmp += $" - '{Termekek[i].ToString()}' \n";
        }
        return tmp;
    }

    public int Vagyon()
    {
        int sum = 0;
        for (int i = 0; i < Termekek.Length; i++)
        {
            sum += Termekek[i].Ar * Termekek[i].Mennyiseg;
        }
        return sum;
    }
    public static Arus Leggazdagabb(Arus[] arusok)
    {
        int idx = 0;
        int sumMax = arusok[0].Vagyon();
        for (int i = 1; i < arusok.Length; i++)
        {
            int sum = arusok[i].Vagyon();
            if (sum > sumMax)
            {
                sumMax = sum;
                idx = i;
            }
        }
        return arusok[idx];
    }
}

class Prg
{
    static void Main(string[] args)
    {
        Arus dani = new Arus("Dani");
        Arus peter = new Arus("Péter");

        for (int i = 0; i < 3; i++)
        {
            dani.AruAtVetel(new Termek($"termek{i + 1}", 1, 10));
            peter.AruAtVetel(new Termek($"termek{i + 1}", 2, 20));
        }

        Console.WriteLine(dani.ToString());
        Console.WriteLine(peter.ToString());

        Console.WriteLine(Arus.Leggazdagabb(new Arus[] { dani, peter }).Nev);

        Arus levi = new Arus("Levi");
        levi.AruAtVetel(new Termek("Marlboro", 15, 2000));
        levi.AruAtVetel(new Termek("Palmal", 25, 1500));
        levi.AruAtVetel(new Termek("Multifitlter", 10, 1765));
        levi.AruAtVetel(new Termek("Rothmans", 30, 1450));
        Console.WriteLine(Arus.Leggazdagabb(new Arus[] { dani, levi, peter }).Nev); ;
    }
}