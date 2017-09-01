using System;
using System.Collections.Generic;
using Astrocell.Battles.Battles;
using Astrocell.Battles.Decks;
using MonoDragons.Core.Common;

namespace Astrocell.Battles.Effects
{
    public static class BattleEffect
    {
        public static void ApplyTo(this CardEffect cardEffect, BattleCharacter src, IList<BattleCharacter> targets)
        {
            var battleEffect = Create(src, cardEffect);
            targets.ForEach(x => battleEffect.ApplyTo(x));
        }

        //TODO: This is the wrong way to solve this
        public static IBattleEffect Create(BattleCharacter source, CardEffect effect)
        {
            var statAmount = source.GetStat(effect.Stat);
            var amount = Multiply(statAmount, effect.Factor);

            if (effect.Type == EffectType.Damage && effect.Stat == BattleStat.Attack)
                return new PhysicalDamageEffect(amount);
            if (effect.Type == EffectType.Damage && effect.Stat == BattleStat.Magic)
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
