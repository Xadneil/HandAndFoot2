using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandAndFoot.Messages.ToServer
{
    [Serializable]
    public class SelectTeam
    {
        public int Team;

        public SelectTeam(int team)
        {
            Team = team;
        }
    }
}
