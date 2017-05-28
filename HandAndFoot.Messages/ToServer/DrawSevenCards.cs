using HandAndFoot.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandAndFoot.Messages.ToServer
{
    [Serializable]
    public class DrawSevenCards : IServerMessage
    {
        public Card Card1, Card2;

        public DrawSevenCards(Card card1, Card card2)
        {
            Card1 = card1;
            Card2 = card2;
        }
    }
}
