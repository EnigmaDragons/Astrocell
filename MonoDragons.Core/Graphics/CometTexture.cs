using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Engine;
using System.Collections.Generic;

namespace MonoDragons.Core.Graphics
{
    public class CometTexture
    {
        private static Dictionary<CometTexture, Texture2D> CachedTextures = new Dictionary<CometTexture, Texture2D>();

        private readonly Color _color;
        private readonly bool _isHorizontal;

        public CometTexture(Color color, bool isHorizontal)
        {
            _color = color;
            _isHorizontal = isHorizontal;
        }

        public static void ClearCache()
        {
            CachedTextures.Clear();
        }

        public Texture2D Create()
        {
            if (CachedTextures.ContainsKey(this))
                return CachedTextures[this];
            var length = 18;
            var data = new Color[1 * 18];

            var alpha = 255;
            for (var i = 1; i < data.Length; i++)
            {
                alpha = 255 * i / length;
                data[i] = new Color(_color, alpha);
            }
            var height = _isHorizontal ? 1 : length;
            var width = _isHorizontal ? length : 1;

            var texture = new Texture2D(GameInstance.TheGame.GraphicsDevice, width, height);
            texture.SetData(data);
            if (CachingRules.CacheTextures)
                CachedTextures.Add(this, texture);
            return texture;
        }

        public override bool Equals(object obj)
        {
            return (obj is CometTexture) ? ((CometTexture)obj).Equals(this) : false;
        }

        public bool Equals(CometTexture other)
        {
            return _color.PackedValue.Equals(other._color.PackedValue) && _isHorizontal == other._isHorizontal;
        }

        public override int GetHashCode()
        {
            return (_color.GetHashCode() << 1) + _isHorizontal.GetHashCode();
        }
    }
}
