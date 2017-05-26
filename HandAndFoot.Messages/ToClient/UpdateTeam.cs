using HandAndFoot.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandAndFoot.Messages.ToClient
{
    [Serializable]
    public class UpdateTeam
    {
        public bool MyTeam;
        public List<Book> Books;

        public UpdateTeam(bool myTeam, List<Book> books)
        {
            MyTeam = myTeam;
            Books = books;
        }
    }
}
