using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandAndFoot.Core
{
    public class Player
    {
        private Hand hand, foot;
        public readonly string Name;
        public int PlayerId;
        public Team Team;

        public Player(string name, Team team, int teamId, int numTeams, int playerId)
        {
            Name = name;
            Team = team;
            PlayerId = teamId + playerId * numTeams;
        }

        public Hand Hand
        {
            get
            {
                return hand == null || (!hand.Any() && foot.Any()) ? foot : hand;
            }
        }

        public bool IsInFoot
        {
            get
            {
                return hand == null || (!hand.Any() && foot.Any());
            }
        }

        public void DealCards(Hand hand, Hand foot)
        {
            this.hand = hand;
            this.foot = foot;
        }
    }
}
