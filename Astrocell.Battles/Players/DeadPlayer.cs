using System;
using System.Collections.Generic;
using Astrocell.Battles.Battles;
using Astrocell.Battles.Decks;

namespace Astrocell.Battles.Players
{
    public sealed class DeadPlayer : IPlayer
    {
        public CardAction SelectAction(IList<Card> cards, IList<BattleCharacter> characters)
        {
            throw new InvalidOperationException("Player is dead and cannot act.");
        }
    }
}
