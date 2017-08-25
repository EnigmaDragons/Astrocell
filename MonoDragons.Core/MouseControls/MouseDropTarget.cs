using System;
using MonoDragons.Core.Entities;

namespace MonoDragons.Core.MouseControls
{
    public sealed class MouseDropTarget : EntityComponent
    {
        public Action OnEnter { get; set; }
        public Action OnExit { get; set; }
        public Action<GameObject> OnDrop { get; set; }
    }
}
