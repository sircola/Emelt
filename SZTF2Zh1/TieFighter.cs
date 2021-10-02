using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SZTF2Zh1
{
    sealed class TieFighter : StarFighter
    {
        

        public int ExtraBatteryValue { get; set; }
        public TieFighter(int YearOfProduction, bool IsUsed, StarFighterCondition Condition) : base(YearOfProduction, IsUsed, Condition)
        {
            ExtraBatteryValue = myrandom.r.Next(20, 100);

        }

        public override event StarFighterEventHandler ShieldOut;
        public override event StarFighterEventHandler BatteryOut;
        public override event StarFighterEventHandler Exploded;

        public override void TimeStep()
        {
            if (myrandom.r.Next(0, 100) <= 70)
            {
                ShieldValue -= 10;
                BatteryValue -= 10;
                ExtraBatteryValue -= 10;
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
