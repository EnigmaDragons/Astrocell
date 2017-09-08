using MonoDragons.Core.Entities;
using MonoDragons.Core.PhysicsEngine;

namespace MonoDragons.Core.Motion
{
    public class MotionState : EntityComponent
    {
        public Rotation2 PreviousRotation { get; set; }
        public Rotation2 PreviousCardinalRotation { get; set; }
    }
}
