using System;
using Astrocell.Battles.Decks;

namespace Astrocell.Battles.BattlePresentation
{
    public sealed class NoPresenter : IBattlePresenter
    {
        public void ShowSelectedCard(Card card, Action continueWith)
        {
            continueWith();
        }
    }
}
