﻿using System;
using Astrocell.Battles.Battles;
using MonoDragons.Core.Logs;

namespace Astrocell.Battles.Effects
{
    public struct WithLogging : IBattleEffect
    {
        private readonly ILog _log;
        private readonly Func<BattleCharacter, string> _description;
        private readonly IBattleEffect _effect;

        public WithLogging(ILog log, Func<BattleCharacter, string> description, IBattleEffect effect)
        {
            _log = log;
            _description = description;
            _effect = effect;
        }

        public void ApplyTo(BattleCharacter target)
        {
            _log.Write(_description(target));
            _effect.ApplyTo(target);
        }
    }
}
