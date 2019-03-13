using FireEmblemCombatSimGBA.UnitClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireEmblemCombatSimGBA
{
    class Combat
    {
        public Unit Initiator { get; set; }
        public Terrain InitTerrain { get; set; }
        public Unit Target { get; set; }
        public Terrain TargetTerrain { get; set; }
        public int Distance { get; set; }

        void InitiatorAttacks()
        {
            Random rng = new Random();
            
            if ((rng.Next(0, 100) + rng.Next(0, 100)) / 2 < FEMath.Accuracy(Initiator, Target, Distance))
            {
                Target.HP -= FEMath.Damage(Initiator, Target, TargetTerrain);
            }

            if (Initiator.EquippedWeapon.SpecialEffect == WSpecialEffect.Brave)
            {
                if ((rng.Next(0, 100) + rng.Next(0, 100)) / 2 < FEMath.Accuracy(Initiator, Target, Distance))
                {
                    Target.HP -= FEMath.Damage(Initiator, Target, TargetTerrain);
                }
            }
        }
        void TargetAttacks()
        {
            // enemy counterattacks if within range
            if (Target.EquippedWeapon.MaxRange >= Distance && Target.EquippedWeapon.MinRange <= Distance)
            {
                Random rng = new Random();

                if ((rng.Next(0, 100) + rng.Next(0, 100)) / 2 < FEMath.Accuracy(Target, Initiator, Distance))
                {
                    Initiator.HP -= FEMath.Damage(Target, Initiator, InitTerrain);
                }

                if (Target.EquippedWeapon.SpecialEffect == WSpecialEffect.Brave)
                {
                    if ((rng.Next(0, 100) + rng.Next(0, 100)) / 2 < FEMath.Accuracy(Target, Initiator, Distance))
                    {
                        Initiator.HP -= FEMath.Damage(Target, Initiator, InitTerrain);
                    }
                }
            }
        }
        public void Battle()
        {
            InitiatorAttacks();
            
            if (Target.HP > 0)
            {
                TargetAttacks();
                
                // if player can double
                if (Initiator.HP > 0 && FEMath.DoubleAttack(Initiator, Target))
                {
                    InitiatorAttacks();
                }
                // if enemy can double
                else if (Initiator.HP > 0 && FEMath.DoubleAttack(Target, Initiator))
                {
                    TargetAttacks();
                }
            }

        }
    }
}
