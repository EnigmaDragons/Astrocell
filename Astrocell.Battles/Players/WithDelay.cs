using System.Collections.Generic;
using Astrocell.Battles.Battles;
using Astrocell.Battles.Decks;
using System.Threading;
using System;

namespace Astrocell.Battles.Players
{
    public sealed class WithDelay : IPlayer
    {
        private readonly TimeSpan _delay;
        private readonly IPlayer _player;

        public WithDelay(TimeSpan delay, IPlayer player)
        {
            _player = player;
            _delay = delay;
        }

        public CardAction SelectAction(BattleCharacter src, IList<Card> cards, BattleCharacters characters)
        {
            var action = _player.SelectAction(src, cards, characters);
            Thread.Sleep(_delay);
            return action;
        }
    }
}
