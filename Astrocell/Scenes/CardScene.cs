using System.Collections.Generic;
using Astrocell.Battles;
using Astrocell.Battles.Decks;
using MonoDragons.Core.Entities;
using MonoDragons.Core.Scenes;
using MonoDragons.Core.Common;
using Microsoft.Xna.Framework;

namespace Astrocell.Scenes
{
    public sealed class CardScene : EcsScene
    {
        protected override IEnumerable<GameObject> CreateObjs()
        {
            return CardDisplay.Create(Card.Load("strike"), Vector2.Zero, true).AsList();
        }
    }
}
