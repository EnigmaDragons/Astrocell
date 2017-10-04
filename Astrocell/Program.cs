using System;
using System.Collections.Generic;
using System.IO;
using Astrocell.Battles;
using Astrocell.Maps;
using Astrocell.Plugins;
using Astrocell.Scenes;
using Microsoft.Xna.Framework.Input;
using MonoDragons.Core.Common;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Entities;
using MonoDragons.Core.Inputs;
using MonoDragons.Core.Navigation;
using MonoDragons.Core.PhysicsEngine;
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
            GameMap.Init(CreateMaps());
            using (var game = new NeedlesslyComplexMainGame(
                "Astrocell",
                "firecave", 
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

        private static Map<string, Func<PlayerLocation, IScene>> CreateMaps()
        {
            return new Map<string, Func<PlayerLocation, IScene>>
            {
                {nameof(FireCave), x => new FireCave(x)},
                {nameof(Large), x => new Large(x)},
            };
        }

        private static SceneFactory CreateSceneFactory()
        {
            return new SceneFactory(new Dictionary<string, Func<IScene>>
            {
                { "FireCave", () => new FireCave(new PlayerLocation { MapName = "FireCave", Transform = new Transform2 { Location = new TilePosition(5, 8) } }) },
                { "CardDisplay", () => new CardScene() },
                { "Battle", () => new BattleScene() },
                { "mapping", () => new MapSelector() },
                { "Large", () => new Large(new PlayerLocation { MapName = "Large", Transform =new Transform2 { Location = new TilePosition(5, 8) } }) },
            });
        }
    }
#endif
}
