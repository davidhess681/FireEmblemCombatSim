﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireEmblemCombatSimGBA
{
    public enum WType { Unarmed, Sword, Lance, Axe, Bow, Anima, Light, Dark, Dragonstone }
    public enum WSpecialEffect { None, Reaver, MagSword, Poison, Brave }
    public enum WStaffEffect { None, Silence, Sleep, Berserk }
    struct Weapon
    {
        public WRank MinimumRank { get; set; }
        public WType Type { get; set; }
        public UMoveType EffectiveVs { get; set; }
        public WSpecialEffect SpecialEffect { get; set; }
        public int MinRange { get; set; }
        public int MaxRange { get; set; }
        public int Weight { get; set; }
        public int Might { get; set; }
        public int Hit { get; set; }
        public int Crit { get; set; }
        public bool DragonSlayer { get; set; }
        public bool IsMagic
        {
            get
            {
                if (Type == WType.Anima || Type == WType.Light || Type == WType.Dark)
                {
                    return true;
                }
                if (SpecialEffect == WSpecialEffect.MagSword)
                {
                    return true;
                }
                return false;
            }
        }
    }

    struct Staff
    {
        public WRank MinimumRank { get; set; }
        public WStaffEffect Effect { get; set; }
        public bool TargetsAlly { get; set; }
    }
}
