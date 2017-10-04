using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Common;
using MonoDragons.Core.Entities;
using MonoDragons.Core.MouseControls;
using MonoDragons.Core.Navigation;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Render;
using MonoDragons.Core.Render.Viewports;
using MonoDragons.Core.Scenes;
using MonoDragons.Core.Tiled;
using MonoDragons.Core.Tiled.TmxLoading;
using MonoDragons.Core.UserInterface;
using MonoDragons.TiledEditor.Events;
using MonoDragons.TiledEditor.Maps;

namespace MonoDragons.TiledEditor.Scenes
{
    public class MapEditor : EcsScene
    {
        private readonly Color NoTint = Color.Transparent;
        private readonly Color Hover = Color.FromNonPremultiplied(0xFF, 0x98, 0x00, 191);
        private readonly Color Selected = Color.FromNonPremultiplied(0xFF, 0x57, 0x22, 191);
        private readonly Color Event = Color.FromNonPremultiplied(0x21, 0x96, 0xF3, 191);

        private readonly string _path;
        private readonly MapEvents _events;

        private GameObject _tilePanel;
        private GameObject _editPanel;
        private GameObject _selectedTile;

        public MapEditor(string path, MapEvents events)
        {
            _path = path;
            _events = events;
        }

        protected override IEnumerable<GameObject> CreateObjs()
        {
            InitTilePanel();
            InitEditPanel();
            var tiles = new OrthographicTileMapFactory().CreateMap(Tmx.Create(_path));
            tiles.ForEach(tile => tile.Add(new HighlightColor
                {
                    Color = _events.GetTileEvents(new TilePosition(tile.World)).Any() ? Event : NoTint,
                    Offset = 10,
                    MinOpacity = 191,
                    MaxOpacity = 255
                })
                .Add(CreateTileMouseActions(tile)));
            _selectedTile = tiles.First();
            var camera = Entity.Create("Map Editor Camera", Transform2.CameraZero).Add(new Camera()).Add(new MouseDrag { Button = MouseButton.Right });
            return new List<GameObject> { camera, _tilePanel, _editPanel }.Concat(tiles);
        }

        private void InitTilePanel()
        {
            _tilePanel = Entity.Create("Tile Panel", new Transform2 {Size = new Size2(200, 900), ZIndex = ZIndex.Max - 13})
                .Add((o, r) => new Texture(r.CreateRectangle(Color.FromNonPremultiplied(70, 70, 70, 255), o)))
                .AttachTo(CurrentViewport.Position);
        }

        private void InitEditPanel()
        {
            _editPanel = Entity.Create("Edit Panel",
                    new Transform2
                    {
                        Size = new Size2(200, 900),
                        ZIndex = ZIndex.Max - 13,
                        Location = new Vector2(1400, 0)
                    })
                .Add((o, r) => new Texture(r.CreateRectangle(Color.FromNonPremultiplied(70, 70, 70, 255), o)))
                .AttachTo(CurrentViewport.Position)
                .Add(OptionPicker.Create("Add Teleport", new Transform2
                    {
                        Size = new Size2(180, 60),
                        Location = new Vector2(1410, 10),
                        ZIndex = ZIndex.Max - 12
                    },
                    new MapOptions(map => Navigate.To(new MapTeleportSelector(map, new TilePosition(_selectedTile.World), _events, _path))).Get().ToArray()));
        }

        private MouseStateActions CreateTileMouseActions(GameObject tile)
        {
            return new MouseStateActions
            {
                OnHover = () => HoverTile(tile),
                OnExit = () => LeaveTile(tile),
                OnPressed = () => SelectTile(tile),
            };
        }

        private void HoverTile(GameObject tile)
        {
            if (tile != _selectedTile)
                tile.With<HighlightColor>(highlight => highlight.Color = Hover);
        }

        private void LeaveTile(GameObject tile)
        {
            if (tile != _selectedTile)
                tile.With<HighlightColor>(highlight => highlight.Color = _events.GetTileEvents(new TilePosition(tile.World)).Any() ? Event : NoTint);
        }

        private void SelectTile(GameObject tile)
        {
            var previousTile = _selectedTile;
            _selectedTile = tile;
            LeaveTile(previousTile);
            tile.With<HighlightColor>(highlight => highlight.Color = Selected);
            RefreshTilePanel();
        }

        private void RefreshTilePanel()
        {
            _tilePanel.ClearChildren();
            _events.GetTileEvents(new TilePosition(_selectedTile.World)).ForEachIndex((e, i) =>
            {
                _tilePanel.Add(Entity.Create(e.TypeName,
                        new Transform2
                        {
                            Size = new Size2(180, 60),
                            Location = new Vector2(10 + _tilePanel.World.Location.X, 10 + _tilePanel.World.Location.Y + (70 * i)),
                            ZIndex = ZIndex.Max - 12
                        })
                    .Add((o, r) => new Texture(r.CreateRectangle(Color.Red, o)))
                    .Add(o => new MouseClickTarget
                    {
                        OnHit = () =>
                        {
                            _events.Remove(e);
                            RefreshTilePanel();
                        }
                    })
                    .Add(new TextDisplay {Text = () => e.TypeName}));
            });
        }
    }
}
