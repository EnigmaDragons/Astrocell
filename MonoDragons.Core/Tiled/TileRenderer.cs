using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Entities;
using MonoDragons.Core.Memory;
using MonoDragons.Core.Tiled.Orthographic;

namespace MonoDragons.Core.Tiled
{
    public class TileRenderer : IRenderer
    {
        public void Draw(IEntities entities, SpriteBatch sprites)
        {
            entities.With<Tile>((o, x) => sprites.Draw(x.Texture, o.Transform.ToRectangle(), x.SourceRect, Color.White, 0, Vector2.Zero, SpriteEffects.None, GetDepth(x.ZIndex)));
        }

        private float GetDepth(int zIndex)
        {
            return Math.Min(zIndex / (float)int.MaxValue, 1.0f);
        }
    }
}
