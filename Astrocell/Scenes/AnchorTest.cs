using System.Collections.Generic;
using Astrocell.Battles;
using Astrocell.Battles.Decks;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Entities;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Render;
using MonoDragons.Core.Scenes;

namespace Astrocell.Scenes
{
    public class AnchorTest : EcsScene
    {
        protected override IEnumerable<GameObject> CreateObjs()
        {
            var card = CardDisplay.Create(Card.Load("strike"));
            var face = Entity.Create(new Transform2 {Size = new Size2(200, 200), ZIndex = 10})
                .Add((o, r) => new Texture(r.LoadTexture("Sprites/gareth-face.png", o)))
                .AnchorTo(card.Transform, new Transform2 { Location = new Vector2(0, 100), Size = new Size2(0, -100)});
            yield return card;
            yield return face;
        }
    }
}
