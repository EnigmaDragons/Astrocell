using Astrocell.Battles.Battles;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Entities;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Render;

namespace Astrocell.Battles
{
    public static class CharacterDisplay
    {
        public static GameObject Create(BattleCharacter character, string charImg, Vector2 location)
        {
            return Entity.Create(new Transform2 { Location = location, Size = new Size2(300, 400)})
                .Add((o, r) => new MultiTexture(
                    new Texture(r.LoadTexture("Battle/char-portrait-back.png", o)),
                    new Texture(r.LoadTexture(charImg, o))
                ));
        }
    }
}
