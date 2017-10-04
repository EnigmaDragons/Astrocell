using MonoDragons.Core.Entities;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Render.Viewports;
using MonoDragons.Core.PhysicsEngine;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Graphics;
using System.Collections.Generic;
using System;

namespace MonoDragons.Core.Render
{
    public sealed class HighlightRenderer : IRenderer
    {
        public void Draw(IEntities entities, SpriteBatch sprites, IViewport viewport)
        {
            entities.With<HighlightColor>((o, h) =>
            {
                var screenPosition = viewport.GetScreenPosition(o.World.Expanded(new Size2(h.Width, h.Width)));
                var texture = new RectangleBorderTexture(screenPosition.Size,h.Width, h.CornerRadius, 
                    new List<Color> {
                        Color.FromNonPremultiplied(h.Color.R, h.Color.G, h.Color.B, Math.Min(h.MaxOpacity, h.Color.A)),
                        Color.FromNonPremultiplied(h.Color.R, h.Color.G, h.Color.B, h.MinOpacity) }).Create(); 
                sprites.Draw(texture, screenPosition.ToRectangle(), null, Color.White,
                    screenPosition.Rotation.Radians, Vector2.Zero, SpriteEffects.None, (screenPosition.ZIndex + h.Offset).AsDepth());
            });
        }
    }
}
