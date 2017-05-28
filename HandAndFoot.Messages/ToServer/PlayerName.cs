using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandAndFoot.Messages.ToServer
{
    [Serializable]
    public class PlayerName
    {
        public string Name;

        public PlayerName(string name)
        {
            Name = name;
        }
    }
}
