using Microsoft.Xna.Framework;
using MonoDragons.Core.Entities;
using MonoDragons.Core.PhysicsEngine;

namespace MonoDragons.Core.Render
{
    public sealed class HighlightColor : EntityComponent
    {
        public int MinOpacity { get; set; } = 10;
        public int MaxOpacity { get; set; } = 120;
        public Color Color { get; set; } = Color.Red;
        public ZIndex Offset { get; set; } = new ZIndex(-1);
        public int Width { get; set; } = 8;
        public int CornerRadius { get; set; } = 4;
    }
}
