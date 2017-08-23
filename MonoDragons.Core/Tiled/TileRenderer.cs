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
            entities.With<Tile>(x => sprites.Draw(Resources.Load<Texture2D>(x.Texture), x.DestRect, x.SourceRect, Color.White, 0, Vector2.Zero, SpriteEffects.None, x.ZIndex));
        }
    }
}
