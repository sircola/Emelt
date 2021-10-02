using System;

namespace SZTF2Zh1
{
    class Program
    {
        static void Main(string[] args)
        {
            StarFighter[] starFighterek = new StarFighter[50];
            for (int i = 0; i < starFighterek.Length; i++)
            {
                starFighterek[i] = StarFighterCreator();
            }
            for (int i = 0; i < starFighterek.Length; i++)
            {
                starFighterek[i].BatteryOut += Program_BatteryOut;
                starFighterek[i].Exploded += Program_Exploded;
                starFighterek[i].ShieldOut += Program_ShieldOut;
            }
            Array.Sort(starFighterek);

            bool seged = false;

            for (int i = 0; i < starFighterek.Length; i++)
            {
                int szamlalo = 0;
                if (starFighterek[i]==null)
                {
                    szamlalo++;
                }
                if (szamlalo==50)
                {
                    seged = true;
                }
            }

            while (!seged)
            {
                for (int k = 0; k < starFighterek.Length; k++)
                {
                    starFighterek[k].TimeStep();

                    System.Threading.Thread.Sleep(500);
                }
            }
            ;
        }

        private static void Program_ShieldOut(StarFighter current)
        {
            Console.WriteLine(current.ID+ " ShieldOut");
        }

        private static void Program_Exploded(StarFighter current)
        {
            Console.WriteLine(current.ID + " Exploded");
            current = null;
        }

        private static void Program_BatteryOut(StarFighter current)
        {
            Console.WriteLine(current.ID + " BatteryOut");
        }

        static StarFighter StarFighterCreator()
        {
            try
            {
                int seged = myrandom.r.Next(0, 3);
                int ev = myrandom.r.Next(-99, 100);
                if (seged == 1)
                {
                    return new TieInterceptor(ev, true, StarFighterCondition.good);
                }
                if (seged == 2)
                {
                    return new TieBomber(ev, true, StarFighterCondition.good);
                }
                else
                {
                    return new TieFighter(ev, true, StarFighterCondition.good);
                }

            }
            catch (InvalidYearNumberException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.Id);
                return StarFighterCreator();
            }
        }

    }
}
