using HandAndFoot.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandAndFoot.Messages.ToServer
{
    [Serializable]
    public class PlayCard : IServerMessage
    {
        public Card Card;
        public int BookId;

        public PlayCard(Card card, int bookId)
        {
            Card = card;
            BookId = bookId;
        }
    }
}
