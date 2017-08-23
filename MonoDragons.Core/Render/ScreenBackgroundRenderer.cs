using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Entities;

namespace MonoDragons.Core.Render
{
    public sealed class ScreenBackgroundRenderer : IRenderer
    {
        public void Draw(IEntities entities, SpriteBatch sprites)
        {
            entities.With<ScreenBackgroundColor>(
                e => World.DrawBackgroundColor(e.Color));
        }
    }
}
