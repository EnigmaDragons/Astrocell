using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Entities;
using MonoDragons.Core.MouseControls;
using MonoDragons.Core.Navigation;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Render;
using MonoDragons.Core.Render.Viewports;
using MonoDragons.Core.Scenes;
using MonoDragons.Core.Tiled;
using MonoDragons.Core.Tiled.TmxLoading;
using MonoDragons.TiledEditor.Events;

namespace MonoDragons.TiledEditor.Scenes
{
    public class MapTeleportSelector : EcsScene
    {
        private readonly string _path;
        private readonly Transform2 _startPosition;
        private readonly MapEvents _events;
        private readonly MapEditor _editor;

        public MapTeleportSelector(string path, Transform2 startPosition, MapEvents events, MapEditor editor)
        {
            _path = path;
            _startPosition = startPosition;
            _events = events;
            _editor = editor;
        }

        protected override IEnumerable<GameObject> CreateObjs()
        {
            var tiles = new OrthographicTileMapFactory().CreateMap(Tmx.Create(_path));
            tiles.ForEach(tile => tile.Add(CreateTileMouseActions(tile)));
            var camera = Entity.Create("Map Editor Camera", Transform2.CameraZero).Add(new Camera()).Add(new MouseDrag { Button = MouseButton.Right });
            return tiles.Concat(new List<GameObject> { camera });
        }

        private MouseStateActions CreateTileMouseActions(GameObject tile)
        {
            return new MouseStateActions
            {
                OnHover = () => tile.With<Texture>(texture => texture.Tint = Color.LightBlue),
                OnExit = () => tile.With<Texture>(texture => texture.Tint = Color.White),
                OnPressed = () => tile.With<Texture>(texture =>
                {
                    _events.Add(new TeleportEvent().Create(_startPosition, tile.World, _path));
                    Navigate.To(_editor);
                }),
            };
        }
    }
}
