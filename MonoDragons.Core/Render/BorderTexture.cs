using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Entities;
using MonoDragons.Core.Graphics;

namespace MonoDragons.Core.Render
{
    public class BorderTexture : EntityComponent
    {
        public Texture2D Value { get; set; }
        public int Width { get; set; } = 3;

        public BorderTexture()
            : this(RectangleTexture.White) { }

        public BorderTexture(Texture2D value)
        {
            Value = value;
        }
    }
}
