using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandAndFoot.Core
{
    public class Team : ICollection<Book>
    {
        public Player[] Players;
        private List<Book> books;

        public Team(int playersPerTeam)
        {
            Players = new Player[playersPerTeam];
            for (int i = 0; i < playersPerTeam; i++)
            {
                Players[i] = new Player();
            }
            books = new List<Book>();
        }

        public int Count
        {
            get
            {
                return books.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public void Add(Book item)
        {
            books.Add(item);
        }

        public void Clear()
        {
            books.Clear();
        }

        public bool Contains(Book item)
        {
            return books.Contains(item);
        }

        public void CopyTo(Book[] array, int arrayIndex)
        {
            books.CopyTo(array, arrayIndex);
        }

        public IEnumerator<Book> GetEnumerator()
        {
            return books.GetEnumerator();
        }

        public bool Remove(Book item)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return books.GetEnumerator();
        }
    }
}
