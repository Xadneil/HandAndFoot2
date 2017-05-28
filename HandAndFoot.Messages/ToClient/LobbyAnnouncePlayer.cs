using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandAndFoot.Messages.ToClient
{
    [Serializable]
    public class LobbyAnnouncePlayer : IClientMessage
    {
        public string Name;

        public LobbyAnnouncePlayer(string name)
        {
            Name = name;
        }
    }
}
