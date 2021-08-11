using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    /// <summary>
    /// Creates a deck that can be used in various ways
    /// </summary>
    public class Deck<T>
    {
        private List<T> deckList = new List<T>();

        System.Random random = new System.Random();

        /// <summary>
        /// Count of cards in deck
        /// </summary>
        public int Count
        {
            get
            {
                return deckList.Count;
            }
        }

        /// <summary>
        /// Returns the first card in the deck
        /// </summary>
        /// <returns></returns>
        public T DrawCard()
        {
            if(deckList.Count > 0)
            {
                return deckList[0];
            }
            throw new System.Exception("Deck Error: Attempting to draw card from empty deck.");          
        }

        /// <summary>
        /// Returns the first card in the deck AND removes it
        /// </summary>
        /// <returns></returns>
        public T DestructiveDraw()
        {
            T value = DrawCard();
            deckList.RemoveAt(0);
            return value;
        }

        /// <summary>
        /// Adds a card to deck
        /// DUPLICATES ALLOWED
        /// </summary>
        /// <param name="card"></param>
        public void AddCard(T card)
        {
            deckList.Add(card);
        }

        /// <summary>
        /// Removes a card to deck
        /// </summary>
        /// <param name="card"></param>
        public void RemoveCard(T card)
        {
            if (deckList.Contains(card))
            {
                deckList.Remove(card);
            }
            else
            {
                throw new System.Exception("Deck Error: Tried to remove card that is not in deck: " + card);
            }           
        }

        /// <summary>
        /// Removes all cards in that match given deck.
        /// (Deck allows duplicates but this is useful in certain cases)
        /// </summary>
        /// <param name="card"></param>
        public void RemoveAllOfCard(T card)
        {
            while (deckList.Contains(card))
            {
                deckList.Remove(card);
            }
        }

        /// <summary>
        /// Shuffles the deck using Fisher-Yates
        /// </summary>
        public void ShuffleDeck()
        {
            int n = deckList.Count;
            while(n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = deckList[k];
                deckList[k] = deckList[n];
                deckList[n] = value;
            }
        }
    }
}

