using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireEmblemCombatSimGBA
{
    public enum UMoveType { Infantry, Cavalry, Flying, Armored }
    abstract class Unit
    {
        
        public int Level
        {
            get
            {
                return 1 + (Exp / 100);
            }
        }
        public int Exp { get; set; }
        public int MaxHP { get; set; }
        public int HP { get; set; }
        public int Strength { get; set; }
        public int Skill { get; set; }
        public int Speed { get; set; }
        public int Luck { get; set; }
        public int Defense { get; set; }
        public int Resistance { get; set; }
        public int Move { get; set; }
        public int Constitution { get; set; }
        public virtual int Aid
        {
            get
            {
                return Constitution - 1;
            }
        }
        public WStaffEffect Status { get; set; }
        public Weapon EquippedWeapon { get; set; }

        public UMoveType MoveType { get; set; }
        public bool IsDragon { get; set; }

        int SRankBonus()
        {
            switch (EquippedWeapon.Type)
            {
                case WType.Sword:
                    if (Sword.Rank == WRank.S) { return 5; }
                    break;
                case WType.Lance:
                    if (Lance.Rank == WRank.S) { return 5; }
                    break;
                case WType.Axe:
                    if (Axe.Rank == WRank.S) { return 5; }
                    break;
                case WType.Bow:
                    if (Bow.Rank == WRank.S) { return 5; }
                    break;
                case WType.Anima:
                    if (Anima.Rank == WRank.S) { return 5; }
                    break;
                case WType.Light:
                    if (Light.Rank == WRank.S) { return 5; }
                    break;
                case WType.Dark:
                    if (Dark.Rank == WRank.S) { return 5; }
                    break;
            }
            return 0;
        }
        public int Attack
        {
            get
            {
                return Strength + EquippedWeapon.Might;
            }
        }
        public int Hit
        {
            get
            {
                return EquippedWeapon.Hit + (Skill * 2) + (Luck / 2) + SRankBonus();
            }
        }
        public int CombatSpeed
        {
            get
            {
                if (EquippedWeapon.Weight >= Constitution) { return Speed - (EquippedWeapon.Weight - Constitution); }
                return Speed;
            }
        }
        public int MinRange
        {
            get
            {
                return EquippedWeapon.MinRange;
            }
        }
        public int MaxRange
        {
            get
            {
                return EquippedWeapon.MaxRange;
            }
        }
        public virtual int Crit
        {
            get
            {
                return EquippedWeapon.Crit + (Skill / 2) + SRankBonus();
            }
        }
        public int Avoid
        {
            get
            {
                return (CombatSpeed * 2) + Luck;
            }
        }
        public int CritAvoid
        {
            get
            {
                return Luck;
            }
        }
        
        public WeaponRank Sword { get; set; }
        public WeaponRank Lance { get; set; }
        public WeaponRank Axe { get; set; }
        public WeaponRank Bow { get; set; }
        public WeaponRank Anima { get; set; }
        public WeaponRank Light { get; set; }
        public WeaponRank Dark { get; set; }
        public WeaponRank Staff { get; set; }
    }
}
