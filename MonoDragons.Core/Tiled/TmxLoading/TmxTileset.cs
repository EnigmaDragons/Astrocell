using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using MonoDragons.Core.Common;

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
        public List<TmxTilesetTile> Tiles;

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
                Tiles = GetTiles(tileset).ToList(),
            };
        }

        private static string GetSourcePath(XElement image, string tmxPath)
        {
            var imageSource = new XValue(image, "source").AsString();
            var tmxDirectory = Path.GetDirectoryName(tmxPath);
            return Path.Combine(tmxDirectory, imageSource);
        }

        private static IEnumerable<TmxTilesetTile> GetTiles(XElement tileset)
        {
            var specialTiles = new Dictionary<int, XElement>();
            tileset.Elements(XName.Get("tile")).ForEach(x => specialTiles[new XValue(x, "id").AsInt()] = x);
            return Enumerable.Range(0, new XValue(tileset, "tilecount").AsInt())
                .Select(id => specialTiles.ContainsKey(id) 
                    ? TmxTilesetTile.CreateFromElement(specialTiles[id]) 
                    : TmxTilesetTile.CreateFromId(id));
        }
    }
}
