using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Entities;
using MonoDragons.Core.Render.Viewports;

namespace MonoDragons.Core.Render
{
    public sealed class ScreenBackgroundRenderer : IRenderer
    {
        public void Draw(IEntities entities, SpriteBatch sprites, IViewport viewport)
        {
            entities.With<ScreenBackgroundColor>(
                e => sprites.GraphicsDevice.Clear(e.Color));
        }
    }
}
