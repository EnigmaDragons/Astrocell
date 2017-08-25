using Microsoft.Xna.Framework;
using MonoDragons.Core.Entities;

namespace MonoDragons.Core.Render
{
    public sealed class ScreenBackgroundColor : EntityComponent
    {
        public Color Color { get; set; } = Color.Black;
    }
}
