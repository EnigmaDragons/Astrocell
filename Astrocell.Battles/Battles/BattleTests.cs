using Astrocell.Battles.Characters;
using Astrocell.Battles.Decks;
using Astrocell.Battles.Players;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Astrocell.Battles.Battles
{
    [TestClass]
    public class BattleTests
    {
        [TestMethod]
        public void Battle_Create_CharactersAreInInitiativeOrder()
        {
            var slowChar = BattleCharacter.Init(BattleSide.Gamer, 
                new CharacterBuilder()
                    .WithStats(new StartingStats {Agility = 6})
                    .WithEquipment(EquipmentSheet.Empty)
                    .WithDeck(EquippedDeckFactory.BuildWhackDeck())
                    .Build());
            var fastChar = BattleCharacter.Init(BattleSide.Enemy, 
                new CharacterBuilder()
                    .WithStats(new StartingStats { Agility = 18 })
                    .WithEquipment(EquipmentSheet.Empty)
                    .WithDeck(EquippedDeckFactory.BuildWhackDeck())
                    .Build());

            var battle = Battle.Create(new DeadPlayer(), new DeadPlayer(), slowChar, fastChar);

            Assert.AreEqual(fastChar, battle.TurnOrder[0]);
            Assert.AreEqual(slowChar, battle.TurnOrder[1]);
        }

        [TestMethod]
        public void Battle_Create_CharactersHasDrawnTheirStartingHands()
        {
            var slowChar = BattleCharacter.Init(BattleSide.Gamer,
                new CharacterBuilder()
                    .WithStats(new StartingStats { Agility = 6 })
                    .WithEquipment(EquipmentSheet.Empty)
                    .WithDeck(EquippedDeckFactory.BuildWhackDeck())
                    .Build());
            var fastChar = BattleCharacter.Init(BattleSide.Enemy,
                new CharacterBuilder()
                    .WithStats(new StartingStats { Agility = 18 })
                    .WithEquipment(EquipmentSheet.Empty)
                    .WithDeck(EquippedDeckFactory.BuildWhackDeck())
                    .Build());

            var battle = Battle.Create(new DeadPlayer(), new DeadPlayer(), slowChar, fastChar);

            battle.TurnOrder.Items.ForEach(x =>
                Assert.AreNotEqual(0, x.Hand.Cards));
        }

        [TestMethod]
        public void Battle_Resolve_CanResolveBattle()
        {
            var slowChar = BattleCharacter.Init(BattleSide.Gamer,
                new CharacterBuilder()
                    .WithStats(new StartingStats
                    {
                        Agility = 18,
                        Intelligence = 6,
                        Toughness = 6,
                        Strength = 6,
                        Willpower = 6,
                    })
                    .WithEquipment(EquipmentSheet.Empty)
                    .WithDeck(EquippedDeckFactory.BuildWhackDeck())
                    .Build());
            var fastChar = BattleCharacter.Init(BattleSide.Enemy,
                new CharacterBuilder()
                    .WithStats(new StartingStats
                    {
                        Agility = 18,
                        Intelligence = 6,
                        Toughness = 6,
                        Strength = 6,
                        Willpower = 6,
                    })
                    .WithEquipment(EquipmentSheet.Empty)
                    .WithDeck(EquippedDeckFactory.BuildWhackDeck())
                    .Build());

            var battle = Battle.Create(new DeadPlayer(), new DeadPlayer(), slowChar, fastChar);

            battle.Resolve();
        }
    }
}
