using HandAndFoot.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandAndFoot.Messages.ToClient
{
    [Serializable]
    public class UpdateTeam : IClientMessage
    {
        public bool MyTeam;
        public Book[] Books;

        public UpdateTeam(bool myTeam, Book[] books)
        {
            MyTeam = myTeam;
            Books = books;
        }
    }
}
