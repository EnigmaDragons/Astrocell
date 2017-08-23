using System;
using System.Collections.Generic;
using System.Linq;
using Astrocell.Battles.Decks;
using MonoDragons.Core.Common;

namespace Astrocell.Battles.Battles
{
    public sealed class BattleDeck
    {
        private readonly IList<Card> _cards;

        public static BattleDeck Create(IEnumerable<Card> cards)
        {
            return new BattleDeck(cards.Shuffled());
        }

        private BattleDeck(IList<Card> cards)
        {
            _cards = cards;
        }

        public Card Draw()
        {
            if (!_cards.Any())
                throw new InvalidOperationException("Cannot draw from an empty deck");

            var card = _cards[0];
            _cards.RemoveAt(0);
            return card;
        }
    }
}
