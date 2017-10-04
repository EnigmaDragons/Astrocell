using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using System.Linq;

namespace MonoDragons.Core.Graphics
{
    public class RoundedRectangleTexture
    {
        private static Dictionary<RoundedRectangleTexture, Texture2D> CachedTextures = new Dictionary<RoundedRectangleTexture, Texture2D>();

        private readonly int _width;
        private readonly int _height;
        private readonly int _borderThickness;
        private readonly int _borderRadius;
        private readonly List<Color> _borderColors;

        public RoundedRectangleTexture(Size2 size, int borderThickness, int borderRadius, Color borderColor)
            : this(size, borderThickness, borderRadius, new List<Color> { borderColor }) { }       

        public RoundedRectangleTexture(Size2 size, int borderThickness, int borderRadius, List<Color> borderColors)
        {
            _width = size.Width;
            _height = size.Height;
            _borderThickness = borderThickness;
            _borderRadius = borderRadius;
            _borderColors = borderColors;
        }

        public static void ClearCache()
        {
            CachedTextures.Clear();
        }

        public Texture2D Create()
        {
            if (CachedTextures.ContainsKey(this))
                return CachedTextures[this];
            if (_borderColors == null || _borderColors.Count == 0) throw new ArgumentException("Must define at least one border color (up to three).");
            if (_borderThickness + _borderRadius > _height / 2 || _borderThickness + _borderRadius > _width / 2) throw new ArgumentException("Border will be too thick and/or rounded to fit on the texture.");

            Texture2D texture = new Texture2D(GameInstance.TheGame.GraphicsDevice, _width, _height, false, SurfaceFormat.Color);
            var data = new Color[texture.Width * texture.Height];
            for (var x = 0; x < texture.Width; x++)
                for (var y = 0; y < _height; y++)
                    data[x + _width * y] = GetPixelColor(x, y, _width, _height, _borderThickness, _borderRadius, _borderColors);

            texture.SetData(data);
            if (CachingRules.CacheTextures)
                CachedTextures.Add(this, texture);
            return texture;
        }

        private Color GetPixelColor(int x, int y, int width, int height, int borderThickness, int borderRadius, List<Color> borderColors)
        {
            var internalRectangle = new Rectangle((borderThickness + borderRadius), (borderThickness + borderRadius),
                width - 2 * (borderThickness + borderRadius), height - 2 * (borderThickness + borderRadius));
            if (internalRectangle.Contains(x, y))
                return borderColors[0];

            var origin = Vector2.Zero;
            var point = new Vector2(x, y);

            if (x < borderThickness + borderRadius)
            {
                if (y < borderRadius + borderThickness)
                    origin = new Vector2(borderRadius + borderThickness, borderRadius + borderThickness);
                else if (y > height - (borderRadius + borderThickness))
                    origin = new Vector2(borderRadius + borderThickness, height - (borderRadius + borderThickness));
                else
                    origin = new Vector2(borderRadius + borderThickness, y);
            }
            else if (x > width - (borderRadius + borderThickness))
            {
                if (y < borderRadius + borderThickness)
                    origin = new Vector2(width - (borderRadius + borderThickness), borderRadius + borderThickness);
                else if (y > height - (borderRadius + borderThickness))
                    origin = new Vector2(width - (borderRadius + borderThickness), height - (borderRadius + borderThickness));
                else
                    origin = new Vector2(width - (borderRadius + borderThickness), y);
            }
            else
            {
                if (y < borderRadius + borderThickness)
                    origin = new Vector2(x, borderRadius + borderThickness);
                else if (y > height - (borderRadius + borderThickness))
                    origin = new Vector2(x, height - (borderRadius + borderThickness));
            }

            if (!origin.Equals(Vector2.Zero))
            {
                var distance = Vector2.Distance(point, origin);

                if (distance > borderRadius + borderThickness + 1)
                    return Color.Transparent;
                if (distance > borderRadius + 1)
                {
                    if (borderColors.Count > 2)
                    {
                        var modNum = distance - borderRadius;

                        if (modNum < borderThickness / 2)
                        {
                            return Color.Lerp(borderColors[2], borderColors[1], (float)((modNum) / (borderThickness / 2.0)));
                        }
                        else
                        {
                            return Color.Lerp(borderColors[1], borderColors[0], (float)((modNum - (borderThickness / 2.0)) / (borderThickness / 2.0)));
                        }
                    }


                    if (borderColors.Count > 0)
                        return borderColors[0];
                }
            }

            return Color.Transparent;
        }

        public override bool Equals(object obj)
        {
            return (obj is RoundedRectangleTexture) ? ((RoundedRectangleTexture)obj).Equals(this) : false;
        }

        public bool Equals(RoundedRectangleTexture other)
        {
            return _borderColors.Select((c) => c.PackedValue).SequenceEqual(other._borderColors.Select((c) => c.PackedValue))
                && _width == other._width && _height == other._height && _borderRadius == other._borderRadius
                && _borderThickness == other._borderThickness;
        }

        public override int GetHashCode()
        {
            return _width + (_height << 4) + (_borderThickness << 8) + (_borderRadius << 16) + (_borderColors.GetHashCode() << 24);
        }
    }
}
