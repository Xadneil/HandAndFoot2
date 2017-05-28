using HandAndFoot.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandAndFoot.Messages.ToClient
{
    [Serializable]
    public class DrawTwoCards : IClientMessage
    {
        public Card Card1, Card2;

        public DrawTwoCards(Card card1, Card card2)
        {
            Card1 = card1;
            Card2 = card2;
        }
    }
}
