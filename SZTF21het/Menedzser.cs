using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Orokles
{
    class Menedzser : Alkalmazott
    {

        public int BeosztottakSzama { get; set; }

        public override int Jutalek
        {
            get
            {
                return ((int)(DateTime.Now - Belepes).TotalDays / 365) * BeosztottakSzama * 5000;
            }
        }

        public Menedzser(string nev, int ev, int alapfizetes, int beosztottak)
            : base(nev, ev, alapfizetes)
        {
            this.BeosztottakSzama = beosztottak;
        }
        public override string ToString()
        {
            return "Én egy menedzser vagyok, a nevem: " + this.Nev;
        }
    }
}
