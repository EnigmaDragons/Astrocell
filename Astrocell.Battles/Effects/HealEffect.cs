﻿using Astrocell.Battles.Battles;

namespace Astrocell.Battles.Effects
{
    public sealed class HealEffect : IBattleEffect
    {
        private readonly int _amount;

        public HealEffect(int amount)
        {
            _amount = amount;
        }

        public void ApplyTo(BattleCharacter target)
        {
            target.Heal(_amount);
        }
    }
}
