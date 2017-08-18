using System;
using Astrocell.Battles.Battles;
using Astrocell.Battles.Decks;

namespace Astrocell.Battles.Effects
{
    public static class BattleEffect
    {
        public static IBattleEffect Create(BattleCharacter source, CardEffect effect)
        {
            if (effect.Type == EffectType.Damage)
                return new DamageEffect(Convert.ToInt32(Math.Ceiling(source.GetStat(effect.Stat) * effect.Factor)));
            return new NoEffect();
        }
    }
}
