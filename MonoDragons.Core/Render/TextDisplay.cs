using System;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Entities;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Text;

namespace MonoDragons.Core.Render
{
    public class TextDisplay : EntityComponent
    {
        public string Font { get; set; } = DefaultFont.Name;
        public Color Color { get; set; } = Color.White;
        public TextAlign Align { get; set; } = TextAlign.Center;
        public Func<string> Text { get; set; } = () => "";
        public Size2 Margin { get; set; } = new Size2(10, 10);
    }
}
