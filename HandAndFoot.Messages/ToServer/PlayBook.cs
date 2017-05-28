using HandAndFoot.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandAndFoot.Messages.ToServer
{
    [Serializable]
    public class PlayBook : IServerMessage
    {
        public Book Book;

        public PlayBook(Book book)
        {
            Book = book;
        }
    }
}
