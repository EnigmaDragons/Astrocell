using System;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Entities;

namespace MonoDragons.Core.MouseControls
{
    public sealed class MouseClickListener : EntityComponent
    {
        public Action<Point> OnClick { get; set; }
    }
}
