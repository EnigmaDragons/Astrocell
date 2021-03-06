﻿using System;
using Astrocell.Battles.Battles;
using Astrocell.Battles.Decks;

namespace Astrocell.Battles.BattlePresentation
{
    public sealed class NoPresenter : IBattlePresenter
    {
        public void ShowBattleBegan(Battle battle, Action callback)
        {
            callback();
        }

        public void ShowTurnBegan(BattleCharacter character, Action callback)
        {
            callback();
        }

        public void ShowPlayedCard(BattleCharacter character, Card card, Action callback)
        {
            callback();
        }

        public void ShowTurnEnded(BattleCharacter character, Action callback)
        {
            callback();
        }

        public void ShowBattleEnded(Battle battle, Action callback)
        {
            callback();
        }
    }
}
