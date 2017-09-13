using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Entities;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Render.Viewports;

namespace MonoDragons.Core.Render
{
    public sealed class BorderRenderer : IRenderer
    {
        public void Draw(IEntities entities, SpriteBatch sprites, IViewport viewport)
        {
            entities.With<BorderTexture>((o, b) =>
            {
                var screenPosition = viewport.GetScreenPosition(o.World.Expanded(new Size2(b.Width, b.Width)));
                sprites.Draw(b.Value, screenPosition.ToRectangle(), null, Color.White,
                    screenPosition.Rotation.Radians, Vector2.Zero, SpriteEffects.None, screenPosition.ZIndex.AsDepth());
            });
        }
    }
}
