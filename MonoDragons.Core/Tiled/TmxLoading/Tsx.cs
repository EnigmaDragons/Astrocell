using System.IO;
using System.Xml.Linq;

namespace MonoDragons.Core.Tiled.TmxLoading
{
    public struct Tsx
    {
        public int FirstId;
        public int TileWidth;
        public int TileHeight;
        public int Spacing;
        public int TileCount;
        public int Columns;
        public string TileSource;

        public static Tsx Create(int firstId, string tsxPath)
        {
            var doc = XDocument.Load(Path.Combine("Content", tsxPath));
            var tileset = doc.Element(XName.Get("tileset"));
            return new Tsx
            {
                FirstId = firstId,
                TileWidth = new XValue(tileset, "tilewidth").AsInt(),
                TileHeight = new XValue(tileset, "tileheight").AsInt(),
                Spacing = new XValue(tileset, "spacing").AsInt(),
                TileCount = new XValue(tileset, "tilecount").AsInt(),
                Columns = new XValue(tileset, "columns").AsInt(),
                TileSource = GetSourcePath(tileset.Element(XName.Get("image")), tsxPath),
            };
        }

        private static string GetSourcePath(XElement image, string tsxPath)
        {
            var imageSource = new XValue(image, "source").AsString();
            var tsxDirectory = Path.GetDirectoryName(tsxPath);
            return Path.Combine("Content", tsxDirectory, imageSource);
        }
    }
}
