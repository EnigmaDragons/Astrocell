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
            {
                var amount = Convert.ToInt32(Math.Ceiling(source.GetStat(effect.Stat) * effect.Factor));
                return new WithLogging(BattleLog.Instance, x => $"Attacks {x.Name} for {amount} {effect.Stat} damage.", new DamageEffect(amount));
            }
            return new NoEffect();
        }
    }
}
