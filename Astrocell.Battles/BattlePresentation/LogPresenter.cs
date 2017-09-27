using System;
using Astrocell.Battles.Battles;
using Astrocell.Battles.Decks;
using MonoDragons.Core.Common;
using MonoDragons.Core.Logs;

namespace Astrocell.Battles.BattlePresentation
{
    public sealed class LogPresenter : IBattlePresenter
    {
        private readonly ILog _log;

        public LogPresenter(ILog log)
        {
            _log = log;
        }

        public void ShowBattleBegan(Battle battle, Action callback)
        {
            _log.Write($"Began Battle with {battle.TurnOrder.Items.CommaSeparated(x => x.Name)}.");
            callback();
        }

        public void ShowTurnBegan(BattleCharacter character, Action callback)
        {
            _log.Write("");
            _log.Write($"Began turn for {character.Loyalty} {character.Name}.");
            callback();
        }

        public void ShowPlayedCard(BattleCharacter character, Card card, Action callback)
        {
            _log.Write($"{character.Name} plays {card.Name}");
            callback();
        }

        public void ShowTurnEnded(BattleCharacter character, Action callback)
        {
            _log.Write($"Ended turn for {character.Loyalty} {character.Name}.");
            callback();
        }

        public void ShowBattleEnded(Battle battle, Action callback)
        {
            _log.Write($"Winner: {battle.Winner}");
            _log.Write("");
            callback();
        }
    }
}
