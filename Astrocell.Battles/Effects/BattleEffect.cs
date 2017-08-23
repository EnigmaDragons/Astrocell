using System;
using Astrocell.Battles.Battles;
using Astrocell.Battles.Decks;

namespace Astrocell.Battles.Effects
{
    public static class BattleEffect
    {
        public static IBattleEffect Create(BattleCharacter source, CardEffect effect)
        {
            if (effect.Type == EffectType.Damage && effect.Stat == EffectStat.Attack)
            {
                var amount = Convert.ToInt32(Math.Ceiling(source.GetStat(effect.Stat) * effect.Factor));
                return new PhysicalDamageEffect(amount);
            }
            return WithLogging(x => "No/Unknown Effect", new NoEffect());
        }

        private static IBattleEffect WithLogging(Func<BattleCharacter, string> description, IBattleEffect fx)
        {
            return new WithLogging(BattleLog.Instance, description, fx);
        }
    }
}
