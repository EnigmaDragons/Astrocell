using Astrocell.Battles.Battles;

namespace Astrocell.Battles.Effects
{
    public sealed class DurationBuffEffect : IBattleEffect
    {
        private readonly BattleStat _stat;
        private readonly float _factor;
        private readonly int _duration;

        public DurationBuffEffect(BattleStat stat, float factor, int duration)
        {
            _stat = stat;
            _factor = factor;
            _duration = duration;
        }

        public void ApplyTo(BattleCharacter target)
        {
            target.ApplyBuff(_stat, _factor, _duration);
        }
    }
}
