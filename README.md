# Hand And Foot
Hand and Foot is a team playing-card game, similar to Rummy.

The goal is to score more points than the other team by building books of cards in the same rank,
and avoiding being caught with valuable cards in your hand at the end of a round.

# Status
This repository is under casual development by Xadneil only. Pull requests will not be accepted.

# Points and Rules
Twos (20 points) and Jokers (50 points) are wild. Aces are worth 50 points.
Black Threes are worth zero points and cannot be played in books, and red Threes are worth 500 points and cannot be played in books.
Cards 4-8 are worth 5 points and cards 9-K are worth 10 points.

There are four rounds, and each ends when a player runs out of cards after he discards.

Each player is dealt two hands of 11 cards. The second hand is not used until the first is exhausted.
This second hand is called the foot.

On each turn, the player must draw cards in one of two ways. The first and most common way is by drawing two cards from the draw pile.
If the top card of the dicard pile matches two natural cards of the same rank in the player's hand,
he may instead draw seven cards from the top of the discard pile and immediately play the three matching cards
(2 from his hand, 1 from the discard pile) as a new book. The seven card draw is not allowed if the team has not melded yet.

To end a player's turn, he discards one card onto the top of the discard pile.
If this discard, or any other play of a card exhausts a player's Hand, he can switch to his Foot.
If this discard exhausts a player's foot, the player's team must have completed at least two of each type of book.
If not, the player must undo enough such that he can discard and not run out of cards.

A book consists of at least three cards of the same rank, or wilds. All books must have more natural cards than wilds.

If a book has wilds in it, it is considered "dirty", otherwise it is "clean".
When a book contains seven cards, it is complete and is worth extra points.
Clean books are worth 500 points and dirty books are worth 300 points.
More natural cards may be added to a complete clean book, but not a complete dirty book.

To be allowed to begin playing books for your team, the team must meld.
This means that a player plays enough books in a single turn such that the total score of the cards in the books is
greater than or equal to the round's meld limit. These limits are 50, 90, 120, and 150 points.

At the end of each round, the sum of points of any cards remaining in the teammates' hands and feet
are subtracted from the sum of points of the cards in the team's books and the extra points from completed books.
