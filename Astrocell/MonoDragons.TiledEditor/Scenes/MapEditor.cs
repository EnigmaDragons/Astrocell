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
            var textbox = Textbox.Create(new Transform2 { Size = new Size2(300, 50), ZIndex = ZIndex.Max });
            var enterAction = Entity.Create(Transform2.Zero)
                .Add(obj => new KeyboardCommand
                {
                    Key = Keys.Enter, Command = () =>
                    {
                        textbox.With<TypingInput>(
                            input => input.Value.If(value => !string.IsNullOrEmpty(value), () =>
                            {
                                _map.ForEach(Entity.Destroy);
                                _map.Clear();
                                new OrthographicTileMapFactory().CreateMap(Tmx.Create(Path.Combine("maps", input.Value))).ForEach(tile =>
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
                            }));
                        textbox.Disable();
                        obj.Disable();
                    }
                });
            return new List<GameObject> { textbox, enterAction };
        }
    }
}
