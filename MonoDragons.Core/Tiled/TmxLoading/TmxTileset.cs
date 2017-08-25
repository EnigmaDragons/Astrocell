using System.IO;
using System.Xml.Linq;

namespace MonoDragons.Core.Tiled.TmxLoading
{
    public struct TmxTileset
    {
        public int FirstId;
        public int TileWidth;
        public int TileHeight;
        public int Spacing;
        public int TileCount;
        public int Columns;
        public string TileSource;

        public static TmxTileset Create(XElement tileset, string tmxPath)
        {
            return new TmxTileset
            {
                FirstId = new XValue(tileset, "firstgid").AsInt(),
                TileWidth = new XValue(tileset, "tilewidth").AsInt(),
                TileHeight = new XValue(tileset, "tileheight").AsInt(),
                Spacing = new XValue(tileset, "spacing").AsInt(),
                TileCount = new XValue(tileset, "tilecount").AsInt(),
                Columns = new XValue(tileset, "columns").AsInt(),
                TileSource = GetSourcePath(tileset.Element(XName.Get("image")), tmxPath),
            };
        }

        private static string GetSourcePath(XElement image, string tmxPath)
        {
            var imageSource = new XValue(image, "source").AsString();
            var tmxDirectory = Path.GetDirectoryName(tmxPath);
            return Path.Combine("Content", tmxDirectory, imageSource);
        }
    }
}
