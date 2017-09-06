using System;
using MonoDragons.Core.Entities;

namespace MonoDragons.Core.Render.Animations
{
    public class Animation : EntityComponent
    {
        public Texture[] Sprites { get; set; }
        public TimeSpan FrameLength { get; set; }
        public int CurrentFrame { get; set; }
        public TimeSpan TimeSpentOnFrame { get; set; }

        public Texture CurrentSprite => Sprites[CurrentFrame];

        public Animation(int frameLength, params Texture[] sprites)
        {
            Sprites = sprites;
            FrameLength = TimeSpan.FromMilliseconds(frameLength);
        }
    }
}
