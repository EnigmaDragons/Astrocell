using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Xna.Framework;

namespace MonoDragons.Core.Tiled.TmxLoading
{
    public struct TmxTilesetTile
    {
        public int Id;
        public List<Rectangle> CollisionBoxes;

        public static TmxTilesetTile CreateFromElement(XElement tile)
        {
            return new TmxTilesetTile
            {
                Id = new XValue(tile, "id").AsInt(),
                CollisionBoxes = tile.Element(XName.Get("objectgroup"))
                                     .Elements(XName.Get("object"))
                                     .Select(x => new Rectangle(
                                         new XValue(x, "x").AsInt(), 
                                         new XValue(x, "y").AsInt(), 
                                         new XValue(x, "width").AsInt(), 
                                         new XValue(x, "height").AsInt())).ToList()
            };
        }

        public static TmxTilesetTile CreateFromId(int id)
        {
            return new TmxTilesetTile
            {
                Id = id,
                CollisionBoxes = new List<Rectangle>()
            };
        }
    }
}
