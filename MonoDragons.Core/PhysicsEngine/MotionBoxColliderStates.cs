using System.Collections.Generic;
using MonoDragons.Core.Entities;
using MonoDragons.Core.Motion;
using MonoDragons.Core.Render.Animations;

namespace MonoDragons.Core.PhysicsEngine
{
    public class MotionBoxColliderStates : EntityComponent
    {
        public Dictionary<int, BoxCollider> Colliders { get; set; }

        public MotionBoxColliderStates(BoxCollider idleUp, BoxCollider idleRight, BoxCollider idleDown, BoxCollider idleLeft,
            BoxCollider movingUp, BoxCollider movingRight, BoxCollider movingDown, BoxCollider movingLeft)
        {
            Colliders = new Dictionary<int, BoxCollider>
            {
                { (int)Rotation2.Up.Degrees + (int)Moving.No, idleUp },
                { (int)Rotation2.Right.Degrees + (int)Moving.No, idleRight },
                { (int)Rotation2.Down.Degrees + (int)Moving.No, idleDown },
                { (int)Rotation2.Left.Degrees + (int)Moving.No, idleLeft },
                { (int)Rotation2.Up.Degrees + (int)Moving.Yes, movingUp },
                { (int)Rotation2.Right.Degrees + (int)Moving.Yes, movingRight },
                { (int)Rotation2.Down.Degrees + (int)Moving.Yes, movingDown },
                { (int)Rotation2.Left.Degrees + (int)Moving.Yes, movingLeft },
            };
        }
    }
}
