using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandAndFoot.Messages.ToClient
{
    [Serializable]
    public class NotAllowed : IClientMessage
    {
        public string Reason;

        public NotAllowed(string reason)
        {
            Reason = reason;
        }
    }
}
