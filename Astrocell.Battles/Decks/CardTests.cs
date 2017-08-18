using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Astrocell.Battles.Decks
{
    [TestClass]
    public class CardTests
    {
        [TestMethod]
        public void Card_Load_LoadedCorrectly()
        {
            var card = Card.Load("strike");

            Assert.AreEqual(2, card.ActionPointCost);
            Assert.AreEqual(EffectTarget.One, card.Effect.Target);
        }
    }
}
