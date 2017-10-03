using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Common;
using MonoDragons.Core.Entities;
using MonoDragons.Core.Navigation;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Scenes;
using MonoDragons.Core.UserInterface;

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
                Directory.GetFiles(Path.Combine("Content", "Maps"))
                    .Where(fileName => Path.GetExtension(fileName).ToLower() == ".tmx")
                    .Select(mapName => new Option(Path.GetFileName(mapName), () => Navigate.To(new MapEditor(GetRelativePathUpOneFolder(mapName))))).ToArray()); 
        }

        private string GetRelativePathUpOneFolder(string path)
        {
            var newPath = path.Substring(path.IndexOf('\\') + 1);
            return newPath;
        }
    }
}
