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
            var textbox = Textbox.Create(new Transform2 { Size = new Size2(300, 50), ZIndex = ZIndex.Max });
            var enterAction = Entity.Create(Transform2.Zero)
                .Add(new KeyboardCommand
                {
                    Key = Keys.Enter, Command = () =>
                    {
                        textbox.With<TypingInput>(
                            t => t.Value.If(v => !string.IsNullOrEmpty(v), () =>
                            {
                                _map.ForEach(Entity.Destroy);
                                _map.Clear();
                                new OrthographicTileMapFactory().CreateMap(Tmx.Create(Path.Combine("maps", t.Value))).ForEach(x =>
                                {
                                    x.Add(new MouseStateActions
                                    {
                                        OnHover = () => x.With<Texture>(texture => texture.Tint = Color.LightBlue),
                                        OnExit = () => x.With<Texture>(texture => texture.Tint = Color.White),
                                    });
                                    _map.Add(x);
                                    AddObj(x);
                                });
                            }));
                    }
                });
            return new List<GameObject> { textbox, enterAction };
        }
    }
}
