using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandAndFoot.Messages.ToClient
{
    [Serializable]
    public class LobbyGameDetails
    {
        public int Teams, PlayersPerTeam;
        public string[][] PlayerTeamAllocation;

        public LobbyGameDetails(int teams, int playersPerTeam, string[][] playerTeamAllocation)
        {
            Teams = teams;
            PlayersPerTeam = playersPerTeam;
            PlayerTeamAllocation = playerTeamAllocation;
        }
    }
}
