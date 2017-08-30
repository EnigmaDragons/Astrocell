using System;
using System.Collections.Generic;
using Astrocell.Scenes;
using Microsoft.Xna.Framework.Input;
using MonoDragons.Core.Common;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Entities;
using MonoDragons.Core.Inputs;
using MonoDragons.Core.Render;
using MonoDragons.Core.Scenes;
using MonoDragons.Core.Tiled;

namespace Astrocell
{
#if WINDOWS || LINUX
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            using (var game = new NeedlesslyComplexMainGame(
                "Astrocell", 
                "DisposeScene", 
                new Display(1600, 900, false, 1), 
                CreateSceneFactory(), 
                CreateController()))
            {
                game.Run();
            }
        }

        private static IController CreateController()
        {
            return new KeyboardController(new Map<Keys, Control>
            {
                { Keys.Z, Control.A },
            });
        }

        private static SceneFactory CreateSceneFactory()
        {
            return new SceneFactory(new Dictionary<string, Func<IScene>>
                {
                    { "Fire Cave", () => new FireCave() },
                    { "DisposeScene", () => new EcsDisposeScene() }
                });
        }
    }
#endif
}
