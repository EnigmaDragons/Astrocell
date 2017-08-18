using System.Collections.Generic;
using System.Diagnostics;
using Astrocell.Battles.Battles;
using Astrocell.Battles.Decks;

namespace Astrocell.Battles.Players
{
    public sealed class WithLogging : IPlayer
    {
        private readonly IPlayer _player;

        public WithLogging(IPlayer player)
        {
            _player = player;
        }

        public CardAction SelectAction(IList<Card> cards, IList<BattleCharacter> characters)
        {
            var action = _player.SelectAction(cards, characters);
            Debug.WriteLine($"Selected {action.Card.Name}, targetting {action.Targets.CommaSeparated(x => x.Name)}");
            return action;
        }
    }
}
