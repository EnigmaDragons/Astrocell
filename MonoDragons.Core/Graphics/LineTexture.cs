using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Engine;
using System.Collections.Generic;

namespace MonoDragons.Core.Graphics
{
    public sealed class LineTexture
    {
        private static Dictionary<LineTexture, Texture2D> CachedTextures = new Dictionary<LineTexture, Texture2D>();

        private readonly Color _color;

        public LineTexture(Color color)
        {
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
            var data = new[] { _color };
            var result = new Texture2D(GameInstance.TheGame.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            result.SetData(data, 0, result.Width * result.Height);
            if (CachingRules.CacheTextures)
                CachedTextures.Add(this, result);
            return result;
        }

        public override bool Equals(object obj)
        {
            return (obj is LineTexture) ? ((LineTexture)obj).Equals(this) : false;
        }

        public bool Equals(LineTexture other)
        {
            return _color.PackedValue.Equals(other._color.PackedValue);
        }

        public override int GetHashCode()
        {
            return _color.GetHashCode();
        }
    }
}
