using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandAndFoot.Messages.ToClient
{
    [Serializable]
    public class CurrentTurn
    {
        public string Name;
        public bool MyTurn;

        public CurrentTurn(string name, bool myTurn)
        {
            Name = name;
            MyTurn = myTurn;
        }
    }
}
