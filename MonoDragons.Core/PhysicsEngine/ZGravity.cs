
using MonoDragons.Core.Entities;

namespace MonoDragons.Core.PhysicsEngine
{
    public sealed class ZGravity : EntityComponent
    {
        public int Acceleration { get; set; } = 10;
        public bool IsEnabled { get; set; } = true;
    }
}
