using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoDragons.Core.Common;
using MonoDragons.Core.Development;
using MonoDragons.Core.Entities;
using MonoDragons.Core.KeyboardControls;
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

namespace MonoDragons.TiledEditor.Scenes
{
    public class MapEditor : EcsScene
    {
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
            tiles.ForEach(tile => tile.Add(CreateTileMouseActions(tile)));
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
                    new MapOptions(map => Navigate.To(new MapTeleportSelector(map, _selectedTile.World, _events, this))).Get().ToArray()));
        }

        private MouseStateActions CreateTileMouseActions(GameObject tile)
        {
            return new MouseStateActions
            {
                OnHover = () => tile.With<Texture>(HoverTile),
                OnExit = () => tile.With<Texture>(LeaveTile),
                OnPressed = () => tile.With<Texture>(texture => SelectTile(texture, tile)),
            };
        }

        private void HoverTile(Texture texture)
        {
            if (texture.Tint == Color.White)
                texture.Tint = Color.LightBlue;
        }

        private void LeaveTile(Texture texture)
        {
            if (texture.Tint == Color.LightBlue)
                texture.Tint = Color.White;
        }

        private void SelectTile(Texture texture, GameObject tile)
        {
            _selectedTile.With<Texture>(selectedTexture => selectedTexture.Tint = Color.White);
            texture.Tint = Color.Orange;
            _selectedTile = tile;
            RefreshTilePanel();
        }

        private void RefreshTilePanel()
        {
            _tilePanel.ClearChildren();
            _events.GetAllTouching(_selectedTile.World).ForEachIndex((e, i) =>
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
