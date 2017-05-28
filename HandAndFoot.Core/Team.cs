using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandAndFoot.Core
{
    public class Team : IList<Book>
    {
        public Player[] Players;
        private List<Book> books;
        public int TeamId;

        public Team(int playersPerTeam, IList<string> names, int teamId, int numTeams)
        {
            TeamId = teamId;
            Players = new Player[playersPerTeam];
            for (int i = 0; i < playersPerTeam; i++)
            {
                Players[i] = new Player(names[i], this, TeamId, numTeams, i);
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

        public Book this[int index]
        {
            get
            {
                return books[index];
            }

            set
            {
                throw new NotImplementedException();
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

        public int IndexOf(Book item)
        {
            return books.IndexOf(item);
        }

        public void Insert(int index, Book item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }
    }
}
