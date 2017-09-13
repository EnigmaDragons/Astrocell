using System;
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
            var card = CardDisplay.Create(Card.Load("strike")).Add(new DurationTravel() { Duration = TimeSpan.FromSeconds(5), Target = new Transform2(new Vector2(500, 500), new Size2(200, 300))});
            var face = Entity.Create(new Transform2 {Size = new Size2(200, 200), ZIndex = 10})
                .Add((o, r) => new Texture(r.LoadTexture("Sprites/gareth-face.png", o)))
                .AttachTo(card.World);
            yield return card;
            yield return face;
        }
    }
}
