using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SZTF2Zh1
{
    public delegate void StarFighterEventHandler(StarFighter current);
    public abstract class StarFighter:IComparable
    {
        private int seged;
        public abstract event StarFighterEventHandler ShieldOut;

        public abstract event StarFighterEventHandler BatteryOut;

        public abstract event StarFighterEventHandler Exploded;

        public StarFighter(int yearOfProduction, bool IsUsed, StarFighterCondition Condition)
        {
            if (yearOfProduction>0)
            {
                throw new InvalidYearNumberException(this);
            }
            else
            {
                YearOfProduction = yearOfProduction;
            }
            this.IsUsed = IsUsed;
            this.Condition = Condition;
            ShieldValue = (int)this.Condition;
            BatteryValue = 100;
        }

        public string ID { get {

                char c = (char)myrandom.r.Next('A','Z');
                
                seged= myrandom.r.Next(1001, 9999);
                return "" + c + seged;
            
            }  }
        public int YearOfProduction { get;  }

        public bool IsUsed { get; set; }
        public StarFighterCondition Condition { get;  }
        public int ShieldValue { get; protected set; }
        public int BatteryValue { get; protected set; }


        public abstract void TimeStep();

        public int CompareTo(object obj)
        {
            StarFighter help = (obj as StarFighter);
            if (this.seged>help.seged)
            {
                return 1;
            }
            if (this.seged < help.seged)
            {
                return 0;
            }
            else
            {
                return 0;
            }
        }
    }
}
