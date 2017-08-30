using System;
using Astrocell.Battles.Battles;
using Astrocell.Battles.Decks;

namespace Astrocell.Battles.Effects
{
    public static class BattleEffect
    {
        //TODO: This is the wrong way to solve this
        public static IBattleEffect Create(BattleCharacter source, CardEffect effect)
        {
            var statAmount = source.GetStat(effect.Stat);
            var amount = Multiply(statAmount, effect.Factor);

            if (effect.Type == EffectType.Damage && effect.Stat == EffectStat.Attack)
                return new PhysicalDamageEffect(amount);
            if (effect.Type == EffectType.Damage && effect.Stat == EffectStat.Magic)
                return new MagicDamageEffect(amount);
            if (effect.Type == EffectType.Heal)
                return new HealEffect(amount);
            return WithLogging(x => "No/Unknown Effect", new NoEffect());
        }

        private static int Multiply(int statAmount, float factor)
        {
            return Convert.ToInt32(Math.Ceiling(statAmount * factor));
        }

        private static IBattleEffect WithLogging(Func<BattleCharacter, string> description, IBattleEffect fx)
        {
            return new WithLogging(BattleLog.Instance, description, fx);
        }
    }
}
