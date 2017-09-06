using System;
using Microsoft.Xna.Framework;

namespace MonoDragons.Core.Tiled
{
    public class SpriteSheetRectangle
    {
        private readonly int _index;
        private readonly int _columns;
        private readonly int _width;
        private readonly int _height;
        private readonly int _spacing;

        public SpriteSheetRectangle(int index, int columns, int width, int height, int spacing)
        {
            _index = index;
            _columns = columns;
            _width = width;
            _height = height;
            _spacing = spacing;
        }

        public Rectangle Get()
        {
            var column = _index % _columns;
            var row = (int)Math.Floor((double)_index / _columns);
            var x = column * _width + (column + 1) * _spacing;
            var y = row * _height + (row + 1) * _spacing;
            return new Rectangle(x, y, _width, _height);
        }
    }
}
