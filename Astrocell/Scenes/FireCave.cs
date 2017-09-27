using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Entities;
using MonoDragons.Core.KeyboardControls;
using MonoDragons.Core.Navigation;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Render.Viewports;
using MonoDragons.Core.Scenes;
using MonoDragons.Core.Tiled;
using MonoDragons.Core.Tiled.TmxLoading;

namespace Astrocell.Scenes
{
    public class FireCave : EcsScene
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
            foreach (var tile in new OrthographicTileMapFactory().CreateMap(Tmx.Create(Path.Combine("Maps", "FireCave.tmx"))))
                yield return tile;
            var exit1 = new Transform2(new Vector2(48 * 7, 48 * 16), new Size2(48 * 3, 10));
            yield return Entity.Create(exit1)
                .Add(new Collision() { IsBlocking = false })
                .Add(new BoxCollider(exit1))
                .Add(new OnCollision
                {
                    Action = x =>
                    {
                        if (x.Equals(player))
                            Navigate.To("Large");
                    }
                })
                .Add(new StepTrigger());
        }
    }
}
