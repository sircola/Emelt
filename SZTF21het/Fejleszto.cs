using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Orokles
{
    sealed class Fejleszto : Alkalmazott
    {

        public int GitRepokSzama { get; set; }

        public double AtlagosKodsorProjektenkent { get; set; }

        public override int Jutalek
        {
            get
            {
                return (int)(GitRepokSzama * AtlagosKodsorProjektenkent) / Belepes.Year - 2000;
            }
        }

        public Fejleszto(string nev, int ev, int alapfizetes, int gitrepok, double atlagkodsor)
            : base(nev, ev, alapfizetes)
        {
            this.GitRepokSzama = gitrepok;
            this.AtlagosKodsorProjektenkent = atlagkodsor;
        }

        public override string ToString()
        {
            return "Én egy fejlesztő vagyok, a nevem: " + this.Nev;
        }
    }


}
