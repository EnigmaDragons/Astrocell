using Microsoft.Xna.Framework;

namespace Astrocell.Maps
{
    public sealed class TilePosition
    {
        private const int TileWidth = 48;
        private readonly int _x;
        private readonly int _y;

        public TilePosition(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public static implicit operator Vector2(TilePosition position)
        {
            return new Vector2(TileWidth * position._x, TileWidth * position._y);
        }
    }
}
