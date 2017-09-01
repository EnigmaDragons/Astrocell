using System.Collections.Generic;
using System.Linq;
using Astrocell.Battles.Effects;
using MonoDragons.Core.Common;

namespace Astrocell.Battles.Battles
{
    public sealed class BattleCharacterStatusEffects
    {
        private readonly Map<StatusEffect, int> _counters = new Map<StatusEffect, int>();

        public IReadOnlyDictionary<StatusEffect, int> EffectCounters => _counters;
        public IList<StatusEffect> CurrentEFfects => _counters.Keys.ToList();

        public bool CanAct => !HasEffect(StatusEffect.Stunned);

        public void Apply(StatusEffect effect, int duration)
        {
            _counters[effect] = duration;
        }

        public void EndTurn()
        {
            _counters.Keys.ForEach(Decrement);
        }

        private void Decrement(StatusEffect effect)
        {
            if (_counters[effect] <= 1)
                _counters.Remove(effect);
            else
                _counters[effect] = _counters[effect] - 1;
        }

        private bool HasEffect(StatusEffect effect)
        {
            return _counters.ContainsKey(effect);
        }
    }
}
