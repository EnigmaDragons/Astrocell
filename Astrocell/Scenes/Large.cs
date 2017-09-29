using System.Collections.Generic;
using System.IO;
using Astrocell.Maps;
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
        private readonly PlayerLocation _player;

        public Large(PlayerLocation player)
        {
            _player = player;
        }

        protected override IEnumerable<GameObject> CreateObjs()
        {
            var player = new OrthographicMovingObjectFactory()
                .CreateMovingObject(Tsx.Create(Path.Combine("Characters", "Gareth.tsx")), _player.Transform.Location, new ZIndex(3))
                .Add(new TopDownMovement { Speed = 0.2f });
            PlayerLocation.Current = new PlayerLocation { MapName = GetType().Name, Transform = player.World };

            yield return player;
            var cameraPosition = Transform2.CameraZero;
            cameraPosition.Center = player.World.Center - new Vector2(800, 450);
            yield return Entity
                .Create("Player Camera", cameraPosition)
                .Add(new Camera())
                .AttachTo(player);
            foreach (var tile in new OrthographicTileMapFactory().CreateMap(Tmx.Create(Path.Combine("Maps", "Large.tmx"))))
                yield return tile;
        }
    }
}

