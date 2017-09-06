using System.Collections.Generic;
using Astrocell.Battles;
using Astrocell.Battles.Decks;
using MonoDragons.Core.Entities;
using MonoDragons.Core.Scenes;

namespace Astrocell.Scenes
{
    public sealed class CardScene : EcsScene
    {
        protected override IEnumerable<GameObject> CreateObjs()
        {
            yield return CardDisplay.Create(Card.Load("strike"));
            yield return CardDisplay.Create(Card.Load("enrage"));
            yield return CardDisplay.Create(Card.Load("stunningbolt"));
            yield return CardDisplay.Create(Card.Load("shakeitoff"));
        }
    }
}
