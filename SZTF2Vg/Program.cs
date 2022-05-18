using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SZTF2Vg
{

    abstract class Absz
    {
        public int walami { get; set; }
    }

    class Lesz : Absz
    {
        public int valami { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Lesz a = new Lesz();
            Absz b = (Absz)a;
            Absz c = a as Absz;
        }
    }
}
