using HandAndFoot.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandAndFoot.Messages.ToClient
{
    [Serializable]
    public class DrawSixCards
    {
        public Card Card1, Card2, Card3, Card4, Card5, Card6;

        public DrawSixCards(Card card1, Card card2, Card card3, Card card4, Card card5, Card card6)
        {
            Card1 = card1;
            Card2 = card2;
            Card3 = card3;
            Card4 = card4;
            Card5 = card5;
            Card6 = card6;
        }
    }
}
