using MonoDragons.Core.Entities;

namespace MonoDragons.Core.Render.Viewports
{
    public sealed class Camera : EntityComponent
    {
        public bool ShouldFocus { get; set; } = true;
    }
}
