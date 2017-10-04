using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Common;
using MonoDragons.Core.Entities;
using MonoDragons.Core.Navigation;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.TiledEditor.Maps;

namespace MonoDragons.TiledEditor.Events
{
    public class TeleportEvent : IMapEventType
    {
        private const string MapName = "map name";

        public string TypeName => "Teleport";

        public GameObject Instantiate(MapEvent mapEvent)
        {
            return Entity.Create("teleport", mapEvent.Position)
                .Add(new Collision { IsBlocking = false })
                .Add(x => new BoxCollider(x.World))
                .Add(new StepTrigger())
                .Add(new OnCollision { Action = _ => Navigate(mapEvent) });
        }

        private void Navigate(MapEvent mapEvent)
        {
            GameMap.NavigateTo(new PlayerLocation
            {
                MapName = mapEvent.Details[MapName],
                Transform = new Transform2(new Vector2(int.Parse(mapEvent.Details["x"]), int.Parse(mapEvent.Details["y"])))
            });
        }

        public MapEvent Create(TilePosition startPostion, Vector2 destination, string mapName)
        {
            return new MapEvent
            {
                TypeName = TypeName,
                Position = startPostion,
                Details = new Dictionary<string, string>
                {
                    { MapName, Path.GetFileNameWithoutExtension(mapName) },
                    { "x", destination.X.ToString() },
                    { "y", destination.Y.ToString() }
                }
            };
        }
    }
}
