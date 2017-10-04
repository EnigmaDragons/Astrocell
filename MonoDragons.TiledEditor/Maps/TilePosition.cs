using Microsoft.Xna.Framework;
using MonoDragons.Core.PhysicsEngine;

namespace MonoDragons.TiledEditor.Maps
{
    public sealed class TilePosition
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int TileWidth { get; set; }

        //For Serialization
        public TilePosition() {}

        public TilePosition(Transform2 transform) 
            : this(transform.Location, transform.Size.Width) {}

        public TilePosition(Vector2 location, int tileWidth)
            : this((int)location.X / tileWidth, (int)location.Y / tileWidth, tileWidth) { }

        public TilePosition(int x, int y, int tileWidth)
        {
            X = x;
            Y = y;
            TileWidth = tileWidth;
        }

        public override string ToString()
        {
            return $"x:{X}, y:{Y}";
        }

        public static implicit operator Vector2(TilePosition position)
        {
            return new Vector2(position.TileWidth * position.X, position.TileWidth * position.Y);
        }

        public static implicit operator Transform2(TilePosition position)
        {
            return new Transform2
            {
                Location = position,
                Size = new Size2(position.TileWidth, position.TileWidth)
            };
        }
    }
}
