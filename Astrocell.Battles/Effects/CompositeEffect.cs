using System.Collections.Generic;
using Astrocell.Battles.Battles;
using MonoDragons.Core.Common;

namespace Astrocell.Battles.Effects
{
    public struct CompositeEffect : IBattleEffect
    {
        private readonly IEnumerable<IBattleEffect> _effects;

        public CompositeEffect(params IBattleEffect[] effects)
            : this ((IEnumerable<IBattleEffect>)effects) { }

        public CompositeEffect(IEnumerable<IBattleEffect> effects)
        {
            _effects = effects;
        }

        public void ApplyTo(BattleCharacter target)
        {
            _effects.ForEach(x => x.ApplyTo(target));
        }
    }
}
