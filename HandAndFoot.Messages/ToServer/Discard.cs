using HandAndFoot.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandAndFoot.Messages.ToServer
{
    [Serializable]
    public class Discard : IServerMessage
    {
        public Card Card;

        public Discard(Card card)
        {
            Card = card;
        }
    }
}
