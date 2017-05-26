using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandAndFoot.Core
{
    [Serializable]
    public enum Round
    {
        ROUND1,
        ROUND2,
        ROUND3,
        ROUND4
    }

    public static class RoundUtils
    {
        public static int PointsToMeld(this Round r)
        {
            switch (r)
            {
                case Round.ROUND1:
                    return 50;
                case Round.ROUND2:
                    return 90;
                case Round.ROUND3:
                    return 120;
                case Round.ROUND4:
                    return 150;
            }
            return 0;
        }
    }
}
