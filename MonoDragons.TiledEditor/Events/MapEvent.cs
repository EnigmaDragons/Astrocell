using System.Collections.Generic;
using MonoDragons.Core.PhysicsEngine;

namespace MonoDragons.TiledEditor.Events
{
    public class MapEvent
    {
        public string TypeName { get; set; }
        public Dictionary<string, string> Details { get; set; }
        public Transform2 Position { get; set; }
    }
}
