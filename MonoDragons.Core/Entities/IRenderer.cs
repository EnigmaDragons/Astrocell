using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Render.Viewports;

namespace MonoDragons.Core.Entities
{
    public interface IRenderer
    {
        void Draw(IEntities entities, SpriteBatch sprites, IViewport viewport);
    }
}
