using System;
using Astrocell.Battles.Decks;

namespace Astrocell.Battles.BattlePresentation
{
    public interface IBattlePresenter
    {
        void ShowSelectedCard(Card card, Action continueWith);
    }
}
