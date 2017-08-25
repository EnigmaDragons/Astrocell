using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Tiled.TmxLoading;

namespace MonoDragons.Core.Tiled.Orthographic
{
    public class TileMapDetails
    {
        private readonly Dictionary<int, TileDetail> _tiles = new Dictionary<int, TileDetail>();

        public TileMapDetails(Tmx tmx)
        {
            tmx.Tilesets.ForEach(AddTileset);
        }

        public TileDetail Get(int textureId)
        {
            return _tiles[textureId];
        }

        private void AddTileset(TmxTileset tileset)
        {
            for (int i = 0; i < tileset.TileCount; i++)
                _tiles[tileset.FirstId + i] = new TileDetail(tileset.TileSource, GetTileRectangle(i, tileset));
        }

        private Rectangle GetTileRectangle(int tile, TmxTileset tileset)
        {
            var column = tile % tileset.Columns;
            var row = (int)Math.Floor((double)tile / tileset.Columns);
            var x = column * tileset.TileWidth + (column + 1) * tileset.Spacing;
            var y = row * tileset.TileHeight + (row + 1) * tileset.Spacing;
            return new Rectangle(x, y, tileset.TileWidth, tileset.TileHeight);
        }
    }
}
