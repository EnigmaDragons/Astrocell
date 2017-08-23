using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Entities;
using MonoDragons.Core.Memory;

namespace MonoDragons.Core.Render
{
    public class TextureRenderer : IRenderer
    {
        public void Draw(IEntities entities, SpriteBatch sprites)
        {
            entities.With<Texture>((o, t) => {
                Resources.Put(t.Value.GetHashCode().ToString(), t.Value);
                sprites.Draw(t.Value, null, o.Transform.ToRectangle(), null, null, o.Transform.Rotation.Value * .017453292519f, new Vector2(1, 1));
            });
        }
    }
}
