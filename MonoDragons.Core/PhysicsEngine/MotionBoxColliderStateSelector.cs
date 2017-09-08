using System;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Entities;
using MonoDragons.Core.Motion;

namespace MonoDragons.Core.PhysicsEngine
{
    public class MotionBoxColliderStateSelector : ISystem
    {
        public void Update(IEntities entities, TimeSpan delta)
        {
            entities.With<MotionBoxColliderStates>(
                (obj, motionStates) => obj.With<MotionState>(
                    motionState => obj.With<Motion2>(
                        motion => obj.With<BoxCollider>(
                            boxCollider => ReplaceCollider(
                                boxCollider,
                                GetCurrentCollider(motionStates, motion, motionState),
                                obj.Transform)))));
        }

        private void ReplaceCollider(BoxCollider oldCollider, BoxCollider newCollider, Transform2 transform)
        {
            oldCollider.IsBlocking = newCollider.IsBlocking;
            oldCollider.Transform = new Transform2(
                new Vector2(newCollider.Transform.Location.X + transform.Location.X, newCollider.Transform.Location.Y + transform.Location.Y), 
                newCollider.Transform.Size);
        }

        private BoxCollider GetCurrentCollider(MotionBoxColliderStates motionStates, Motion2 motion, MotionState motionState)
        {
            return motionStates.Colliders[(int)motionState.PreviousCardinalRotation.Degrees
                + (motion.Velocity.IsMoving()
                    ? (int)Moving.Yes
                    : (int)Moving.No)];
        }
    }
}
