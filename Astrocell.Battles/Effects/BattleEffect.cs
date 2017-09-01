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
            var effects = new List<IBattleEffect>();
            var statAmount = source.GetStat(effect.Stat);
            var amount = Multiply(statAmount, effect.Factor);

            if (effect.Type == EffectType.Damage && effect.Stat == BattleStat.Attack)
                effects.Add(new PhysicalDamageEffect(amount));
            if (effect.Type == EffectType.Damage && effect.Stat == BattleStat.Magic)
                effects.Add(new MagicDamageEffect(amount));
            if (effect.Type == EffectType.Heal)
                effects.Add(new HealEffect(amount));
            if (effect.Status != CardStatusEffect.None)
                effects.Add(new DurationStatusEffect(effect.Status.ToStatusEffect(), effect.Duration));
            if (effects.None())
                effects.Add(WithLogging(x => "No/Unknown Effect", new NoEffect()));
            return new CompositeEffect(effects);
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
