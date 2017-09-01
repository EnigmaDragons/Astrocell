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
        
        public static IBattleEffect Create(BattleCharacter src, CardEffect e)
        {
            var amount = src.GetStat(e.Stat).StatMultiplyBy(e.Factor);

            var fx = new List<IBattleEffect>();
            if (e.Type == EffectType.Damage && e.Stat == BattleStat.Attack)
                fx.Add(new PhysicalDamageEffect(amount));
            if (e.Type == EffectType.Damage && e.Stat == BattleStat.Magic)
                fx.Add(new MagicDamageEffect(amount));
            if (e.Type == EffectType.Heal)
                fx.Add(new HealEffect(amount));
            if (e.Status != CardStatusEffect.None)
                fx.Add(new DurationStatusEffect(e.Status.ToStatusEffect(), e.Duration));
            if (e.Type == EffectType.Buff)
                fx.Add(new DurationBuffEffect(e.Stat, e.Factor, e.Duration));
            if (fx.None())
                fx.Add(WithLogging(x => "No/Unknown Effect", new NoEffect()));
            return new CompositeEffect(fx);
        }

        private static IBattleEffect WithLogging(Func<BattleCharacter, string> description, IBattleEffect fx)
        {
            return new WithLogging(BattleLog.Instance, description, fx);
        }
    }
}
