﻿using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Entities;
using MonoDragons.Core.KeyboardControls;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Scenes;
using MonoDragons.Core.Tiled;
using MonoDragons.Core.Tiled.TmxLoading;

namespace Astrocell.Scenes
{
    public class FireCave : EcsScene
    {
        protected override IEnumerable<GameObject> CreateObjs()
        {
            var charTransform = new Transform2(new Rectangle(48 * 5, 48 * 8, 48, 48), new ZIndex(3));
            yield return new OrthographicCharacterFactory().CreateCharacter(Tsx.Create(Path.Combine("Characters", "Gareth.tsx")), new Vector2(48 * 5, 48 * 8))
                .Add(new Motion2(new Velocity2()))
                .Add(new TopDownMovement { Speed = 0.2f })
                .Add(new BoxCollider(charTransform));
            foreach (var tile in new OrthographicTileMapFactory().CreateMap(Tmx.Create(Path.Combine("Maps", "FireCave.tmx"))))
                yield return tile;
        }
    }
}
