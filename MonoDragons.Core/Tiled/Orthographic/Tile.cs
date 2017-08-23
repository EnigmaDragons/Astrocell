using Microsoft.Xna.Framework;

namespace MonoDragons.Core.Tiled.Orthographic
{
    public class Tile
    {
        public string Texture { get; }
        public Rectangle SourceRect { get; }
        public Rectangle DestRect { get; }
        public int ZIndex { get; }

        public Tile(TileDetail detail, Rectangle destRect, int zIndex)
        {
            Texture = detail.Texture;
            SourceRect = detail.SourceRect;
            DestRect = destRect;
            ZIndex = zIndex;
        }
    }
}
