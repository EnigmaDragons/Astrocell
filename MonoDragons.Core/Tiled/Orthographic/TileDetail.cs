using Microsoft.Xna.Framework;

namespace MonoDragons.Core.Tiled.Orthographic
{
    public class TileDetail
    {
        public string Texture { get; }
        public Rectangle SourceRect { get; }

        public TileDetail(string texture, Rectangle sourceRect)
        {
            Texture = texture;
            SourceRect = sourceRect;
        }
    }
}
