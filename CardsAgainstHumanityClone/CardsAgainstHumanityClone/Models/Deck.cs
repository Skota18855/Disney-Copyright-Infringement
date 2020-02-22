using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardsAgainstHumanityClone.Models
{
    public enum eReplaceType
    {
        Discard,
        Top, 
        Bottom,
        Shuffle
    }

    public class BlackDeck
    {
        // Represents all card that are a part of the deck 
        public BlackCard[] DeckCards { get; set; }
        // Represents all cards that are a part of the discard pile (if applicable)
        public BlackCard[] DiscardedCards { get; set; }
        
        /// <summary>
        /// Returns an array of BlackCard equal to the amount specified. Cards returned will be removed from the active deck.
        /// </summary>
        /// <param name="amount">The amount of cards to be drawn.</param>
        /// <returns></returns>
        public BlackCard[] DrawCards(int amount = 1)
        {
            if (amount > DeckCards.Length) throw new ArgumentException("Cannot pass in a value greater than the size of the current deck.");
            BlackCard[] result = new BlackCard[amount];

            for (int i = 0; i < amount; i++)
            {
                result[i] = DeckCards[i];
            }

            int deckSize = DeckCards.Length;
            BlackCard[] tempDeck = new BlackCard[deckSize - amount];

            for (int i = amount; i < DeckCards.Length; i++)
            {
                tempDeck[i - amount] = DeckCards[i];
            }

            DeckCards = tempDeck;

            return result;
        }

        /// <summary>
        /// Replaces the array of cards provided into either the main deck or discarded deck depending on what is specified.
        /// </summary>
        /// <param name="type">The type of replacement to use. Discard will place cards into the Discard deck, while the other types place into the main deck.</param>
        /// <param name="returnedCards">The array of cards being returned.</param>
        public void ReplaceCards(eReplaceType type, BlackCard[] returnedCards)
        {
            int length;
            BlackCard[] tempDeck;
            switch (type)
            {
                case eReplaceType.Discard:
                    length = DiscardedCards.Length + returnedCards.Length;
                    tempDeck = new BlackCard[length];
                    for (int i = 0; i < DiscardedCards.Length; i++)
                    {
                        tempDeck[i] = DiscardedCards[i];
                    }
                    for (int i = DiscardedCards.Length; i < length; i++)
                    {
                        tempDeck[i] = returnedCards[i - DiscardedCards.Length];
                    }
                    DiscardedCards = tempDeck;
                    break;
                case eReplaceType.Top:
                    length = DeckCards.Length + returnedCards.Length;
                    tempDeck = new BlackCard[length];

                    foreach(BlackCard card in DeckCards)
                    {
                        returnedCards.Append(card);
                    }
                    DeckCards = tempDeck;
                    break;
                case eReplaceType.Bottom:
                    foreach (BlackCard card in returnedCards)
                    {
                        DeckCards.Append(card);
                    }
                    break;
                case eReplaceType.Shuffle:
                    foreach(BlackCard card in returnedCards)
                    {
                        DeckCards.Append(card);
                    }
                    ShuffleCards();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Shuffles the main deck.
        /// </summary>
        /// <param name="withDiscard">Shuffle the deck after replacing all discarded cards. Good for if there are not enough cards to draw from.</param>
        public void ShuffleCards(bool withDiscard = false)
        {

        }

        /// <summary>
        /// Replaces the discarded cards back into the main deck.
        /// </summary>
        /// <param name="replaceType">The type of replacement for the discarded cards to be re entered into the main deck.</param>
        public void ReplaceDiscardedCards(eReplaceType replaceType = eReplaceType.Shuffle)
        {
            switch (replaceType)
            {
                case eReplaceType.Discard:
                    ReplaceCards(eReplaceType.Discard, DiscardedCards);
                    break;
                case eReplaceType.Top:
                    ReplaceCards(eReplaceType.Top, DiscardedCards);
                    break;
                case eReplaceType.Bottom:
                    ReplaceCards(eReplaceType.Bottom, DiscardedCards);
                    break;
                case eReplaceType.Shuffle:
                    ReplaceCards(eReplaceType.Shuffle, DiscardedCards);
                    break;
                default:
                    break;
            }
            DiscardedCards = new BlackCard[0];
        }
    }
    public class WhiteDeck
    {

    }
}
