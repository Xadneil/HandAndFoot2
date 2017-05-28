using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandAndFoot.Messages.ToClient
{
    [Serializable]
    public class LobbyPlayerChoseTeam
    {
        public string Name;
        public int Team;

        public LobbyPlayerChoseTeam(string name, int team)
        {
            Name = name;
            Team = team;
        }
    }
}
