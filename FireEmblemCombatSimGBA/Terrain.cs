using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireEmblemCombatSimGBA.UnitClasses
{
    abstract class Terrain
    {
        public virtual int AvoidBonus { get; }
        public virtual int DefenseBonus { get; }
    }

    class Plain : Terrain
    {
        public override int AvoidBonus => 0;
        public override int DefenseBonus => 0;
    }
    class Sand : Terrain
    {
        public override int AvoidBonus => 5;
        public override int DefenseBonus => 0;
    }
    class Building : Terrain
    {
        public override int AvoidBonus => 10;
        public override int DefenseBonus => 0;
    }
    class Forest : Terrain
    {
        public override int AvoidBonus => 20;
        public override int DefenseBonus => 1;
    }
    class Mountain : Terrain
    {
        public override int AvoidBonus => 30;
        public override int DefenseBonus => 1;
    }
    class Peak : Terrain
    {
        public override int AvoidBonus => 40;
        public override int DefenseBonus => 2;
    }
    class Fort : Terrain
    {
        public override int AvoidBonus => 20;
        public override int DefenseBonus => 2;
    }
}
