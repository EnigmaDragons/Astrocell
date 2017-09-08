using System;
using MonoDragons.Core.Entities;
using MonoDragons.Core.Motion;
using MonoDragons.Core.PhysicsEngine;

namespace MonoDragons.Core.Render.Animations
{
    public class MotionAnimationStateSelector : ISystem
    {
        public void Update(IEntities entities, TimeSpan delta)
        {
            entities.With<MotionAnimationStates>(
                (obj, motionStates) => obj.With<MotionState>(
                    motionState => obj.With<Motion2>(
                        motion => obj.With<Animation>(
                            animation => EnsureCorrectAnimation(motionStates, motion, motionState, animation)))));
        }

        private void EnsureCorrectAnimation(MotionAnimationStates motionStates, Motion2 motion, MotionState motionState, Animation animation)
        {
            var newKey = GetAnimationKey(motion, motionState);
            if (newKey == motionStates.CurrentAnimationKey)
                return;
            motionStates.CurrentAnimationKey = newKey;
            ReplaceAnimation(animation, motionStates.Animations[newKey]);
        }

        private int GetAnimationKey(Motion2 motion, MotionState motionState)
        {
            return (int) motionState.PreviousCardinalRotation.Degrees
                   + (motion.Velocity.IsMoving()
                       ? (int) Moving.Yes
                       : (int) Moving.No);
        } 

        private void ReplaceAnimation(Animation oldAnimation, Animation newAnimation)
        {
            oldAnimation.CurrentFrame = 0;
            oldAnimation.Sprites = newAnimation.Sprites;
            oldAnimation.FrameLength = newAnimation.FrameLength;
            oldAnimation.TimeSpentOnFrame = TimeSpan.Zero;
        }
    }
}
