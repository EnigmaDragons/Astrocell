using System;
using MonoDragons.Core.Entities;

namespace MonoDragons.Core.MouseControls
{
    public sealed class MouseClickTarget : EntityComponent
    {
        public Action OnHit { get; set; }
        public Action OnMiss { get; set; }
    }
}
