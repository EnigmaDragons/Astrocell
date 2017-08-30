using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Entities;

namespace MonoDragons.Core.Render
{
    public class TextureRenderer : IRenderer
    {
        public void Draw(IEntities entities, SpriteBatch sprites)
        {
            entities.With<Texture>((o, t) => 
                sprites.Draw(t.Value, o.Transform.ToRectangle(), t.SourceRect, Color.White, 
                    o.Transform.Rotation.Radians, Vector2.Zero, SpriteEffects.None, o.Transform.ZIndex.AsDepth()));
        }
    }
}
