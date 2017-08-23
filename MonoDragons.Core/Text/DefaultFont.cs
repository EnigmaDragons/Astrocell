using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoDragons.Core.Text
{
    public static class DefaultFont
    {
        public static string Name => "Fonts/12";
        public static SpriteFont Font { get; set; }

        public static void Load(ContentManager content)
        {
            Font = content.Load<SpriteFont>(Name);
        }
    }
}
