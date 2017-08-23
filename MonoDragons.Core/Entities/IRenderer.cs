using Microsoft.Xna.Framework.Graphics;

namespace MonoDragons.Core.Entities
{
    public interface IRenderer
    {
        void Draw(IEntities entities, SpriteBatch sprites);
    }
}
