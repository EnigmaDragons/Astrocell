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

namespace MonoDragons.TiledEditor.Scenes
{
    public class MapEditor : EcsScene
    {
        private readonly string _path;

        private GameObject _tilePanel;
        private GameObject _editPanel;
        private GameObject _selectedTile;

        public MapEditor(string path)
        {
            _path = path;
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
                .Add((o, r) => new Texture(r.CreateRectangle(Color.FromNonPremultiplied(70, 70, 70, 255), o)));
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
            texture.Tint = Color.Purple;
            _selectedTile = tile;
        }
    }
}
