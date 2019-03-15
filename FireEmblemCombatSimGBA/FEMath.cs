using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireEmblemCombatSimGBA.UnitClasses
{
    class FEMath
    {
        static int WeaponTriangleBonus(Weapon w1, Weapon w2)
        {
            int result = 1;
            WType weapon1 = w1.Type;
            WType weapon2 = w2.Type;

            if (w1.SpecialEffect == WSpecialEffect.Reaver ^ w2.SpecialEffect == WSpecialEffect.Reaver) { result *= 2; }

            switch (weapon1)
            {
                case WType.Sword:
                    if (weapon2 == WType.Axe) { return result; }
                    if (weapon2 == WType.Lance) { return -result; }
                    break;
                case WType.Axe:
                    if (weapon2 == WType.Lance) { return result; }
                    if (weapon2 == WType.Sword) { return -result; }
                    break;
                case WType.Lance:
                    if (weapon2 == WType.Sword) { return result; }
                    if (weapon2 == WType.Axe) { return -result; }
                    break;
                case WType.Anima:
                    if (weapon2 == WType.Light) { return result; }
                    if (weapon2 == WType.Dark) { return -result; }
                    break;
                case WType.Light:
                    if (weapon2 == WType.Dark) { return result; }
                    if (weapon2 == WType.Anima) { return -result; }
                    break;
                case WType.Dark:
                    if (weapon2 == WType.Anima) { return result; }
                    if (weapon2 == WType.Light) { return -result; }
                    break;
                default:
                    break;
            }
            return 0;
        }
        static int RuneswordRule(Weapon w)
        {
            if (w.SpecialEffect == WSpecialEffect.MagSword)
            {
                return 2;
            }
            return 1;
        }
        static int EffectiveCoefficient(Weapon w, Unit Defender)
        {
            if (w.EffectiveVs == Defender.MoveType)
            {
                return 2;
            }
            if (w.DragonSlayer && Defender.IsDragon)
            {
                return 2;
            }
            return 1;
        }
        static int CritCoefficient(Unit Attacker, Unit Defender)
        {
            Random rng = new Random();
            int critRate = Attacker.Crit - Defender.CritAvoid;

            if (rng.Next(0, 100) < critRate)
            {
                return 3;
            }
            return 1;
        }

        public static void DealDamage(Combat thisBattle)
        {
            Unit Attacker = thisBattle.DirectionOfCombat ? thisBattle.Initiator : thisBattle.Target;
            Unit Defender = !thisBattle.DirectionOfCombat ? thisBattle.Initiator : thisBattle.Target;
            Terrain DefTerrain = !thisBattle.DirectionOfCombat ? thisBattle.InitTerrain : thisBattle.TargetTerrain;
            Weapon w1 = Attacker.EquippedWeapon;
            Weapon w2 = Defender.EquippedWeapon;

            int attack = (Attacker.Strength / RuneswordRule(w1)) +
                ((w1.Might + WeaponTriangleBonus(w1, w2)) * EffectiveCoefficient(w1, Defender));
            int defense = (w1.IsMagic ? Defender.Resistance : Defender.Defense) + DefTerrain.DefenseBonus;
            int crit = CritCoefficient(Attacker, Defender);

            Defender.HP -= (attack - defense) * crit;
        }
        public static bool DoubleAttack(int AttackerSpeed, int DefenderSpeed)
        {
            if (AttackerSpeed - DefenderSpeed >= 4)
            {
                return true;
            }
            return false;
        }
        public static int Accuracy(Combat thisBattle)
        {
            Unit Attacker = thisBattle.DirectionOfCombat ? thisBattle.Initiator : thisBattle.Target;
            Unit Defender = !thisBattle.DirectionOfCombat ? thisBattle.Initiator : thisBattle.Target;
            Terrain DefTerrain = !thisBattle.DirectionOfCombat ? thisBattle.InitTerrain : thisBattle.TargetTerrain;

            int accuracy = Attacker.Hit + (WeaponTriangleBonus(Attacker.EquippedWeapon, Defender.EquippedWeapon) * 15);
            int avoid = Defender.Avoid + DefTerrain.AvoidBonus;

            return accuracy - avoid;
        }
        public static int StaffAccuracy(Combat thisBattle)
        {
            Unit StaffUser = thisBattle.Initiator;
            Unit Target = thisBattle.Target;
            int Distance = thisBattle.Distance;

            int accuracy = 30 + (StaffUser.Strength * 5) + StaffUser.Skill;
            int avoid = (Target.Resistance * 5) + (Distance * 2);

            return accuracy - avoid;
        }
    }
}
