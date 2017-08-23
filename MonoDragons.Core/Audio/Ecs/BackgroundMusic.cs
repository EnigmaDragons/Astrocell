using MonoDragons.Core.Common;

namespace MonoDragons.Core.Audio.Ecs
{
    public class BackgroundMusic
    {
        public bool ShouldStopMusic { get; set; }
        public Optional<string> Song { get; set; }

        public BackgroundMusic(string song)
        {
            ShouldStopMusic = false;
            Song = song;
        }

        internal void Reset()
        {
            ShouldStopMusic = false;
            Song = new Optional<string>();
        }
    }
}
