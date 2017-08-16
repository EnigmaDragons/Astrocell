namespace Astrocell.Battles.Characters
{
    public interface ICharExtrinsicStats
    {
        int MaxHp { get; }
        int Attack { get; }
        int Magic { get; }
        int Resistance { get; }
        int Defense { get; }
        int Draw { get; }
        int ActionPoints { get; }
        int StartingEnergy { get; }
        int StartingCards { get; }
        float CriticalChance { get; }
        float CriticalDamageFactor { get; }
    }
    
    public struct ExtrinsicStatsFromBaseStats : ICharExtrinsicStats
    {
        private readonly ICharIntrinsicStats _char;

        public int MaxHp => _char.Toughness * 3 + _char.Strength;
        public int Draw => _char.Intelligence / 6;
        public int StartingCards => 2 + Draw;
        public int Attack => _char.Strength;
        public int Magic => _char.Willpower;
        public int Defense => _char.Toughness / 6;
        public int Resistance => _char.Toughness / 6;
        public int ActionPoints => 1 + _char.Agility / 6;
        public int StartingEnergy => _char.Willpower / 6;
        public float CriticalChance => (_char.Agility + _char.Intelligence) / 6f;
        public float CriticalDamageFactor => (_char.Agility + _char.Intelligence) / 10f;

        public ExtrinsicStatsFromBaseStats(ICharIntrinsicStats charStats)
        {
            _char = charStats;
        }
    }

    public struct CombinedExtrinsicStats : ICharExtrinsicStats
    {
        private readonly ICharExtrinsicStats _stats1;
        private readonly ICharExtrinsicStats _stats2;

        public int MaxHp => _stats1.MaxHp + _stats2.MaxHp;
        public int Attack => _stats1.Attack + _stats2.Attack;
        public int Magic => _stats1.Magic + _stats2.Magic;
        public int Resistance => _stats1.Resistance + _stats2.Resistance;
        public int Defense => _stats1.Defense + _stats2.Defense;
        public int Draw => _stats1.Draw + _stats2.Draw;
        public int ActionPoints => _stats1.ActionPoints + _stats2.ActionPoints;
        public int StartingEnergy => _stats1.StartingEnergy + _stats2.StartingEnergy;
        public int StartingCards => _stats1.StartingCards + _stats2.StartingCards;
        public float CriticalChance => _stats1.CriticalChance + _stats2.CriticalChance;
        public float CriticalDamageFactor => _stats1.CriticalDamageFactor * _stats2.CriticalDamageFactor;

        public CombinedExtrinsicStats(ICharExtrinsicStats stats1, ICharExtrinsicStats stats2)
        {
            _stats1 = stats1;
            _stats2 = stats2;
        }
    }

    public struct ExtrinsicStatsMods : ICharExtrinsicStats
    {
        public int MaxHp { get; set; }
        public int Attack { get; set; }
        public int Magic { get; set; }
        public int Resistance { get; set; }
        public int Defense { get; set; }
        public int Draw { get; set; }
        public int ActionPoints { get; set; }
        public int StartingEnergy { get; set; }
        public int StartingCards { get; set; }
        public float CriticalChance { get; set; }
        public float CriticalDamageFactor { get; set; }
    }
}
