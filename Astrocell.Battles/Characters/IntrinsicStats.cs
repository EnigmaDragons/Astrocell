namespace Astrocell.Battles.Characters
{
    public interface ICharIntrinsicStats
    {
        int Level { get; }
        int Strength { get; }
        int Agility { get; }
        int Toughness { get; }
        int Willpower { get; }
        int Intelligence { get; }
    }

    public struct StartingStats : ICharIntrinsicStats
    {
        public int Level { get; set; }
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Toughness { get; set; }
        public int Willpower { get; set; }
        public int Intelligence { get; set; }
    }

    public struct CombinedIntrinsicStats : ICharIntrinsicStats
    {
        private readonly ICharIntrinsicStats _stats1;
        private readonly ICharIntrinsicStats _stats2;

        public int Level => _stats1.Level + _stats2.Level;
        public int Strength => _stats1.Strength + _stats2.Strength;
        public int Agility => _stats1.Agility + _stats2.Agility;
        public int Toughness => _stats1.Toughness + _stats2.Toughness;
        public int Willpower => _stats1.Willpower + _stats2.Willpower;
        public int Intelligence => _stats1.Intelligence + _stats2.Intelligence;

        public CombinedIntrinsicStats(ICharIntrinsicStats stats1, ICharIntrinsicStats stats2)
        {
            _stats1 = stats1;
            _stats2 = stats2;
        }
    }
}
