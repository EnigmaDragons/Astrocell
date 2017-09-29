using System.Collections.Generic;
using System.IO;
using System.Linq;
using MonoDragons.Core.Common;
using MonoDragons.Core.Entities;
using MonoDragons.Core.Navigation;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Scenes;
using MonoDragons.Core.UserInterface;
using MonoDragons.TiledEditor.Scenes;

namespace Astrocell.Scenes
{
    public sealed class PickerScene : EcsScene
    {
        protected override IEnumerable<GameObject> CreateObjs()
        {
            yield return OptionPicker.Create("picker", new Transform2 {Size = new Size2(500, 50)},
                Directory.GetFiles(Path.Combine("Content", "Maps"))
                    .Where(fileName => Path.GetExtension(fileName).ToLower() == ".tmx")
                    .Select(mapName => new Option(mapName, () => Navigate.To(new MapEditor(mapName)))).ToArray());
        }
    }
}
