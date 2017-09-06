using System;
using MonoDragons.Core.Entities;
using MonoDragons.Core.PhysicsEngine;

namespace MonoDragons.Core.Render.Animations
{
    public class MotionAnimationStateSelector : ISystem
    {
        public void Update(IEntities entities, TimeSpan delta)
        {
            entities.With<MotionAnimationStates>((obj, character) =>
            {
                obj.With<Motion2>(motion =>
                {
                    obj.With<Animation>(animation =>
                    {
                        var isStanding = !motion.Velocity.IsMoving();
                        var facing = motion.Velocity.Direction;
                        if (character.WasStanding == isStanding && character.LastFacing.Equals(facing))
                            return;
                        var animationFacing = GetAnimationRotation(character.LastFacing, facing);
                        var currentAnimation = CalculateCurrentAnimation(character, animationFacing, isStanding);
                        ReplaceAnimation(animation, currentAnimation);
                        character.WasStanding = isStanding;
                        character.LastFacing = facing;
                    });
                });
            });
        }

        private Animation CalculateCurrentAnimation(MotionAnimationStates character, Rotation2 animationFacing, bool isStanding)
        {
            var currentAnimation = isStanding
                ? character.StandingAnimations[animationFacing]
                : character.MovingAnimations[animationFacing];
            return currentAnimation;
        }

        private Rotation2 GetAnimationRotation(Rotation2 previousRotation, Rotation2 currentRotation2)
        {
            if (currentRotation2.Equals(Rotation2.UpRight))
                return previousRotation.Equals(Rotation2.Up) ? Rotation2.Right : Rotation2.Up;
            if (currentRotation2.Equals(Rotation2.UpLeft))
                return previousRotation.Equals(Rotation2.Up) ? Rotation2.Left : Rotation2.Up;
            if (currentRotation2.Equals(Rotation2.DownRight))
                return previousRotation.Equals(Rotation2.Down) ? Rotation2.Right : Rotation2.Down;
            if (currentRotation2.Equals(Rotation2.DownLeft))
                return previousRotation.Equals(Rotation2.Down) ? Rotation2.Left : Rotation2.Down;
            return currentRotation2;
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
