using HandAndFoot.Core;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HandAndFoot.Core.Test
{
    [TestClass]
    public class BookTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Empty()
        {
            var test = new Book(new Card[]
            {

            });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Two()
        {
            var test = new Book(new Card[]
            {
                new Card(Rank.ACE, Suit.CLUBS),
                new Card(Rank.ACE, Suit.CLUBS)
            });
        }
    }
}
