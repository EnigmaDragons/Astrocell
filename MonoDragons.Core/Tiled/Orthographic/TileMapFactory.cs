using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Entities;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Tiled.TmxLoading;

namespace MonoDragons.Core.Tiled.Orthographic
{
    public class TileMapFactory
    {
        public List<GameObject> CreateMap(Tmx tmx)
        {
            var details = new TileMapDetails(tmx);
            return tmx.Layers.SelectMany(
                x => x.Tiles.Select(y => Entity.Create(
                        new Transform2(new Rectangle(y.Column * tmx.TileWidth, y.Row * tmx.TileHeight, tmx.TileWidth, tmx.TileHeight)))
                    .Add(new Tile( details.Get(y.TextureId), x.ZIndex)))).ToList();
        }
    }
}
