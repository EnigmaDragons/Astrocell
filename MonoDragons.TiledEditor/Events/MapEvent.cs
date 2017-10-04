using System.Collections.Generic;
using MonoDragons.TiledEditor.Maps;

namespace MonoDragons.TiledEditor.Events
{
    public class MapEvent
    {
        public string TypeName { get; set; }
        public Dictionary<string, string> Details { get; set; }
        public TilePosition Position { get; set; }
    }
}
