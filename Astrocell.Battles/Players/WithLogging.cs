using System.Collections.Generic;
using Astrocell.Battles.Battles;
using Astrocell.Battles.Decks;

namespace Astrocell.Battles.Players
{
    public sealed class WithLogging : IPlayer
    {
        private readonly ILog _log;
        private readonly IPlayer _player;

        public WithLogging(ILog log, IPlayer player)
        {
            _log = log;
            _player = player;
        }

        public CardAction SelectAction(BattleCharacter forCharacter, IList<Card> cards, BattleCharacters allCharacters)
        {
            _log.Write($"{forCharacter.CurrentActionPoints}A, {forCharacter.CurrentEnergy}E: Selecting from {cards.CommaSeparated(x => x.Name)}");
            var action = _player.SelectAction(forCharacter, cards, allCharacters);
            _log.Write($"Selected {action.Card.Name}, targetting {action.Targets.CommaSeparated(x => x.Name)}");
            return action;
        }
    }
}
