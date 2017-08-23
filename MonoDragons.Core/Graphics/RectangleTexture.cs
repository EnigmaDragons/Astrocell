using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using System;
using System.Collections.Generic;
using MonoDragons.Core.Graphics;

namespace MonoDragons.Core.Graphics
{
    public class RectangleTexture
    {
        private static Dictionary<RectangleTexture, Texture2D> CachedTextures = new Dictionary<RectangleTexture, Texture2D>();

        private readonly int _width;
        private readonly int _height;
        private readonly Color _color;

        public RectangleTexture(Rectangle rect, Color color) : this (rect.Size.X, rect.Size.Y, color) { }
        public RectangleTexture(Size2 size, Color color) : this(size.Width, size.Height, color) { }

        public RectangleTexture(int width, int height, Color color)
        {
            _width = width;
            _height = height;
            _color = color;
        }

        public static void ClearCache()
        {
            CachedTextures.Clear();
        }

        public Texture2D Create()
        {
            if (CachedTextures.ContainsKey(this))
                return CachedTextures[this];
            var data = new Color[_width * _height];
            for (var i = 0; i < data.Length; ++i)
                data[i] = _color;

            var texture = new Texture2D(Hack.TheGame.GraphicsDevice, _width, _height);
            texture.SetData(data);
            if(CachingRules.CacheTextures)
                CachedTextures.Add(this, texture);
            return texture;
        }

        public override bool Equals(object obj)
        {
            return (obj is RectangleTexture) ? ((RectangleTexture)obj).Equals(this) : false;
        }

        public bool Equals(RectangleTexture other)
        {
            return _color.PackedValue.Equals(other._color.PackedValue) && _width == other._width && _height == other._height;
        }

        public override int GetHashCode()
        {
            return _width + (_height << 8) + (_color.GetHashCode() << 16);
        }
    }
}
