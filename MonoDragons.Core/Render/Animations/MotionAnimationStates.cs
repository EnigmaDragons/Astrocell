using System.Collections.Generic;
using MonoDragons.Core.Entities;
using MonoDragons.Core.PhysicsEngine;

namespace MonoDragons.Core.Render.Animations
{
    public class MotionAnimationStates : EntityComponent
    {
        public Dictionary<Rotation2, Animation> StandingAnimations { get; set; }
        public Dictionary<Rotation2, Animation> MovingAnimations { get; set; }

        public bool WasStanding { get; set; }
        public Rotation2 LastFacing { get; set; }

        public MotionAnimationStates(Animation standingUp, Animation standingRight, Animation standingDown, Animation standingLeft, 
            Animation movingUp, Animation movingRight, Animation movingDown, Animation movingLeft)
        {
            StandingAnimations = new Dictionary<Rotation2, Animation>
            {
                { Rotation2.Up, standingUp },
                { Rotation2.Right, standingRight },
                { Rotation2.Down, standingDown },
                { Rotation2.Left, standingLeft }
            };
            MovingAnimations = new Dictionary<Rotation2, Animation>
            {
                { Rotation2.Up, movingUp },
                { Rotation2.Right, movingRight },
                { Rotation2.Down, movingDown },
                { Rotation2.Left, movingLeft }
            };
        }
    }
}
