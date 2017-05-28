using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HandAndFoot.Core;
using HandAndFoot.Client.Properties;

namespace HandAndFoot.Client
{
    public partial class CardControl : UserControl
    {
        Card Card;

        public CardControl(Card card)
        {
            Card = card;
            InitializeComponent();
            pnlCard.BackgroundImage = GetImageResource(Card);
        }

        private Bitmap GetImageResource(Card card)
        {
            switch (card.Suit)
            {
                case Suit.HEARTS:
                    switch (card.Rank)
                    {
                        case Rank.ACE:
                            return Resources.ace_of_hearts;
                        case Rank.TWO:
                            return Resources._2_of_hearts;
                        case Rank.THREE:
                            return Resources._3_of_hearts;
                        case Rank.FOUR:
                            return Resources._4_of_hearts;
                        case Rank.FIVE:
                            return Resources._5_of_hearts;
                        case Rank.SIX:
                            return Resources._6_of_hearts;
                        case Rank.SEVEN:
                            return Resources._7_of_hearts;
                        case Rank.EIGHT:
                            return Resources._8_of_hearts;
                        case Rank.NINE:
                            return Resources._9_of_hearts;
                        case Rank.TEN:
                            return Resources._9_of_hearts;
                        case Rank.JACK:
                            return Resources.jack_of_hearts;
                        case Rank.QUEEN:
                            return Resources.queen_of_hearts;
                        case Rank.KING:
                            return Resources.king_of_hearts;
                        case Rank.JOKER:
                            return Resources.joker;
                    }
                    break;
                case Suit.DIAMONDS:
                    switch (card.Rank)
                    {
                        case Rank.ACE:
                            return Resources.ace_of_diamonds;
                        case Rank.TWO:
                            return Resources._2_of_diamonds;
                        case Rank.THREE:
                            return Resources._3_of_diamonds;
                        case Rank.FOUR:
                            return Resources._4_of_diamonds;
                        case Rank.FIVE:
                            return Resources._5_of_diamonds;
                        case Rank.SIX:
                            return Resources._6_of_diamonds;
                        case Rank.SEVEN:
                            return Resources._7_of_diamonds;
                        case Rank.EIGHT:
                            return Resources._8_of_diamonds;
                        case Rank.NINE:
                            return Resources._9_of_diamonds;
                        case Rank.TEN:
                            return Resources._9_of_diamonds;
                        case Rank.JACK:
                            return Resources.jack_of_diamonds;
                        case Rank.QUEEN:
                            return Resources.queen_of_diamonds;
                        case Rank.KING:
                            return Resources.king_of_diamonds;
                    }
                    break;
                case Suit.CLUBS:
                    switch (card.Rank)
                    {
                        case Rank.ACE:
                            return Resources.ace_of_clubs;
                        case Rank.TWO:
                            return Resources._2_of_clubs;
                        case Rank.THREE:
                            return Resources._3_of_clubs;
                        case Rank.FOUR:
                            return Resources._4_of_clubs;
                        case Rank.FIVE:
                            return Resources._5_of_clubs;
                        case Rank.SIX:
                            return Resources._6_of_clubs;
                        case Rank.SEVEN:
                            return Resources._7_of_clubs;
                        case Rank.EIGHT:
                            return Resources._8_of_clubs;
                        case Rank.NINE:
                            return Resources._9_of_clubs;
                        case Rank.TEN:
                            return Resources._9_of_clubs;
                        case Rank.JACK:
                            return Resources.jack_of_clubs;
                        case Rank.QUEEN:
                            return Resources.queen_of_clubs;
                        case Rank.KING:
                            return Resources.king_of_clubs;
                    }
                    break;
                case Suit.SPADES:
                    switch (card.Rank)
                    {
                        case Rank.ACE:
                            return Resources.ace_of_spades;
                        case Rank.TWO:
                            return Resources._2_of_spades;
                        case Rank.THREE:
                            return Resources._3_of_spades;
                        case Rank.FOUR:
                            return Resources._4_of_spades;
                        case Rank.FIVE:
                            return Resources._5_of_spades;
                        case Rank.SIX:
                            return Resources._6_of_spades;
                        case Rank.SEVEN:
                            return Resources._7_of_spades;
                        case Rank.EIGHT:
                            return Resources._8_of_spades;
                        case Rank.NINE:
                            return Resources._9_of_spades;
                        case Rank.TEN:
                            return Resources._9_of_spades;
                        case Rank.JACK:
                            return Resources.jack_of_spades;
                        case Rank.QUEEN:
                            return Resources.queen_of_spades;
                        case Rank.KING:
                            return Resources.king_of_spades;
                    }
                    break;
            }
            throw new Exception("Invalid Card");
        }
    }
}
