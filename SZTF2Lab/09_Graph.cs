using System;
using System.Collections.Generic;

namespace _08_Graf
{
    class Program
    {
        static SzomszedsagiLista szl;
        static CsucsMatrix csm;

        static void Main(string[] args)
        {
            // A szomszédsági listás, illetve a csúcsmátrixos megoldás futási idejének szemléltetésére nagyméretű gráfokat generálunk. A GrafGen metódus két egyforma gráfot hoz létre, különböző tárolási móddal.
            // Ne felejtsük el, hogy a tárolási mód megválasztásakor nem csak a futási időre kell tekintettel lennünk, hanem a memóriaigényre is! A csúcsmátrixok nagyobb csúcsszám esetén nagy mennyiségű memóriát foglalnak!

            GrafGen(50000, 50000);

            DateTime start = DateTime.Now; // Elmentjük az aktuális időt
            szl.SzelessegiBejaras(0);
            Console.WriteLine($"Szomszédsági lista:\t{(DateTime.Now - start).TotalMilliseconds}ms");  // Kiírjuk a jelenlegi, és az elmentett idő között eltelt miliszekundumok számát.

            start = DateTime.Now;
            csm.SzelessegiBejaras(0);
            Console.WriteLine($"Csúcsmátrix:\t\t{(DateTime.Now - start).TotalMilliseconds}ms");

            Console.ReadLine();
        }

        static void GrafGen(int meret, int suruseg) // sűrűség az élek száma
        {
            Random rnd = new Random();
            csm = new CsucsMatrix(meret);
            szl = new SzomszedsagiLista(meret);

            for (int i = 0; i < suruseg; i++)
            {
                int rand1 = rnd.Next(0, meret);
                int rand2 = rnd.Next(0, meret);

                csm.ElFelvetel(rand1, rand2);
                szl.ElFelvetel(rand1, rand2);
            }
        }

        static SzomszedsagiLista GrafGenSZL(int meret, int suruseg)
        {
            SzomszedsagiLista szl = new SzomszedsagiLista(meret);
            Random rnd = new Random();
            for (int i = 0; i < suruseg; i++)
                szl.ElFelvetel(rnd.Next(0, meret), rnd.Next(0, meret));
            return szl;
        }

        static CsucsMatrix GrafGenCSM(int meret, int suruseg)
        {
            CsucsMatrix csm = new CsucsMatrix(meret);
            Random rnd = new Random();
            for (int i = 0; i < suruseg; i++)
                csm.ElFelvetel(rnd.Next(0, meret), rnd.Next(0, meret));
            return csm;
        }

    }

    abstract class Graf
    {
        protected int N;
        public Graf(int csucsokszama) { N = csucsokszama; }

        public List<int> Szomszedok(int cs)
        {
            List<int> szomszedok = new List<int>();
            List<int> csucsok = Csucsok(); // leszármazottak definiálják - Csucsok()

            for (int i = 0; i < csucsok.Count; i++)
                if (VezetEl(cs, csucsok[i])) // leszármazott definiálja - VezetEl()
                    szomszedok.Add(csucsok[i]);

            return szomszedok;
        }

        public abstract List<int> Csucsok();
        public abstract void ElFelvetel(int honnan, int hova);
        public abstract bool VezetEl(int honnan, int hova);


