using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Entities;

namespace MonoDragons.Core.Render
{
    public sealed class ScreenBackgroundRenderer : IRenderer
    {
        public void Draw(IEntities entities, SpriteBatch sprites)
        {
            entities.With<ScreenBackgroundColor>(
                e => sprites.GraphicsDevice.Clear(e.Color));
        }
    }
}
