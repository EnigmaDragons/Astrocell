using System;
using Microsoft.Xna.Framework;

namespace MonoDragons.Core.MouseControls
{
    public sealed class MouseClickListener
    {
        public Action<Point> OnClick { get; }
    }
}
