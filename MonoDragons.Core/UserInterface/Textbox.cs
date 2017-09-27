using Microsoft.Xna.Framework;
using MonoDragons.Core.Entities;
using MonoDragons.Core.KeyboardControls;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Render;
using MonoDragons.Core.Text;

namespace MonoDragons.Core.UserInterface
{
    public static class Textbox
    {
        public static GameObject Create(Transform2 transform)
        {
            return Entity.Create("Textbox", transform)
                .Add((o, r) => new Texture(r.CreateRectangle(Color.White, o)))
                .Add((o, r) => new BorderTexture { Value = r.CreateRectangle(Color.Orange, o) })
                .Add(new TypingInput { IsActive = true } )
                .Add(o => new TextDisplay { Color = Color.Black, Text = () => o.Get<TypingInput>().DisplayValue, Align = TextAlign.Left });
        }
    }
}
