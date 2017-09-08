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
                CollisionBoxes = new XBoxCollisions(tile).Get()
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
