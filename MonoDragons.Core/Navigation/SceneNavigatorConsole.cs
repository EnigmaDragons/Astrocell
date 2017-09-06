using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoDragons.Core.Common;
using MonoDragons.Core.Entities;
using MonoDragons.Core.KeyboardControls;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Render;
using MonoDragons.Core.UserInterface;

namespace MonoDragons.Core.Navigation
{
    public sealed class SceneNavigatorConsole
    {
        private bool _isEnabled;
        private readonly List<GameObject> _objs;

        private SceneNavigatorConsole(params GameObject[] objs)
        {
            _objs = objs.ToList();
        }

        private void Toggle()
        {
            _isEnabled = !_isEnabled;
            _objs.ForEach(x => x.IsEnabled = _isEnabled);
        }

        public static void Enable()
        {
            var panel = Entity.Create(new Transform2 {Size = new Size2(1920, 100), ZIndex=ZIndex.Max.Plus(-1)})
                .Add((o, r) => new Texture(r.CreateRectangle(Color.DarkGray, o)))
                .Disable();
            var textbox = Textbox.Create(new Transform2 {Location = new Vector2(400, 25), Size = new Size2(300, 50), ZIndex = ZIndex.Max})
                .Disable();
            var navigateCommand = new KeyboardCommand {Key = Keys.Enter};
            var enterAction = Entity.Create(Transform2.Zero)
                .Add(navigateCommand)
                .Disable();
            var sceneNavigator = new SceneNavigatorConsole(textbox, panel, enterAction);

            navigateCommand.Command = () => textbox.With<TypingInput>(
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
                    }));

            Entity.Create(Transform2.Zero)
                .Add(new KeyboardCommand {Key = Keys.OemTilde, Command = () => sceneNavigator.Toggle()});
        }
    }
}
