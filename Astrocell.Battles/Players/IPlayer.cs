using Astrocell.Battles.Battles;
using Astrocell.Battles.Decks;
using System;
using System.Collections.Generic;

namespace Astrocell.Battles.Players
{
    public interface IPlayer
    {
        void SelectAction(BattleCharacter src, IList<Card> cards, BattleCharacters characters, Action<CardAction> onSelected);
    }
}
