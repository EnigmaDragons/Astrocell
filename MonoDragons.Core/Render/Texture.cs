using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Entities;

namespace MonoDragons.Core.Render
{
    public class Texture : EntityComponent
    {
        public Texture2D Value { get; set; }
        public Rectangle SourceRect { get; set; }

        public Texture(Texture2D value)
            : this(value, value.Bounds) {}

        public Texture(Texture2D value, Rectangle sourceRect)
        {
            Value = value;
            SourceRect = sourceRect;
        }
    }
}
