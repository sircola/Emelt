using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Orokles
{
    abstract class Alkalmazott
    {
        public string Nev { get; set; }

        protected DateTime Belepes { get; set; }

        public int AlapFizetes { get; set; }

        public abstract int Jutalek
        {
            get;
        }

        public Alkalmazott(string nev, int ev, int alapfizetes)
        {
            this.Nev = nev;
            this.Belepes = new DateTime(ev, 1, 1);
            this.AlapFizetes = alapfizetes;
        }

        public override bool Equals(object obj)
        {
            Alkalmazott a = this;
            Alkalmazott b = obj as Alkalmazott;

            return a.Nev == b.Nev && a.Belepes == b.Belepes;
        }

        

    }
}
