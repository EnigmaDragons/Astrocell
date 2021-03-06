﻿using System;
using Astrocell.Battles.Decks;
using MonoDragons.Core.Common;

namespace Astrocell.Battles.Characters
{
    public sealed class CharacterBuilder
    {
        private readonly Store<string> _name = new Store<string>(Rng.Int().ToString());
        private readonly Store<ICharIntrinsicStats> _stats = new Store<ICharIntrinsicStats>();
        private readonly Store<EquipmentSheet> _equip = new Store<EquipmentSheet>();
        private readonly Store<EquippedDeck> _deck = new Store<EquippedDeck>();

        public CharacterBuilder WithName(string name)
        {
            _name.Put(name);
            return this;
        }

        public CharacterBuilder WithStats(StartingStats stats)
        {
            if (!stats.IsValid())
                throw new ArgumentException("Starting stats are invalid");
            _stats.Put(stats);
            return this;
        }

        public CharacterBuilder WithStats(EnemyStartingStats stats)
        {
            _stats.Put(stats);
            return this;
        }

        public CharacterBuilder WithEquipment(EquipmentSheet equip)
        {
            _equip.Put(equip);
            return this;
        }

        public CharacterBuilder WithDeck(EquippedDeck deck)
        {
            _deck.Put(deck);
            return this;
        }

        public CharacterSheet Build()
        {
            if (_stats.IsEmpty || _equip.IsEmpty || _deck.IsEmpty)
                throw new InvalidOperationException("Cannot build character sheet without StartingStats, Equipment, and Deck.");

            var name = _name.Get();
            var stats = _stats.Get();
            var equipment = _equip.Get();
            var equipStatsMods = equipment.GetStatMods();
            var charExtrinsicStats = new ExtrinsicStatsFromBaseStats(stats);
            var modifiedExtrinsicStats = new CombinedExtrinsicStats(charExtrinsicStats, equipStatsMods);
            var currentStats = new CurrentStats(stats, modifiedExtrinsicStats);
            var charSheet = new CharacterSheet(name, currentStats, equipment, _deck.Get());

            SetupNextCharacter();
            return charSheet;
        }

        private void SetupNextCharacter()
        {
            _name.Put(Rng.Int().ToString());
        }
    }
}
