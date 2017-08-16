using System.Collections.Generic;
using Astrocell.Battles.Decks;

namespace Astrocell.Battles.Battles
{
    public sealed class BattleHand
    {
        public IList<Card> Cards { get; } = new List<Card>();

        public void Add(Card card)
        {
            Cards.Add(card);
        }

        public Card Take(Card card)
        {
            Cards.Remove(card);
            return card;
        }
    }
}
