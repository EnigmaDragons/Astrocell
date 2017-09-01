using Astrocell.Battles.Battles;

namespace Astrocell.Battles.Effects
{
    public struct DurationStatusEffect : IBattleEffect
    {
        private readonly StatusEffect _effect;
        private readonly int _durationTurns;

        public DurationStatusEffect(StatusEffect effect, int durationTurns)
        {
            _effect = effect;
            _durationTurns = durationTurns;
        }

        public void ApplyTo(BattleCharacter target)
        {
            target.ApplyStatusEffect(_effect, _durationTurns);
        }
    }
}
