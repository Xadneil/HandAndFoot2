using HandAndFoot.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandAndFoot.Messages.ToClient
{
    [Serializable]
    public class DealCards
    {
        public List<Card> Cards;

        public DealCards(List<Card> Cards)
        {
            this.Cards = Cards;
        }
    }
}
