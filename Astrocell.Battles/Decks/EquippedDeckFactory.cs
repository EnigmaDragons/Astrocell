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

        public static EquippedDeck Load(string name)
        {
            var deck = DeckDefinition.Load(name);
            return EquippedDeck.FromDeckDefinition(deck);
        }

        public static EquippedDeck BuildElectricianDeck()
        {
            return Load("electrician");
        }

        public static EquippedDeck BuildDumbBruteDeck()
        {
            return Load("dumbbrute");
        }
    }
}
