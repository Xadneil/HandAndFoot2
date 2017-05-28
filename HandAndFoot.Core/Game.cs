using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandAndFoot.Core
{
    public class Game
    {
        public readonly Team[] Teams;
        public List<Card> DrawPile, DiscardPile;
        public Round Round;
        public int[] TeamPoints;
        private int cardsPerHand, decks;

        public Game(int teams, int playersPerTeam, int cardsPerHand, int decks, string[][] names)
        {
            this.cardsPerHand = cardsPerHand;
            this.decks = decks;
            Teams = new Team[teams];
            TeamPoints = new int[teams];
            for (int i = 0; i < teams; i++)
            {
                Teams[i] = new Team(playersPerTeam, names[i], i, teams);
                TeamPoints[i] = 0;
            }

            DiscardPile = new List<Card>();
            DrawPile = new List<Card>(54 * decks);
            for (int i = 0; i < decks; i++)
            {
                foreach (Suit suit in Enum.GetValues(typeof(Suit)))
                {
                    foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                    {
                        DrawPile.Add(new Card(rank, suit));
                    }
                }
                DrawPile.Add(Card.Joker());
                DrawPile.Add(Card.Joker());
            }

            Shuffle(DrawPile);

            Round = Round.ROUND1;

            foreach (Player player in Teams.SelectMany(t => t.Players))
            {
                player.DealCards(new Hand(DrawPile, cardsPerHand), new Hand(DrawPile, cardsPerHand));
            }
        }

        // shuffle the pile with the Fisher-Yates shuffle
        private static void Shuffle(List<Card> DrawPile)
        {
            var rng = new Random();
            int n = DrawPile.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Card value = DrawPile[k];
                DrawPile[k] = DrawPile[n];
                DrawPile[n] = value;
            }
        }

        private void Reset()
        {
            DiscardPile = new List<Card>();
            DrawPile = new List<Card>(54 * decks);
            for (int i = 0; i < decks; i++)
            {
                foreach (Suit suit in Enum.GetValues(typeof(Suit)))
                {
                    foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                    {
                        DrawPile.Add(new Card(rank, suit));
                    }
                }
                DrawPile.Add(Card.Joker());
                DrawPile.Add(Card.Joker());
            }

            Shuffle(DrawPile);

            foreach (Player player in Teams.SelectMany(t => t.Players))
            {
                player.DealCards(new Hand(DrawPile, cardsPerHand), new Hand(DrawPile, cardsPerHand));
            }

            foreach (Team team in Teams)
            {
                team.Clear();
            }
        }

        public void PlayOnBook(Player player, Card card, Book book)
        {
            if (!player.Hand.Remove(card))
                throw new InvalidOperationException($"The player does not have the reported card ({card.Rank + (card.Rank == Rank.JOKER ? "" : (" of " + card.Suit))}) in their hand.");
            book.Add(card);
        }

        public void PlayNewBook(Team team, Player player, Book book)
        {
            foreach (Card card in book)
            {
                if (!player.Hand.Remove(card))
                    throw new InvalidOperationException($"The player does not have the reported card ({card.Rank + (card.Rank == Rank.JOKER ? "" : (" of " + card.Suit))}) in their hand.");
            }
            team.Add(book);
        }

        public Card[] DrawTwoCards(Player player)
        {
            Card[] ret = new Card[2];
            for (int i = 0; i < 2; i++)
            {
                if (!DrawPile.Any())
                {
                    if (DiscardPile.Any())
                    {
                        DrawPile = DiscardPile;
                        DiscardPile = new List<Card>();
                        Shuffle(DrawPile);
                    }
                    else
                    {
                        throw new InvalidOperationException("There are not enough cards to draw.");
                    }
                }
                var card = DrawPile[DrawPile.Count - 1];
                DrawPile.RemoveAt(DrawPile.Count - 1);
                player.Hand.Add(card);
                ret[i] = card;
            }
            return ret;
        }

        public Card[] DrawSevenCardsFromDiscardAndPlayNewBook(Team team, Player player, Card card1, Card card2)
        {
            if (DiscardPile.Count < 7)
                throw new InvalidOperationException("There are not enough cards in the discard pile. It must have 7 cards.");

            if (!player.Hand.Remove(card1))
                throw new InvalidOperationException($"The player does not have the reported card ({card1.Rank + (card1.Rank == Rank.JOKER ? "" : (" of " + card1.Suit))}) in their hand.");
            if (!player.Hand.Remove(card2))
                throw new InvalidOperationException($"The player does not have the reported card ({card2.Rank + (card2.Rank == Rank.JOKER ? "" : (" of " + card2.Suit))}) in their hand.");

            Card[] ret = new Card[6];
            Card topDiscard = DiscardPile[DiscardPile.Count - 1];

            for (int i = 0; i < 7; i++)
            {
                var card = DiscardPile[DiscardPile.Count - 1];
                DiscardPile.RemoveAt(DiscardPile.Count - 1);
                if (i > 0)
                {
                    player.Hand.Add(card);
                    ret[i - 1] = card;
                }
            }

            team.Add(new Book(new Card[] { card1, card2, topDiscard }));
            return ret;
        }

        public void Discard(Team team, Player player, Card card)
        {
            if (player.Hand.Count == 1 && player.IsInFoot)
            {
                if (team.Count(b => b.IsDirty && b.IsComplete) < 2)
                    throw new InvalidOperationException("The player's team does not have enough complete dirty piles to go out.");
                if (team.Count(b => !b.IsDirty && b.IsComplete) < 2)
                    throw new InvalidOperationException("The player's team does not have enough complete clean piles to go out.");

                // The player is ending a round. Total and accumulate points, and reset teams for another round if applicable.
                foreach (var t in Teams.Select((x, i) => new { Team = x, Index = i }))
                {
                    TeamPoints[t.Index] += t.Team.Sum(b => b.Score());
                    TeamPoints[t.Index] -= t.Team.Players.SelectMany(p => p.Hand).Sum(c => c.Points());
                }

                switch (Round)
                {
                    case Round.ROUND1:
                        Round = Round.ROUND2;
                        Reset();
                        break;
                    case Round.ROUND2:
                        Round = Round.ROUND3;
                        Reset();
                        break;
                    case Round.ROUND3:
                        Round = Round.ROUND4;
                        Reset();
                        break;
                    case Round.ROUND4:
                        break;
                }
            }

            if (!player.Hand.Remove(card))
                throw new InvalidOperationException("The player does not have the reported card in their hand.");
            DiscardPile.Add(card);
        }
    }
}
