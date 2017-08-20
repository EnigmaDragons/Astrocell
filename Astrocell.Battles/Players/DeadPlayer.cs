using System;
using System.Collections.Generic;
using Astrocell.Battles.Battles;
using Astrocell.Battles.Decks;

namespace Astrocell.Battles.Players
{
    public sealed class DeadPlayer : IPlayer
    {
        public CardAction SelectAction(BattleCharacter forCharacter, IList<Card> cards, BattleCharacters allCharacters)
        {
            throw new InvalidOperationException("Player is dead and cannot act.");
        }
    }
}
