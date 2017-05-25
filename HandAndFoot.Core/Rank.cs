using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandAndFoot.Core
{
    public enum Rank
    {
        ACE = 1,
        TWO,
        THREE,
        FOUR,
        FIVE,
        SIX,
        SEVEN,
        EIGHT,
        NINE,
        TEN,
        JACK,
        QUEEN,
        KING,
        JOKER
    }

    public static class RankUtils
    {
        public static bool IsWild(this Rank r)
        {
            return r == Rank.TWO || r == Rank.JOKER;
        }

        public static Rank GetRank(string abbreviation)
        {
            switch (abbreviation)
            {
                case "A": return Rank.ACE;
                case "2": return Rank.TWO;
                case "3": return Rank.THREE;
                case "4": return Rank.FOUR;
                case "5": return Rank.FIVE;
                case "6": return Rank.SIX;
                case "7": return Rank.SEVEN;
                case "8": return Rank.EIGHT;
                case "9": return Rank.NINE;
                case "10": return Rank.TEN;
                case "J": return Rank.JACK;
                case "Q": return Rank.QUEEN;
                case "K": return Rank.KING;
                case "*J*": return Rank.JOKER;
            }
            throw new ArgumentOutOfRangeException(nameof(abbreviation));
        }
    }
}
