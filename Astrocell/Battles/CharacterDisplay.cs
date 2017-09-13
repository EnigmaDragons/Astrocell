using Astrocell.Battles.Battles;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Entities;
using MonoDragons.Core.MouseControls;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Render;

namespace Astrocell.Battles
{
    public static class CharacterDisplay
    {
        public static GameObject Create(BattleCharacter character, string charImg, Vector2 location)
        {
            var display = Entity.Create(new Transform2 { Location = location, Size = new Size2(300, 400) })
                .Add((o, r) => new Texture(r.LoadTexture("Battle/char-portrait-back.png", o)))
                .Add(new MouseWheelScale());
            var face = Entity.Create(new Transform2 {Location = location, Size = new Size2(180, 180), ZIndex = 8 })
                .Add((o, r) => new Texture(r.LoadTexture(charImg, o)));
            face.AnchorTo(display.Transform, new Transform2(new Vector2(10, 110), new Size2(500, 500)));
            return display;
        }
    }
}
