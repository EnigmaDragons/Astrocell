using System;
using MonoDragons.Core.Entities;
using MonoDragons.Core.PhysicsEngine;

namespace MonoDragons.Core.Motion
{
    public class MotionStateSelector : ISystem
    {
        public void Update(IEntities entities, TimeSpan delta)
        {
            entities.With<MotionState>((obj, motionState) =>
            {
                obj.With<Motion2>(motion =>
                {
                    if (motionState.PreviousRotation.Equals(motion.Velocity.Direction))
                        return;
                    motionState.PreviousRotation = motion.Velocity.Direction;
                    motionState.PreviousCardinalRotation = motion.Velocity.Direction.ToCardinal(motionState.PreviousCardinalRotation);
                });
            });
        }
    }
}
