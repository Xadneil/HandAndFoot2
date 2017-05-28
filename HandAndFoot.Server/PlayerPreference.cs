using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HandAndFoot.Server
{
    public class PlayerPreference
    {
        public string Name;
        public int? Team;
        public NetworkStream Stream;
        public int PlayerId;
    }
}
