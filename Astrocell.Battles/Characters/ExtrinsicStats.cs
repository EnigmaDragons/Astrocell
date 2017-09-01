
namespace Astrocell.Battles.Characters
{
    public enum Extrinsic
    {
        MaxHp,
        Attack,
        Magic,
        Resistance,
        Defense,
        Draw,
        ActionPoints,
        StartingCards,
        StartingEnergy,
        EnergyGain,
    }

    public interface ICharExtrinsicStats
    {
        int this[Extrinsic stat] { get; }
    }
    
    public struct ExtrinsicStatsFromBaseStats : ICharExtrinsicStats
    {
        public int this[Extrinsic stat] => this.GetStatValue(stat);
        
        private readonly ICharIntrinsicStats _char;

        public int MaxHp => _char[Intrinsic.Toughness] * 3 + _char[Intrinsic.Strength];
        public int Attack => _char[Intrinsic.Strength];
        public int Magic => _char[Intrinsic.Willpower];
        public int Resistance => _char[Intrinsic.Toughness] / 6;
        public int Defense => _char[Intrinsic.Toughness] / 6;
        public int Draw => _char[Intrinsic.Intelligence] / 6;
        public int ActionPoints => 1 + _char[Intrinsic.Agility] / 6;
        public int StartingCards => 2;
        public int StartingEnergy => _char[Intrinsic.Willpower] / 6;
        public int EnergyGain => _char[Intrinsic.Willpower] / 6;

        public ExtrinsicStatsFromBaseStats(ICharIntrinsicStats charStats)
        {
            _char = charStats;
        }
    }

    public struct CombinedExtrinsicStats : ICharExtrinsicStats
    {
        public int this[Extrinsic stat] => _stats1[stat] + _stats2[stat];

        private readonly ICharExtrinsicStats _stats1;
        private readonly ICharExtrinsicStats _stats2;
        
        public CombinedExtrinsicStats(ICharExtrinsicStats stats1, ICharExtrinsicStats stats2)
        {
            _stats1 = stats1;
            _stats2 = stats2;
        }
    }

    public struct ExtrinsicStatsMods : ICharExtrinsicStats
    {
        public int this[Extrinsic stat] => this.GetStatValue(stat);

        public int MaxHp { get; set; }
        public int Attack { get; set; }
        public int Magic { get; set; }
        public int Resistance { get; set; }
        public int Defense { get; set; }
        public int Draw { get; set; }
        public int ActionPoints { get; set; }
        public int StartingEnergy { get; set; }
        public int StartingCards { get; set; }
        public int EnergyGain { get; set; }
    }
}
