﻿using System;
using MonoDragons.Core.Entities;

namespace MonoDragons.Core.Audio
{
    public sealed class MusicPlayer : ISystem
    {
        public void Update(IEntities entities, TimeSpan delta)
        {
            entities.With<BackgroundMusic>(m =>
            {
                if (m.ShouldStopMusic)
                    Audio.StopMusic();
                else if (m.Song.HasValue)
                    Audio.PlayMusic(m.Song.Value);
                m.Reset();
            });
        }
    }
}
