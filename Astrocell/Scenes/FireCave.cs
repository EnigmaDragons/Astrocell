using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Entities;
using MonoDragons.Core.KeyboardControls;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Scenes;
using MonoDragons.Core.Tiled.Orthographic;
using MonoDragons.Core.Tiled.TmxLoading;
using Texture = MonoDragons.Core.Render.Texture;

namespace Astrocell.Scenes
{
    public class FireCave : EcsScene
    {
        protected override IEnumerable<GameObject> CreateObjs()
        {
            yield return Entity.Create(new Transform2(new Rectangle(0, 0, 48, 48), new ZIndex(3)))
                .Add((o, r) => new Texture(r.LoadTexture("sprites/gareth-mapchar.png", o), new Rectangle(48, 0, 48, 48)))
                .Add(new Motion2(new Velocity2()))
                .Add(new TopDownMovement { Speed = 1 });
            foreach (var tile in new TileMapFactory().CreateMap(Tmx.Create(Path.Combine("Maps", "FireCave.tmx"))))
                yield return tile;
        }
    }
}
