using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireEmblemCombatSimGBA
{
    public enum WRank { N, E, D, C, B, A, S }

    class WeaponRank
    {
        public int EXP { get; set; }
        public WRank Rank
        {
            get
            {
                if (EXP >= 251) { return WRank.S; }
                else if (EXP >= 181) { return WRank.A; }
                else if (EXP >= 121) { return WRank.B; }
                else if (EXP >= 71) { return WRank.C; }
                else if (EXP >= 31) { return WRank.D; }
                else if (EXP >= 1) { return WRank.E; }
                else { return WRank.N; }
            }
        }
    }
}
