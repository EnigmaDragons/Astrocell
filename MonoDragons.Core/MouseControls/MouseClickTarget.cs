using System;

namespace MonoDragons.Core.MouseControls
{
    public sealed class MouseClickTarget
    {
        public Action OnHit { get; set; }
        public Action OnMiss { get; set; }
    }
}
