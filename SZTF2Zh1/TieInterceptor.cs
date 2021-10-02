using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SZTF2Zh1
{
    sealed class TieInterceptor : StarFighter
    {
        public bool Hyperdrive { get; set; }
        public TieInterceptor(int YearOfProduction, bool IsUsed, StarFighterCondition Condition) : base(YearOfProduction, IsUsed, Condition)
        {

            if (myrandom.r.Next(0,2)==1)
            {
                Hyperdrive = true;
            }
            else
            {
                Hyperdrive = false;
            }
        }

        public override event StarFighterEventHandler ShieldOut;
        public override event StarFighterEventHandler BatteryOut;
        public override event StarFighterEventHandler Exploded;

        public override void TimeStep()
        {
            if (Hyperdrive==true)
            {
                if (myrandom.r.Next(0, 100) <= 20)
                {
                    ShieldValue -= 10;
                    BatteryValue -= 10;
                }
                
            }
            else
            {
                if (myrandom.r.Next(0, 100) <= 60)
                {
                    ShieldValue -= 10;
                    BatteryValue -= 10;
                }
            }
            if (BatteryValue == 0)
            {
                BatteryOut?.Invoke(this);
            }
            if (ShieldValue == 0)
            {
                ShieldOut?.Invoke(this);
            }
            if (ShieldValue == 0 && BatteryValue == 0)
            {
                Exploded?.Invoke(this);
            }
        }
    }
}
