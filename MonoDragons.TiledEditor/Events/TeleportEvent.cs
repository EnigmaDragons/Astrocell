using System.Collections.Generic;
using MonoDragons.Core.Entities;
using MonoDragons.Core.PhysicsEngine;

namespace MonoDragons.TiledEditor.Events
{
    public class TeleportEvent : IMapEventType
    {
        private const string MapName = "map name";

        public string TypeName => "Teleport";

        public GameObject Instantiate(MapEvent mapEvent)
        {
            return Entity.Create("bob");
        }

        public MapEvent Create(Transform2 startPostion, Transform2 destination, string mapName)
        {
            return new MapEvent
            {
                TypeName = TypeName,
                Position = startPostion,
                Details = new Dictionary<string, string>
                {
                    { MapName, mapName },
                    { "x", destination.Location.X.ToString() },
                    { "y", destination.Location.Y.ToString() }
                }
            };
        }
    }
}
