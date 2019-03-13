using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireEmblemCombatSimGBA.UnitClasses
{
    class Swordmaster : Unit
    {
        public override int Crit => base.Crit + 15;
    }
}
