using System;
using Astrocell.Battles.Battles;
using Astrocell.Battles.Decks;

namespace Astrocell.Battles.BattlePresentation
{
    public interface IBattlePresenter
    {
        void ShowBattleBegan(Battle battle, Action callback);
        void ShowTurnBegan(BattleCharacter character, Action callback);
        void ShowPlayedCard(BattleCharacter character, Card card, Action callback);
        void ShowTurnEnded(BattleCharacter character, Action callback);
        void ShowBattleEnded(Battle battle, Action callback);
    }
}
