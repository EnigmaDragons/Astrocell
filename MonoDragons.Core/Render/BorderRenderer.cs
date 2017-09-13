using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Entities;
using MonoDragons.Core.PhysicsEngine;

namespace MonoDragons.Core.Render
{
    public sealed class BorderRenderer : IRenderer
    {
        public void Draw(IEntities entities, SpriteBatch sprites)
        {
            entities.With<BorderTexture>((o, b) =>
            {
                sprites.Draw(b.Value, o.World.Expanded(new Size2(b.Width, b.Width)).ToRectangle(), null, Color.White,
                    o.World.Rotation.Radians, Vector2.Zero, SpriteEffects.None, o.World.ZIndex.AsDepth());
            });
        }
    }
}
