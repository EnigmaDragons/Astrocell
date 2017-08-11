using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Entities;

namespace Astrocell.Scenes
{
    public interface IScene
    {
        void Setup(EntityComponentSystem ecs, GraphicsDevice gfx);
    }
}
