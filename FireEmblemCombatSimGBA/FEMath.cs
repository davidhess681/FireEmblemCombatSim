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
        public static int Damage(Unit Attacker, Unit Defender, Terrain DefTerrain)
        {
            Weapon w1 = Attacker.EquippedWeapon;
            Weapon w2 = Defender.EquippedWeapon;
            int attack = (Attacker.Strength / RuneswordRule(w1)) +
                ((w1.Might + WeaponTriangleBonus(w1, w2)) * EffectiveCoefficient(w1, Defender));
            int defense = (w1.IsMagic ? Defender.Resistance : Defender.Defense) + DefTerrain.DefenseBonus;
            int crit = CritCoefficient(Attacker, Defender);

            return (attack - defense) * crit;
        }
        public static bool DoubleAttack(Unit Attacker, Unit Defender)
        {
            if (Attacker.CombatSpeed - Defender.CombatSpeed >= 4)
            {
                return true;
            }
            return false;
        }
        public static int Accuracy(Unit Attacker, Unit Defender, Terrain DefTerrain)
        {
            int accuracy = Attacker.Hit + (WeaponTriangleBonus(Attacker.EquippedWeapon, Defender.EquippedWeapon) * 15);
            int avoid = Defender.Avoid + DefTerrain.AvoidBonus;

            return accuracy - avoid;
        }
        public static int Accuracy(Unit StaffUser, Unit Target, int Distance)
        {
            int accuracy = 30 + (StaffUser.Strength * 5) + StaffUser.Skill;
            int avoid = (StaffUser.Resistance * 5) + (Distance * 2);

            return accuracy - avoid;
        }
    }
}
