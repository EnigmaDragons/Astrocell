using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Engine;

namespace MonoDragons.Core.Graphics
{
    public class RectangleTexture
    {
        private readonly Color _color;

        public RectangleTexture(Color color)
        {
            _color = color;
        }
        
        public Texture2D Create()
        {
            var data = new Color[1];
            for (var i = 0; i < data.Length; ++i)
                data[i] = _color;

            var texture = new Texture2D(GameInstance.TheGame.GraphicsDevice, 1, 1);
            texture.SetData(data);
            return texture;
        }
    }
}
