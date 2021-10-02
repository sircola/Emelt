using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SZTF2Zh1
{
    sealed class TieBomber : StarFighter
    {
        public int BombNumber { get; set; }
        public TieBomber(int YearOfProduction, bool IsUsed, StarFighterCondition Condition) : base(YearOfProduction, IsUsed, Condition)
        {
            BombNumber = myrandom.r.Next(4,10);
        }

        public override event StarFighterEventHandler ShieldOut;
        public override event StarFighterEventHandler BatteryOut;
        public override event StarFighterEventHandler Exploded;

        public override void TimeStep()
        {
            if (myrandom.r.Next(0, 100) <= 50)
            {
                ShieldValue -= 10;
                BatteryValue -= 10;
                BombNumber -= 1;
            }
            if (BatteryValue==0)
            {
                BatteryOut?.Invoke(this);
            }
            if (ShieldValue==0)
            {
                ShieldOut?.Invoke(this);
            }
            if (ShieldValue==0&&BatteryValue==0)
            {
                Exploded?.Invoke(this);
            }
        }
    }
}
