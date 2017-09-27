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
            return CardDisplay.Create(Card.Load("strike"));
        }
    }
}
