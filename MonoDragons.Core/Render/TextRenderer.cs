using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Entities;
using MonoDragons.Core.Text;
using System;
using System.Collections.Generic;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Render.Viewports;

namespace MonoDragons.Core.Render
{
    public sealed class TextRenderer : IRenderer
    {
        public void Draw(IEntities entities, SpriteBatch sprites, IViewport viewport)
        {
            entities.With<TextDisplay>((o, t) => RenderText(sprites, t, o, viewport));
            entities.With<MultiTextDisplay>((o, m) => m.Displays.ForEach(t => RenderText(sprites, t, o, viewport)));
        }

        private static void RenderText(SpriteBatch sprites, TextDisplay t, GameObject o, IViewport viewport)
        {
            var spriteFont = GameInstance.TheGame.Content.Load<SpriteFont>(t.Font);
            var wrapped = new WrappingText(() => spriteFont, () => o.World.WithPadding(t.Margin).ToRectangle().Width).Wrap(t.Text());
            var size = spriteFont.MeasureString(wrapped);
            var screenPosition = viewport.GetScreenPosition(o.World.WithPadding(t.Margin));
            sprites.DrawString(spriteFont, wrapped,
                AlignPositions[t.Align](screenPosition.ToRectangle(), size), t.Color,
                    screenPosition.Rotation.Radians, Vector2.Zero, 1, SpriteEffects.None, 1);
        }

        private static readonly Dictionary<TextAlign, Func<Rectangle, Vector2, Vector2>> AlignPositions =
            new Dictionary<TextAlign, Func<Rectangle, Vector2, Vector2>>
        {
            { TextAlign.Left, (area, size) => new Vector2(Left(area), CenterVertical(area, size)) },
            { TextAlign.Center, (area, size) => new Vector2(CenterHorizontal(area, size), CenterVertical(area, size)) },
            { TextAlign.Right, (area, size) => new Vector2(Right(area, size), CenterVertical(area, size)) },
            { TextAlign.TopLeft, (area, size) => new Vector2(Left(area), Top(area)) },
            { TextAlign.TopCenter, (area, size) => new Vector2(CenterHorizontal(area, size), Top(area)) },
            { TextAlign.TopRight, (area, size) => new Vector2(Right(area, size), Top(area)) },
        };

        private static int Top(Rectangle area)
        {
            return area.Location.Y;
        }

        private static int Left(Rectangle area)
        {
            return area.Location.X;
        }

        private static float Right(Rectangle area, Vector2 size)
        {
            return area.Location.X + area.Width - size.X;
        }

        private static float CenterHorizontal(Rectangle area, Vector2 size)
        {
            return area.Location.X + (area.Width / 2) - (size.X / 2);
        }

        private static float CenterVertical(Rectangle area, Vector2 size)
        {
            return area.Location.Y + (area.Height / 2) - (size.Y / 2);
        }
    }
}
