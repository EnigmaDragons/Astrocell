using System.Collections.Generic;
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
            yield return new OrthographicMovingObjectFactory()
                .CreateMovingObject(Tsx.Create(Path.Combine("Characters", "Gareth.tsx")), new Vector2(48 * 5, 48 * 8), new ZIndex(3))
                .Add(new TopDownMovement {Speed = 0.2f});
            foreach (var tile in new OrthographicTileMapFactory().CreateMap(Tmx.Create(Path.Combine("Maps", "FireCave.tmx"))))
                yield return tile;
        }
    }
}
