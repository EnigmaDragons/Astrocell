using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Entities;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Render;
using MonoDragons.Core.Tiled.TmxLoading;

namespace MonoDragons.Core.Tiled
{
    public class OrthographicTileMapFactory
    {
        public List<GameObject> CreateMap(Tmx tmx)
        {
            var tileIdToGameObject = CreateTileIdToGameObjectMap(tmx);
            return tmx.Layers.SelectMany(
                layer => layer.Tiles.Select(
                    tile => tileIdToGameObject[tile.TileId](
                        new Transform2(
                            new Rectangle(tile.Column * tmx.TileWidth, tile.Row * tmx.TileHeight, tmx.TileWidth, tmx.TileHeight), 
                            new ZIndex(layer.ZIndex))))).ToList();
        }

        private Dictionary<int, Func<Transform2, GameObject>> CreateTileIdToGameObjectMap(Tmx tmx)
        {
            var map = new Dictionary<int, Func<Transform2, GameObject>>();
            tmx.Tilesets.ForEach(
                tileset => tileset.Tiles.ForEach(
                    tile => map[tileset.FirstId + tile.Id] = transform => CreateTile(tileset, tile, transform)));
            return map;
        }

        private GameObject CreateTile(TmxTileset tileset, TmxTilesetTile tile, Transform2 transform)
        {
            var entity = Entity.Create(transform)
                .Add((o, r) => new Texture(r.LoadTexture(tileset.TileSource, o), GetTileRectangle(tile.Id, tileset)));
            return tile.CollisionBoxes.Any() ? WithBoxColliders(tile, entity) : entity;
        }

        private Rectangle GetTileRectangle(int tile, TmxTileset tileset)
        {
            var column = tile % tileset.Columns;
            var row = (int)Math.Floor((double)tile / tileset.Columns);
            var x = column * tileset.TileWidth + (column + 1) * tileset.Spacing;
            var y = row * tileset.TileHeight + (row + 1) * tileset.Spacing;
            return new Rectangle(x, y, tileset.TileWidth, tileset.TileHeight);
        }

        private GameObject WithBoxColliders(TmxTilesetTile tile, GameObject entity)
        {
            //TODO: allow multiple boxes
            var box = tile.CollisionBoxes.First();
            return entity.Add(new BoxCollider(
                new Transform2(
                    entity.Transform.Location + box.Location.ToVector2(),
                    new Size2(box.Width, box.Height))));
        }
    }
}
