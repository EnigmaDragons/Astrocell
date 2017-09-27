using System.Collections.Generic;
using System.IO;
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
        private readonly List<GameObject> _map = new List<GameObject>(); 

        protected override IEnumerable<GameObject> CreateObjs()
        {
            GameObject selectedTile;
            var textbox = Textbox.Create(new Transform2 { Size = new Size2(300, 50), ZIndex = ZIndex.Max })
                .Add(obj => new KeyboardCommand
                {
                    Key = Keys.Enter, Command = () =>
                    {
                        obj.With<TypingInput>(
                            input => input.Value.If(value => !string.IsNullOrEmpty(value), () =>
                            {
                                var path = Path.Combine("maps", input.Value);
                                if (!new TmxExists(path).Get())
                                {
                                    input.Value = "Map Not Found";
                                    return;
                                }
                                _map.ForEach(Entity.Destroy);
                                _map.Clear();
                                new OrthographicTileMapFactory().CreateMap(Tmx.Create(path)).ForEach(tile =>
                                {
                                    selectedTile = tile;
                                    tile.Add(new MouseStateActions
                                    {
                                        OnHover = () => tile.With<Texture>(texture =>
                                        {
                                            if (texture.Tint == Color.White)
                                                texture.Tint = Color.LightBlue;
                                        }),
                                        OnExit = () => tile.With<Texture>(texture =>
                                        {
                                            if (texture.Tint == Color.LightBlue)
                                                texture.Tint = Color.White;
                                        }),
                                        OnPressed = () => tile.With<Texture>(texture =>
                                        {
                                            selectedTile.With<Texture>(selectedTexture => selectedTexture.Tint = Color.White);
                                            texture.Tint = Color.Purple;
                                            selectedTile = tile;
                                        }),
                                    });
                                    _map.Add(tile);
                                    AddObj(tile);
                                });
                                obj.Disable();
                            }));
                    }
                });
            var camera = Entity.Create("Map Editor Camera", Transform2.CameraZero).Add(new Camera()).Add(new MouseDrag { Button = MouseButton.Right });
            return new List<GameObject> { textbox, camera };
        }
    }
}
