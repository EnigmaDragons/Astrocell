using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Entities;
using MonoDragons.Core.KeyboardControls;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Render.Viewports;
using MonoDragons.Core.Scenes;
using MonoDragons.Core.Tiled;
using MonoDragons.Core.Tiled.TmxLoading;

namespace Astrocell.Scenes
{
    public class Large : EcsScene
    {
        protected override IEnumerable<GameObject> CreateObjs()
        {
            var player = new OrthographicMovingObjectFactory()
                .CreateMovingObject(Tsx.Create(Path.Combine("Characters", "Gareth.tsx")), new Vector2(48 * 5, 48 * 8), new ZIndex(3))
                .Add(new TopDownMovement { Speed = 0.2f });
            yield return player;
            var cameraPosition = Transform2.CameraZero;
            cameraPosition.Center = player.World.Center - new Vector2(800, 450);
            yield return Entity
                .Create(cameraPosition)
                .Add(new Camera())
                .AttachTo(player);
            foreach (var tile in new OrthographicTileMapFactory().CreateMap(Tmx.Create(Path.Combine("Maps", "Large.tmx"))))
                yield return tile;
        }
    }
}

