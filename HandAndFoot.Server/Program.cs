using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandAndFoot.Server
{
    public class Program
    {
        public static void Main()
        {
            int numTeams = 2, playersPerTeam = 2, port = 9792;

            var prefs = Lobby.ConnectPlayers(numTeams, playersPerTeam, port);

            SocketServer.Initialize(new Core.Game(numTeams, playersPerTeam, 11, 5, prefs.GroupBy(p => p.Team).Select(g => g.Select(p => p.Name).ToArray()).ToArray()), prefs);
        }
    }
}
