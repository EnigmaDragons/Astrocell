using System.Collections.Generic;
using Astrocell.Battles.Battles;
using Astrocell.Battles.Decks;

namespace Astrocell.Battles.Players
{
    public interface IPlayer
    {
        CardAction SelectAction(IList<Card> cards, IList<BattleCharacter> characters);
    }
}
