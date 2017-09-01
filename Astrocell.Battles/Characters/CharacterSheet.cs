using Astrocell.Battles.Decks;

namespace Astrocell.Battles.Characters
{
    public sealed class CharacterSheet
    {
        public string Name { get; }
        public ICharStats Stats { get; }
        public EquipmentSheet Equipment { get; }
        public EquippedDeck Deck { get; }

        public CharacterSheet(string name, ICharStats stats, EquipmentSheet equip, EquippedDeck deck)
        {
            Name = name;
            Stats = stats;
            Equipment = equip;
            Deck = deck;
        }
    }

    public interface ICharStats : ICharIntrinsicStats, ICharExtrinsicStats { }

    public struct CurrentStats : ICharStats
    {
        int ICharIntrinsicStats.this[Intrinsic stat] => _intrinsic[stat];
        int ICharExtrinsicStats.this[Extrinsic stat] => _extrinsic[stat];

        private readonly ICharIntrinsicStats _intrinsic;
        private readonly ICharExtrinsicStats _extrinsic;

        public CurrentStats(ICharIntrinsicStats intrinsic, ICharExtrinsicStats extrinsic)
        {
            _intrinsic = intrinsic;
            _extrinsic = extrinsic;
        }
    }
}
