using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Entities;

namespace MonoDragons.Core.Render
{
    public class BorderTexture : EntityComponent
    {
        public Texture2D Value { get; set; }

        public BorderTexture(Texture2D texture)
        {
            Value = texture;
        }
    }
}
