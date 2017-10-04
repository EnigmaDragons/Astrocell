using System;
using Astrocell.Battles.Battles;
using Astrocell.Battles.Decks;

namespace Astrocell.Battles.Players
{
    public class DeadPlayer : IPlayer
    {
        public void SelectAction(BattleCharacter src, BattleHand hand, BattleCharacters characters, Action<CardAction> onSelected)
        {
            throw new NotImplementedException("I'm not alive. I will never pick a card. Not any card!");
        }
    }
}
