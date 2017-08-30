using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonoDragons.Core.Common;
using MonoDragons.Core.Engine;

namespace Astrocell.Battles.Decks
{
    [TestClass]
    public class DeckTests
    {
        [TestMethod]
        public void Deck_Save_CanLoad()
        {
            var deck = new DeckDefinition {Name = "AllStrikes", CardCounts = new Map<string, int> {{"strike", 45}}};
            deck.SaveAs("AllStrikes");

            var loaded = DeckDefinition.Load("AllStrikes");

            Assert.AreEqual("AllStrikes", loaded.Name);
            Assert.AreEqual(45, loaded.CardCounts["strike"]);
        }

        [TestMethod]
        public void Deck_ElectricianDeck_CanLoad()
        {
            EquippedDeckFactory.BuildElectricianDeck();
        }

        [TestMethod]
        public void Deck_DumbBruteDeck_CanLoad()
        {
            EquippedDeckFactory.BuildDumbBruteDeck();
        }
    }
}