        // A szélességi bejárás során kiindulunk egy adott csomópontból (int s), majd ennek szomszédain keresztül kezdjük bejárni a gráfot.
        // Az 's' kiinduló pont minden szomszédját egy sorba helyezzük, majd ezt követően a sorból egyesével kivesszük az elemeket,
        // amelyeknek a szomszédait szintén belehelyezzük a sorba.
        // Az F listában elhelyezünk minden már vizsgált elemet, elkerülve így, hogy amennyiben két különböző csúcsnak közös szomszédja van,
        // akkor az adott csúcsot kétszer vizsgáljuk meg.
        public void SzelessegiBejaras(int s) // adott csomópontból indulunk ki
        {
            List<int> F = new List<int>(); // feldolgozott elemek - elkerülve az újrafeldolgozást (közös szomszéd esetén)
            Queue<int> S = new Queue<int>(); // feldolgozatlan elemek

            F.Add(s);
            S.Enqueue(s);
            while (S.Count != 0) // addig megyünk míg van feldolgozatlan csomópont
            {
                int k = S.Dequeue(); // legelső elem kivétele
                //Console.WriteLine(k); // feldolgozás
                List<int> szomszedok = Szomszedok(k);
                for (int i = 0; i < szomszedok.Count; i++)
                {
                    if (!F.Contains(szomszedok[i]))
                    {
                        S.Enqueue(szomszedok[i]); // a csomópont minden szomszédját egy sorba helyezzük
                        F.Add(szomszedok[i]);
                    }
                }
            }
        }

        public void MelysegiBejaras(int start) // dia.18 - rekurziót indító fv
        {
            List<int> F = new List<int>();
            MelysegiBejarasRek(start, F);
        }
        // Mélységi bejárás során ugyanazon az elven indulunk el, mint a szélességi bejáráskor,
        // azonban sor helyett itt verem megoldást alkalmazunk. Ezt gyakorlatban viszont csak rekurzívan
        // tudjuk megoldani, ahol a verem a tulajdonképpen a rekurzió során megvalósult hívás-hierarchia.
        void MelysegiBejarasRek(int cs, List<int> F)
        {
            F.Add(cs);
            Console.WriteLine(cs);
            foreach (int x in Szomszedok(cs))
                if (!F.Contains(x))
                    MelysegiBejarasRek(x, F);
        }
    }


    // A csúcsmátrixhoz létrehozunk egy NxN-es mátrixot, ahol N a csomópontok száma.
    // A mátrix adott cellája tartalmazza, hogy az adott pontból egy adott másikba vezet-e él.
    class CsucsMatrix : Graf        // Pl.: Ha a mátrix [3,18] indexű eleme 1, akkor a 3-as csomópontból vezet él a 18-as csomópontba. Ha az érték 0, akkor nem vezet él. Súlyozott mátrix esetén közvetlenül a súlyt is eltárolhatjuk.
    {
        public int[,] CS;

        public CsucsMatrix(int csucsokszama) : base(csucsokszama)
        {
            CS = new int[csucsokszama, csucsokszama];
        }

        public override List<int> Csucsok()
        {
            List<int> cs = new List<int>();

            for (int i = 0; i < N; i++)
                cs.Add(i);

            return cs;
        }

        public override void ElFelvetel(int honnan, int hova)
        {
            CS[honnan, hova] = 1; // van él ha 1
        }

        public override bool VezetEl(int honnan, int hova)
        {
            return CS[honnan, hova] != 0;
        }
    }


    // A szomszédsági listákban való tároláshoz listák tömbjét kell létrehoznunk.
    // Annyi üres listát hozunk létre, ahány csúcs van a gráfban. 
    class SzomszedsagiLista : Graf              // Az egyes listák tárolják az adott csúcs szomszédait, tehát azon csúcsok számát, amelyekbe vezet él. Adott csúcs száma == a lista indexe a tömbben.
    {
        List<int>[] L; // lista tömb

        public SzomszedsagiLista(int csucsokszama) : base(csucsokszama)
        {
            L = new List<int>[csucsokszama];
            for (int i = 0; i < L.Length; i++)
                L[i] = new List<int>();
        }

        public override List<int> Csucsok()
        {
            List<int> cs = new List<int>();

            for (int i = 0; i < N; i++)
                cs.Add(i);

            return cs;
        }

        public override void ElFelvetel(int honnan, int hova)
        {
            L[honnan].Add(hova); // irányított
        }

        public override bool VezetEl(int honnan, int hova)
        {
            for (int i = 0; i < L[honnan].Count; i++)
                if (L[honnan][i] == hova)
                    return true;
            return false;
        }
    }
}