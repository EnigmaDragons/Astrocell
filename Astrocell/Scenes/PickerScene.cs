using System.Collections.Generic;
using MonoDragons.Core.Common;
using MonoDragons.Core.Entities;
using MonoDragons.Core.Navigation;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Scenes;
using MonoDragons.Core.UserInterface;

namespace Astrocell.Scenes
{
    public sealed class PickerScene : EcsScene
    {
        protected override IEnumerable<GameObject> CreateObjs()
        {
            yield return OptionPicker.Create("picker", new Transform2 {Size = new Size2(500, 50)},
                new Option("Fire Cave", () => Navigate.To("Fire Cave")),
                new Option("Large Map", () => Navigate.To("Large")));
        }
    }
}
