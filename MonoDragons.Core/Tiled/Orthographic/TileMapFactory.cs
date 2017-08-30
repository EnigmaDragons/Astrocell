using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Entities;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Render;
using MonoDragons.Core.Tiled.TmxLoading;

namespace MonoDragons.Core.Tiled.Orthographic
{
    public class TileMapFactory
    {
        public List<GameObject> CreateMap(Tmx tmx)
        {
            var details = new TileMapDetails(tmx);
            return tmx.Layers.SelectMany(
                    x => x.Tiles.Select(
                        y => Entity.Create(new Transform2(
                                new Rectangle(y.Column * tmx.TileWidth, y.Row * tmx.TileHeight, tmx.TileWidth, tmx.TileHeight), 
                                    new ZIndex(x.ZIndex)))
                            .Add((o, r) => new Texture(r.LoadTexture(details.Get(y.TextureId).Texture, o), details.Get(y.TextureId).SourceRect))))
                .ToList();
        }
    }
}
