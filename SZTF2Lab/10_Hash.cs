using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Hallgató
    {
        public string Név { get; set; } = "";

        public override int GetHashCode()
        {
            return Név.GetHashCode();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<Hallgató, int> dict = new Dictionary<Hallgató, int>();

            Hallgató h1 = new Hallgató() { Név = "Béla" };
            Hallgató h2 = new Hallgató() { Név = "Sanyi" };
            Hallgató h3 = new Hallgató() { Név = "Gizi" };

            dict.Add(h1, 1);
            dict.Add(h2, 2);
            dict.Add(h3, 3);

            Console.WriteLine(dict[h1]);
            Console.WriteLine(dict[h2]);
            Console.WriteLine(dict[h3]);

            Hallgató h = new Hallgató();
            bool e = dict.TryGetValue(h1, out int value);
            if (e) Console.WriteLine(value); //Console.WriteLine(dict[h]);
            else Console.WriteLine("nono");

            h1.Név = "Béla2";
            Console.WriteLine(dict[h1]);

            foreach (KeyValuePair<Hallgató, int> item in dict)
            {
                Console.WriteLine(item.Key.Név);
                Console.WriteLine(item.Value);
            }
        }
    }
}
