using System.Collections.Generic;
using MonoDragons.Core.Common;

namespace Astrocell.Battles.Decks
{
    public struct EquippedDeck
    {
        public IEnumerable<Card> Cards { get; set; }

        public static EquippedDeck FromDeckDefinition(DeckDefinition def)
        {
            var cards = new List<Card>();
            foreach (var slot in def.CardCounts)
            {
                var card = Card.Load(slot.Key);
                slot.Value.PerformNTimes(() => cards.Add(card));
            }
            return new EquippedDeck {Cards = cards};
        }
    }
}
