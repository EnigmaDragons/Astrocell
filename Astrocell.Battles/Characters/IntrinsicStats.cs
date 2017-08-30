
namespace Astrocell.Battles.Characters
{
    public enum Intrinsic
    {
        Level,
        Strength,
        Agility,
        Toughness,
        Willpower,
        Intelligence,
    }

    public interface ICharIntrinsicStats
    {
        int this[Intrinsic stat] { get; }
    }

    public struct StartingStats : ICharIntrinsicStats
    {
        public int this[Intrinsic stat] => stat.Get(this);

        public int Level { get; set; }
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Toughness { get; set; }
        public int Willpower { get; set; }
        public int Intelligence { get; set; }

        // TODO: Evolve this to give specific error messages and validate min values
        public bool IsValid()
        {
            return Strength + Agility + Toughness + Willpower + Intelligence == (Level * 6) + 33;
        }
    }

    public class CombinedIntrinsicStats : ICharIntrinsicStats
    {
        public int this[Intrinsic stat] => _stats1[stat] + _stats2[stat];

        private readonly ICharIntrinsicStats _stats1;
        private readonly ICharIntrinsicStats _stats2;

        public CombinedIntrinsicStats(ICharIntrinsicStats stats1, ICharIntrinsicStats stats2)
        {
            _stats1 = stats1;
            _stats2 = stats2;
        }
    }
}
