using Astrocell.Battles.Decks;

namespace Astrocell.Battles.Characters
{
    public sealed class CharacterSheet
    {
        public ICharStats Stats { get; }
        public EquipmentSheet Equipment { get; }
        public EquippedDeck Deck { get; }

        public CharacterSheet(ICharStats stats, EquipmentSheet equip, EquippedDeck deck)
        {
            Stats = stats;
            Equipment = equip;
            Deck = deck;
        }
    }

    public interface ICharStats : ICharIntrinsicStats, ICharExtrinsicStats { }

    public struct CurrentStats : ICharStats
    {
        private readonly ICharIntrinsicStats _intrinsic;
        private readonly ICharExtrinsicStats _extrinsic;

        public CurrentStats(ICharIntrinsicStats intrinsic, ICharExtrinsicStats extrinsic)
        {
            _intrinsic = intrinsic;
            _extrinsic = extrinsic;
        }

        public int Level => _intrinsic.Level;
        public int Strength => _intrinsic.Strength;
        public int Agility => _intrinsic.Agility;
        public int Toughness => _intrinsic.Toughness;
        public int Willpower => _intrinsic.Willpower;
        public int Intelligence => _intrinsic.Intelligence;

        public int MaxHp => _extrinsic.MaxHp;
        public int Attack => _extrinsic.Attack;
        public int Magic => _extrinsic.Magic;
        public int Resistance => _extrinsic.Resistance;
        public int Defense => _extrinsic.Defense;
        public int Draw => _extrinsic.Draw;
        public int ActionPoints => _extrinsic.ActionPoints;
        public int StartingEnergy => _extrinsic.StartingEnergy;
        public int StartingCards => _extrinsic.StartingCards;
        public float CriticalChance => _extrinsic.CriticalChance;
        public float CriticalDamageFactor => _extrinsic.CriticalDamageFactor;
    }
}
