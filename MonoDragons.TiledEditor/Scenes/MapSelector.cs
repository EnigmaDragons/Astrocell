using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Entities;
using MonoDragons.Core.Navigation;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Scenes;
using MonoDragons.Core.UserInterface;
using MonoDragons.TiledEditor.Events;
using MonoDragons.TiledEditor.Maps;

namespace MonoDragons.TiledEditor.Scenes
{
    public class MapSelector : EcsScene
    {
        protected override IEnumerable<GameObject> CreateObjs()
        {
            yield return OptionPicker.Create("Pick Map",
                new Transform2
                {
                    Size = new Size2(120, 40),
                    Center = new Vector2(800, 30)
                },
                new MapOptions(map => Navigate.To(new MapEditor(map, new MapEvents(GetProjContentPath(map), new TeleportEvent())))).Get().ToArray());
        }

        private string GetProjContentPath(string map)
        {
            return Path.ChangeExtension(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\", "Content", map), ".events");
        }
    }
}
