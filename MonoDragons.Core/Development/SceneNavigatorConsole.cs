using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoDragons.Core.Common;
using MonoDragons.Core.Entities;
using MonoDragons.Core.KeyboardControls;
using MonoDragons.Core.Navigation;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Render;
using MonoDragons.Core.UserInterface;
using MonoDragons.Core.Render.Viewports;

namespace MonoDragons.Core.Development
{
    public sealed class SceneNavigatorConsole
    {
        private bool _isEnabled = true;
        private readonly List<GameObject> _objs;

        private SceneNavigatorConsole(params GameObject[] objs)
        {
            _objs = objs.ToList();
            Toggle();
        }

        private void Toggle()
        {
            _isEnabled = !_isEnabled;
            _objs.ForEach(x => x.Toggle());
        }

        public static void Enable()
        {
            var commands = new KeyboardCommands();
            var textbox = Textbox.Create(new Transform2 { Location = new Vector2(400, 25), Size = new Size2(300, 50), ZIndex = ZIndex.Max - 8 });
            var panel = Entity.Create("Scene Navigation Console", new Transform2 {Size = new Size2(1920, 100), ZIndex = ZIndex.Max - 10})
                .Add((o, r) => new Texture(r.CreateRectangle(Color.DarkGray, o)))
                .AttachTo(CurrentViewport.Position)
                .Add(commands);
            textbox
                .AttachTo(panel);
            var sceneNavigator = new SceneNavigatorConsole(textbox, panel);

            commands.Add(Keys.Back, () => textbox.With<TypingInput>(x => x.Clear()));
            commands.Add(Keys.Enter, () => textbox.With<TypingInput>(
                t => t.Value.If(v => !string.IsNullOrEmpty(v), () =>
                    {
                        var sceneName = t.Value;
                        if (Navigate.SceneExists(sceneName))
                        {
                            Navigate.To(sceneName);
                            t.Clear();
                            sceneNavigator.Toggle();
                        }
                        else
                            t.Value = "Scene Not Found";
                    })));

            Entity.Create("Scene Navigation Console Commands", Transform2.Zero)
                .Add(new KeyboardCommand {Key = Keys.OemTilde, Command = () => sceneNavigator.Toggle()});
        }
    }
}
