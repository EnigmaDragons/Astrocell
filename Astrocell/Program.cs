using System;
using System.Collections.Generic;
using Astrocell.Battles;
using Astrocell.Plugins;
using Astrocell.Scenes;
using Microsoft.Xna.Framework.Input;
using MonoDragons.Core.Common;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Entities;
using MonoDragons.Core.Inputs;
using MonoDragons.Core.Navigation;
using MonoDragons.Core.Render;
using MonoDragons.Core.Scenes;
using MonoDragons.TiledEditor.Scenes;

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
                "fire cave", 
                new Display(1600, 900, false, 1), 
                CreateSceneFactory(), 
                CreateController()))
            {
                AstrocellSystems.RegisterAll(Entity.System);
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
                { "CardDisplay", () => new CardScene() },
                { "Battle", () => new BattleScene() },
                { "Large", () => new Large() },
                { "MapEditor", () => new MapEditor() },
            });
        }
    }
#endif
}
