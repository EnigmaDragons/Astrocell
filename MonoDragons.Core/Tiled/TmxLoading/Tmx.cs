using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace MonoDragons.Core.Tiled.TmxLoading
{
    public struct Tmx
    {
        public int Width;
        public int Height;
        public int TileWidth;
        public int TileHeight;
        public List<TmxTileset> Tilesets;
        public List<TmxLayer> Layers;

        public static Tmx Create(string tmxPath)
        {
            var doc = XDocument.Load(Path.Combine("Content", tmxPath));
            var map = doc.Element(XName.Get("map"));
            var result = new Tmx
            {
                Width = new XValue(map, "width").AsInt(),
                Height = new XValue(map, "height").AsInt(),
                TileWidth = new XValue(map, "tilewidth").AsInt(),
                TileHeight = new XValue(map, "tileheight").AsInt(),
                Tilesets = map.Elements(XName.Get("tileset")).Select(x => TmxTileset.Create(x, tmxPath)).ToList(),
                Layers = new List<TmxLayer>(),
            };
            var layers = map.Elements(XName.Get("layer")).ToList();
            for (var i = 0; i < layers.Count; i++)
                 result.Layers.Add(TmxLayer.Create(i, layers[i]));
            return result;
        }
    }
}
