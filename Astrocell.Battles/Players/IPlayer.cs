using Astrocell.Battles.Battles;
using Astrocell.Battles.Decks;
using System;

namespace Astrocell.Battles.Players
{
    public interface IPlayer
    {
        void SelectAction(BattleCharacter src, BattleHand hand, BattleCharacters characters, Action<CardAction> onSelected);
    }
}
