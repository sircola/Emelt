using System;

namespace SZTF2HF
{
    public enum KurzusKategória
    {
        Online,
        Offline
    }

    public enum VizsgaTípus
    {
        Szóbeli,
        Írásbeli
    }

    interface IKurzus
    {
        string Kód { get; set; }
        KurzusKategória Típus { get; set; }
        int Nap { get; set; }
        int ÓraKezdet { get; set; }
        int ÓraVég { get; set; }
        bool Ütközik( IKurzus[] kurzusok);
        Tantárgy Tantárgy { get; set; }
    }

    abstract class Tantárgy
    {
        public IKurzus fej;
        public string Név { get; }
        public int Kredit { get; } 
        public int Félév { get; }
        public int Kreditek { get; set; }
    }

    class Gyakorlat : Tantárgy
    {

    }

    class Labor : Gyakorlat
    {

    }

    class Előadás : Tantárgy
    {
        public VizsgaTípus vizsgatípus { get; set; } 
    }

    class VizsgaKurzus : Tantárgy
    {

    }

    class SZTF1 : Tantárgy
    {

    }
    
    class Analízis : Tantárgy
    {

    }

    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
