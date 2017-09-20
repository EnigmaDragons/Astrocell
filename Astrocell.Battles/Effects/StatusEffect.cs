using System.Collections.Generic;
using Astrocell.Battles.Decks;
using MonoDragons.Core.Common;

namespace Astrocell.Battles.Effects
{
    public enum StatusEffect
    {
        None,
        Stunned,
        LockedOn,
    }

    public static class CardStatusEffectConverter
    {
        static CardStatusEffectConverter()
        {
            Enums.ForEach<CardStatusEffect>(x => ToStatusEffect(x));
        }

        public static StatusEffect ToStatusEffect(this CardStatusEffect effect)
        {
            if (effect == CardStatusEffect.LockedOn)
                return StatusEffect.LockedOn;
            if (effect == CardStatusEffect.Stun)
                return StatusEffect.Stunned;
            if (effect == CardStatusEffect.None)
                return StatusEffect.None;
            throw new KeyNotFoundException($"No Status Effect found for {effect}");
        }
    }
}
