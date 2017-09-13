using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Entities;
using MonoDragons.Core.Render.Viewports;

namespace MonoDragons.Core.Render
{
    public sealed class TextureRenderer : IRenderer
    {
        public void Draw(IEntities entities, SpriteBatch sprites, IViewport viewport)
        {
            entities.With<Texture>((o, t) => Draw(sprites, t, o, viewport));
            entities.With<MultiTexture>((o, m) => m.Textures.ForEach(t => Draw(sprites, t, o, viewport)));
        }

        private static void Draw(SpriteBatch sprites, Texture t, GameObject o, IViewport viewport)
        {
            var screenPosition = viewport.GetScreenPosition(o.World);
            sprites.Draw(t.Value, screenPosition.ToRectangle(), t.SourceRect, Color.White, 
                screenPosition.Rotation.Radians, Vector2.Zero, SpriteEffects.None, screenPosition.ZIndex.AsDepth());
        }
    }
}
