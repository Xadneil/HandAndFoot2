using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandAndFoot.Core
{
    [Serializable]
    public enum Suit
    {
        HEARTS,
        DIAMONDS,
        CLUBS,
        SPADES
    }

    public static class SuitUtils
    {
        public static bool IsRed(this Suit s)
        {
            return s == Suit.HEARTS || s == Suit.DIAMONDS;
        }
    }
}
