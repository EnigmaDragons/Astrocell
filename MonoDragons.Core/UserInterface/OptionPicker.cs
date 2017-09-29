using Microsoft.Xna.Framework;
using MonoDragons.Core.Common;
using MonoDragons.Core.Entities;
using MonoDragons.Core.MouseControls;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Render;

namespace MonoDragons.Core.UserInterface
{
    public static class OptionPicker
    {
        public static GameObject Create(string name, Transform2 transform, params Option[] options)
        {
            var picker = Entity.Create(name, transform)
                .Add((o, r) => new Texture(r.CreateRectangle(Color.Orange, o)))
                .Add(o => new MouseClickListener { OnClick = x => o.ToggleChildren() })
                .Add(new TextDisplay { Text = () => "Click Me" });
            options.ForEachIndex((x, i) => picker.Add(CreateItem(transform, x, i + 1)));
            return picker.DisableChildren();
        }
     
        private static GameObject CreateItem(Transform2 transform, Option option, int itemNumber)
        {
            var itemTransform = transform.Copy();
            itemTransform.Location += new Vector2(0, transform.Size.Height * itemNumber);
            return Entity.Create($"Picker Option: {option.Name}", itemTransform)
                .Add((o, r) => new Texture(r.CreateRectangle(Color.Purple, o)))
                .Add(new TextDisplay { Text = () => option.Name })
                .Add(new MouseClickTarget { OnHit = option.Action });
        }
    }
}
