using System.Collections.Generic;
using System.IO;
using Astrocell.Battles;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Common;
using MonoDragons.Core.Entities;
using MonoDragons.Core.KeyboardControls;
using MonoDragons.Core.Navigation;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Render.Viewports;
using MonoDragons.Core.Scenes;
using MonoDragons.Core.Tiled;
using MonoDragons.Core.Tiled.TmxLoading;
using MonoDragons.TiledEditor.Events;
using MonoDragons.TiledEditor.Maps;

namespace Astrocell.Scenes
{
    public class FireCave : EcsScene
    {
        private readonly PlayerLocation _player;

        public FireCave(PlayerLocation player)
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
            foreach (var tile in new OrthographicTileMapFactory().CreateMap(Tmx.Create(Path.Combine("Maps", "FireCave.tmx"))))
                yield return tile;

            yield return Entity.Create("Start Battle", new Transform2(new TilePosition(3, 5, 48), new Size2(48 * 3, 10)))
                .Add(new Collision { IsBlocking = false })
                .Add(x => new BoxCollider(x.World))
                .Add(new StepTrigger())
                .Add(new OnCollision { Action = x => x.IfEquals(player, () => Navigate.To(BattleFactory.Create())) });

            foreach (var mapEvent in MapEventsFactory.Create(Path.Combine("Content", "Maps", "FireCave.events")).InstantiateEvents())
                yield return mapEvent;
        }
    }
}
