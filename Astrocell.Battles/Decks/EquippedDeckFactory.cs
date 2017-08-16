using System.Linq;

namespace Astrocell.Battles.Decks
{
    public static class EquippedDeckFactory
    {
        public static EquippedDeck BuildWhackDeck()
        {
            var card = Card.Load("strike");
            return new EquippedDeck { Cards = Enumerable.Range(0, 45).Select(x => card) };
        }
    }
}
