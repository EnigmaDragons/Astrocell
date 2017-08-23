using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace MonoDragons.Core.Tiled.TmxLoading
{
    public struct TmxLayer
    {
        public int ZIndex;
        public int Width;
        public int Height;
        public List<TmxTile> Tiles;

        public static TmxLayer Create(int zIndex, XElement layer)
        {
            var result = new TmxLayer
            {
                ZIndex = zIndex,
                Width = new XValue(layer, "width").AsInt(),
                Height = new XValue(layer, "height").AsInt(),
                Tiles = new List<TmxTile>()
            };
            var layerData = layer.Element(XName.Get("data")).Value;
            var textureIds = new IntegersInText(layerData).Get().ToList();
            for (var i = 0; i < textureIds.Count; i++)
                if (textureIds[i] != 0)
                    result.Tiles.Add(TmxTile.Create(i % result.Width, (int)Math.Floor((double)i / result.Width), textureIds[i]));
            return result;
        }
    }
}
