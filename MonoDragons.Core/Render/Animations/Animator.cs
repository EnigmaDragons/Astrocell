using System;
using MonoDragons.Core.Entities;

namespace MonoDragons.Core.Render.Animations
{
    public class Animator : ISystem
    {
        public void Update(IEntities entities, TimeSpan delta)
        {
            entities.With<Animation>((obj, animation) =>
            {
                animation.TimeSpentOnFrame = animation.TimeSpentOnFrame.Add(delta);
                if (animation.TimeSpentOnFrame.Ticks >= animation.FrameLength.Ticks)
                    NextFrame(animation);
                obj.With<Texture>(x =>
                {
                    x.Value = animation.CurrentSprite.Value;
                    x.SourceRect = animation.CurrentSprite.SourceRect;
                });
            });
        }

        private static void NextFrame(Animation animation)
        {
            animation.CurrentFrame += 1;
            if (animation.CurrentFrame == animation.Sprites.Length)
                animation.CurrentFrame = 0;
            animation.TimeSpentOnFrame = TimeSpan.Zero;
        }
    }
}
