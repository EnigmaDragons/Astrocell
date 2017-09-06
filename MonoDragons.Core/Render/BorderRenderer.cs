using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Entities;

namespace MonoDragons.Core.Render
{
    public sealed class BorderRenderer : IRenderer
    {
        public void Draw(IEntities entities, SpriteBatch sprites)
        {
            entities.With<BorderTexture>((o, b) => {
                sprites.Draw(b.Value, null, o.Transform.ToRectangle(), null, null, o.Transform.Rotation.Degrees * .017453292519f, new Vector2(1, 1));
            });
        }
    }
}
