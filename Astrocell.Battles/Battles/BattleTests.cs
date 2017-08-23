﻿using System;
using Astrocell.Battles.Characters;
using Astrocell.Battles.Decks;
using Astrocell.Battles.Players;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonoDragons.Core.Common;
using MonoDragons.Core.Logs;

namespace Astrocell.Battles.Battles
{
    [TestClass]
    public class BattleTests
    {
        private readonly CharacterBuilder _builder = new CharacterBuilder();

        [TestInitialize]
        public void Init()
        {
            BattleLog.Instance = new DebugLog();
            _builder.WithEquipment(EquipmentSheet.Empty)
                .WithDeck(EquippedDeckFactory.BuildWhackDeck());
        }

        private BattleCharacter CreateEnemy(StartingStats stats)
        {
            return Build(BattleSide.Enemy, stats);
        }

        private BattleCharacter CreateHero(StartingStats stats)
        {
            return Build(BattleSide.Gamer, stats);
        }

        private BattleCharacter Build(BattleSide loyalty, StartingStats stats)
        {
            return BattleCharacter.Init(loyalty,
                _builder.WithName(Guid.NewGuid().ToString().Substring(0, 7))
                       .WithStats(stats)
                       .Build());
        }

        [TestMethod]
        public void Battle_Create_CharactersAreInInitiativeOrder()
        {
            var slowChar = CreateHero(new StartingStats {Agility = 6});
            var fastChar = CreateEnemy(new StartingStats {Agility = 18});

            var battle = Battle.Create(new DeadPlayer(), new DeadPlayer(), slowChar, fastChar);

            Assert.AreEqual(fastChar, battle.TurnOrder[0]);
            Assert.AreEqual(slowChar, battle.TurnOrder[1]);
        }

        [TestMethod]
        public void Battle_Create_CharactersHasDrawnTheirStartingHands()
        {
            var slowChar = CreateHero(new StartingStats { Agility = 6 });
            var fastChar = CreateEnemy(new StartingStats { Agility = 18 });

            var battle = Battle.Create(new DeadPlayer(), new DeadPlayer(), slowChar, fastChar);

            battle.TurnOrder.Items.ForEach(x =>
                Assert.AreNotEqual(0, x.Hand.Cards));
        }

        [TestMethod]
        public void Battle_Resolve_CanResolveBattle()
        {
            var hero = CreateHero(new StartingStats
                {
                    Agility = 18,
                    Intelligence = 6,
                    Toughness = 6,
                    Strength = 12,
                    Willpower = 6,
                });
            var enemy = CreateEnemy(new StartingStats
                {
                    Agility = 18,
                    Intelligence = 6,
                    Toughness = 6,
                    Strength = 6,
                    Willpower = 6,
                });

            var log = new DebugLog();
            var battle = Battle.Create(log, 
                new WithLogging(log, new FirstValidCardPlayer()), 
                new WithLogging(log, new FirstValidCardPlayer()), hero, enemy);

            var result = battle.Resolve();
            Assert.AreEqual(BattleSide.Gamer, result);
        }
    }
}
