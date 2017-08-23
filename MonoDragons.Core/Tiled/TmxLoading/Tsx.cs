using System.IO;
using System.Xml.Linq;

namespace MonoDragons.Core.Tiled.TmxLoading
{
    public class Tsx
    {
        public int FirstId { get; }
        public int TileWidth { get; }
        public int TileHeight { get; }
        public int Spacing { get; }
        public int TileCount { get; }
        public int Columns { get; }
        public string TileSource { get; }

        public Tsx(int firstId, string tsxPath)
        {
            FirstId = firstId;
            var doc = XDocument.Load($"Content/{tsxPath}");
            var tileset = doc.Element(XName.Get("tileset"));
            TileWidth = new XValue(tileset, "tilewidth").AsInt();
            TileHeight = new XValue(tileset, "tileheight").AsInt();
            Spacing = new XValue(tileset, "spacing").AsInt();
            TileCount = new XValue(tileset, "tilecount").AsInt();
            Columns = new XValue(tileset, "columns").AsInt();
            TileSource = Path.Combine("Content", new XValue(tileset.Element(XName.Get("image")), "source").AsString());
        }
    }
}
