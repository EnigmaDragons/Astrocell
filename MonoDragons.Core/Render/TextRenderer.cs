using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Entities;
using MonoDragons.Core.Memory;
using MonoDragons.Core.Text;
using System;
using System.Collections.Generic;

namespace MonoDragons.Core.Render
{
    public sealed class TextRenderer : IRenderer
    {
        public void Draw(IEntities entities, SpriteBatch sprites)
        {
            entities.With<TextDisplay>((o, t) => {
                var spriteFont = Resources.Load<SpriteFont>(t.Font);
                var wrapped = new WrappingText(() => spriteFont, () => o.Transform.WithPadding(t.Margin).ToRectangle().Width).Wrap(t.Text());
                var size = spriteFont.MeasureString(wrapped);
                sprites.DrawString(spriteFont, wrapped, _alignPositions[t.Align](o.Transform.WithPadding(t.Margin).ToRectangle(), size), t.Color,
                    o.Transform.Rotation.Value * .017453292519f, Vector2.Zero, 1, SpriteEffects.None, 1);
            });
        }

        private static readonly Dictionary<TextAlign, Func<Rectangle, Vector2, Vector2>> _alignPositions =
            new Dictionary<TextAlign, Func<Rectangle, Vector2, Vector2>>
        {
            { TextAlign.Left, GetLeftPosition },
            { TextAlign.Center, GetCenterPosition },
            { TextAlign.Right, GetRightPosition },
        };

        private static Vector2 GetLeftPosition(Rectangle area, Vector2 size)
        {
            return new Vector2(area.Location.X, area.Location.Y + (area.Height / 2) - (size.Y / 2));
        }

        private static Vector2 GetCenterPosition(Rectangle area, Vector2 size)
        {
            return new Vector2(area.Location.X + (area.Width / 2) - (size.X / 2), area.Location.Y + (area.Height / 2) - (size.Y / 2));
        }

        private static Vector2 GetRightPosition(Rectangle area, Vector2 size)
        {
            return new Vector2(area.Location.X + area.Width - size.X, area.Location.Y + (area.Height / 2) - (size.Y / 2));
        }
    }
}
