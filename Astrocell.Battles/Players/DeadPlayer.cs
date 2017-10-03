using System;
using System.Collections.Generic;
using Astrocell.Battles.Battles;
using Astrocell.Battles.Decks;

namespace Astrocell.Battles.Players
{
    public sealed class DeadPlayer : IPlayer
    {
        public void SelectAction(BattleCharacter src, IList<Card> cards, BattleCharacters allCharacters, Action<CardAction> onCardSelected)
        {
            throw new InvalidOperationException("Player is dead and cannot act.");
        }
    }
}
