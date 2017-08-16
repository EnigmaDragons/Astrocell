using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }
    }
}
