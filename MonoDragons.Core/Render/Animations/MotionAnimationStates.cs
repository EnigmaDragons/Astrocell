﻿using System.Collections.Generic;
using MonoDragons.Core.Entities;
using MonoDragons.Core.Motion;
using MonoDragons.Core.PhysicsEngine;

namespace MonoDragons.Core.Render.Animations
{
    public class MotionAnimationStates : EntityComponent
    {
        public Dictionary<int, Animation> Animations { get; set; }
        public int CurrentAnimationKey { get; set; }

        public MotionAnimationStates(Animation idleUp, Animation idleRight, Animation idleDown, Animation idleLeft, 
            Animation movingUp, Animation movingRight, Animation movingDown, Animation movingLeft)
        {
            Animations = new Dictionary<int, Animation>
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
