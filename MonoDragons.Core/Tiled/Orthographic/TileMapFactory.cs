using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Tiled.TmxLoading;

namespace MonoDragons.Core.Tiled.Orthographic
{
    public class TileMapFactory
    {
        public List<Tile> CreateMap(Tmx tmx)
        {
            var details = new TileMapDetails(tmx);
            return tmx.Layers.SelectMany(
                x => x.Tiles.Where(y => y.TextureId != 0)
                    .Select(y => new Tile( details.Get(y.TextureId), 
                        new Rectangle(y.Column * tmx.TileWidth, y.Row * tmx.TileHeight, tmx.TileWidth, tmx.TileHeight), x.ZIndex))).ToList();
        }
    }
}
