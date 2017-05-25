using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandAndFoot.Core
{
    public struct Card
    {
        public readonly Rank Rank;
        public readonly Suit Suit;

        public Card(Rank r, Suit s)
        {
            Rank = r;
            Suit = s;
        }

        public Card(string r, Suit s)
        {
            Rank = RankUtils.GetRank(r);
            Suit = s;
        }

        public int Points()
        {
            switch (Rank)
            {
                case Rank.ACE:
                case Rank.TWO:
                    return 20;
                case Rank.JOKER:
                    return 50;
                case Rank.THREE:
                    return Suit.IsRed() ? 500 : 0;
                case Rank.FOUR:
                case Rank.FIVE:
                case Rank.SIX:
                case Rank.SEVEN:
                case Rank.EIGHT:
                    return 5;
                case Rank.NINE:
                case Rank.TEN:
                case Rank.JACK:
                case Rank.QUEEN:
                case Rank.KING:
                    return 10;
            }
            throw new InvalidOperationException($"The rank {Rank} is not valid.");
        }
    }
}
