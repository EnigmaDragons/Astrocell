using MonoDragons.Core.Entities;

namespace MonoDragons.Core.MouseControls
{
    public sealed class MouseWheelScale : EntityComponent
    {
        public float ScaleAmount { get; set; } = .10f;
    }
}
