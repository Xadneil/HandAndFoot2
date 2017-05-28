using HandAndFoot.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandAndFoot.Messages.ToClient
{
    [Serializable]
    public class NewDiscard : IClientMessage
    {
        public Card Card;

        public NewDiscard(Card card)
        {
            Card = card;
        }
    }
}
