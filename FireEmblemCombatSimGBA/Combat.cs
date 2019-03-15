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
        public bool DirectionOfCombat { get; set; }     // indicates whether the initiator is the one dealing or taking damage

        bool Roll(int accuracy)
        {
            Random rng = new Random();
            if ((rng.Next(0, 100) + rng.Next(0, 100)) / 2 < accuracy)
            {
                return true;
            }
            return false;
        }
        void DealDamage()
        {
            Random rng = new Random();
            
            if (Roll(FEMath.Accuracy(this)))
            {
                FEMath.DealDamage(this);
            }
        }
        bool[] Rounds()
        {
            // determines who is attacking and how long combat runs

            List<bool> result = new List<bool>();

            // initiator's attack
            result.Add(true);
            if (Initiator.EquippedWeapon.SpecialEffect == WSpecialEffect.Brave) { result.Add(true); }

            // determine counterattack
            if (Target.EquippedWeapon.MaxRange >= Distance && Target.EquippedWeapon.MinRange <= Distance)
            {
                result.Add(false);
                if (Target.EquippedWeapon.SpecialEffect == WSpecialEffect.Brave) { result.Add(false); }
            }

            // determine follow-up attack
            if (FEMath.DoubleAttack(Initiator.CombatSpeed, Target.CombatSpeed))
            {
                result.Add(true);
                if (Initiator.EquippedWeapon.SpecialEffect == WSpecialEffect.Brave) { result.Add(true); }
            }
            else if (FEMath.DoubleAttack(Target.CombatSpeed, Initiator.CombatSpeed))
            {
                if (Target.EquippedWeapon.MaxRange >= Distance && Target.EquippedWeapon.MinRange <= Distance)
                {
                    result.Add(false);
                    if (Target.EquippedWeapon.SpecialEffect == WSpecialEffect.Brave) { result.Add(false); }
                }
            }

            return result.ToArray();
        }

        public void Battle()
        {
            var rounds = Rounds();
            for (int i = 0; i < rounds.Length; i++)
            {
                DirectionOfCombat = rounds[i];
                DealDamage();

                if (Initiator.HP <= 0 || Target.HP <= 0) { break; }
            }
        }
        public void UseStaff(Staff staff)
        {
            Random rng = new Random();

            if (Roll(FEMath.StaffAccuracy(this)))
            {
                Target.Status = staff.Effect;
            }
        }

    }
}
