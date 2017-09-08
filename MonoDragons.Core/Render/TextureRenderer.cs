using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Entities;

namespace MonoDragons.Core.Render
{
    public sealed class TextureRenderer : IRenderer
    {
        public void Draw(IEntities entities, SpriteBatch sprites)
        {
            entities.With<Texture>((o, t) => Draw(sprites, t, o));
            entities.With<MultiTexture>((o, m) => m.Textures.ForEach(t => Draw(sprites, t, o)));
        }

        private static void Draw(SpriteBatch sprites, Texture t, GameObject o)
        {
            sprites.Draw(t.Value, o.Transform.ToRectangle(), t.SourceRect, Color.White, 
                o.Transform.Rotation.Radians, Vector2.Zero, SpriteEffects.None, o.Transform.ZIndex.AsDepth());
        }
    }
}
