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

        [TestMethod]
        [ExpectedException(typeof(WildNaturalBalanceException))]
        public void WildBalance0()
        {
            var test = new Book(new Card[]
            {
                new Card(Rank.TWO, Suit.CLUBS),
                new Card(Rank.TWO, Suit.CLUBS),
                Card.Joker()
            });
        }

        [TestMethod]
        [ExpectedException(typeof(WildNaturalBalanceException))]
        public void WildBalance1()
        {
            var test = new Book(new Card[]
            {
                new Card(Rank.ACE, Suit.CLUBS),
                new Card(Rank.TWO, Suit.CLUBS),
                Card.Joker()
            });
        }

        [TestMethod]
        [ExpectedException(typeof(WildNaturalBalanceException))]
        public void WildBalance2()
        {
            var test = new Book(new Card[]
            {
                new Card(Rank.ACE, Suit.CLUBS),
                new Card(Rank.ACE, Suit.CLUBS),
                new Card(Rank.TWO, Suit.CLUBS),
                new Card(Rank.TWO, Suit.CLUBS),
                Card.Joker()
            });
        }

        [TestMethod]
        [ExpectedException(typeof(WildNaturalBalanceException))]
        public void WildBalance3()
        {
            var test = new Book(new Card[]
            {
                Card.Joker(),
                new Card(Rank.ACE, Suit.CLUBS),
                new Card(Rank.ACE, Suit.DIAMONDS)
            });
            test.Add(Card.Joker());
        }

        [TestMethod]
        public void BookCapacity1()
        {
            // eight cards should be OK in a clean book
            var test = new Book(new Card[]
            {
                new Card(Rank.ACE, Suit.CLUBS),
                new Card(Rank.ACE, Suit.DIAMONDS),
                new Card(Rank.ACE, Suit.CLUBS),
                new Card(Rank.ACE, Suit.DIAMONDS),
                new Card(Rank.ACE, Suit.CLUBS),
                new Card(Rank.ACE, Suit.DIAMONDS),
                new Card(Rank.ACE, Suit.CLUBS),
                new Card(Rank.ACE, Suit.DIAMONDS)
            });
            Assert.AreEqual(8, test.CountNatural);
            Assert.AreEqual(8, test.Count);
            Assert.AreEqual(0, test.CountWild);
            Assert.IsFalse(test.IsDirty);
            Assert.IsTrue(test.IsComplete);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void BookCapacity2()
        {
            // eight cards should NOT be OK in a dirty book
            var test = new Book(new Card[]
            {
                new Card(Rank.ACE, Suit.CLUBS),
                new Card(Rank.ACE, Suit.DIAMONDS),
                new Card(Rank.ACE, Suit.CLUBS),
                new Card(Rank.ACE, Suit.DIAMONDS),
                new Card(Rank.ACE, Suit.CLUBS),
                new Card(Rank.ACE, Suit.DIAMONDS),
                new Card(Rank.ACE, Suit.CLUBS),
                Card.Joker()
            });
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void BookCapacity3()
        {
            var test = new Book(new Card[]
            {
                new Card(Rank.ACE, Suit.CLUBS),
                new Card(Rank.ACE, Suit.DIAMONDS),
                new Card(Rank.ACE, Suit.CLUBS),
                new Card(Rank.ACE, Suit.DIAMONDS),
                new Card(Rank.ACE, Suit.CLUBS),
                new Card(Rank.ACE, Suit.DIAMONDS),
                Card.Joker()
            });
            Assert.AreEqual(6, test.CountNatural);
            Assert.AreEqual(7, test.Count);
            Assert.AreEqual(1, test.CountWild);
            Assert.IsTrue(test.IsDirty);
            Assert.IsTrue(test.IsComplete);

            // eight cards should NOT be OK in a dirty book
            test.Add(new Card(Rank.ACE, Suit.SPADES));
        }

        [TestMethod]
        public void MainTest()
        {
            var test = new Book(new Card[]
            {
                new Card(Rank.ACE, Suit.CLUBS),
                new Card(Rank.ACE, Suit.CLUBS),
                new Card(Rank.ACE, Suit.DIAMONDS)
            });
            Assert.AreEqual(3, test.CountNatural);
            Assert.AreEqual(3, test.Count);
            Assert.AreEqual(0, test.CountWild);
            Assert.IsFalse(test.IsDirty);

            test.Add(new Card(Rank.TWO, Suit.DIAMONDS));
            Assert.AreEqual(3, test.CountNatural);
            Assert.AreEqual(4, test.Count);
            Assert.AreEqual(1, test.CountWild);
            Assert.IsTrue(test.IsDirty);

            test = new Book(new Card[]
            {
                Card.Joker(),
                new Card(Rank.ACE, Suit.CLUBS),
                new Card(Rank.ACE, Suit.DIAMONDS)
            });
            Assert.AreEqual(2, test.CountNatural);
            Assert.AreEqual(3, test.Count);
            Assert.AreEqual(1, test.CountWild);
            Assert.IsTrue(test.IsDirty);
        }
    }
}
