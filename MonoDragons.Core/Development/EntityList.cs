using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoDragons.Core.Entities;
using MonoDragons.Core.KeyboardControls;
using MonoDragons.Core.Render;
using MonoDragons.Core.Common;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Render.Viewports;
using MonoDragons.Core.Text;

namespace MonoDragons.Core.Development
{
    public static class EntityList
    {
        private static readonly List<GameObject> _items = new List<GameObject>();

        public static void Enable(Keys key)
        {
            Entity.Create("Entity List")
                .Add(new KeyboardCommand {Key = key, Command = ToggleEntities});
        }

        private static void ToggleEntities()
        {
            if (_items.Any())
                _items.DequeueEach(Entity.Destroy);
            else
            {
                var viewportLoc = CurrentViewport.Position.World.Location;
                const int height = 25;
                var i = 0;
                Entity.Objs.ForEach(x =>
                {
                    var num = i++;
                    _items.Add(
                        Entity.Create($"Entity[{num}] Text", 
                            new Transform2 { Size = new Size2(400, height), Location = new Vector2(viewportLoc.X, viewportLoc.Y + (i - 1) * height)})
                            .Add(new TextDisplay {Align = TextAlign.Left, Text = () => $"{num}: {x.Name}"})
                            .AttachTo(CurrentViewport.Position));
                });
            }
        }
    }
}
