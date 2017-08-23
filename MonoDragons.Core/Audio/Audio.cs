using MonoDragons.Core.Audio.Internal;
using NAudio.Wave;

namespace MonoDragons.Core.Audio
{
    public static class Audio
    {
        private static DampeningSampleProvider _musicTrack;

        public static void PlaySound(string soundName, float volume = 1.0f)
        {
            var filename = $"Content/Sounds/{ soundName }.mp3";
            var input = new DisposingFileReader(new AudioFileReader(filename));
            AudioPlaybackEngine.Instance.Play(input);
        }

        public static void PlayMusicOnce(string songName, float volume = 1.0f)
        {
            var filename = $"Content/{ songName }.mp3";
            var song = new AudioFileReader(filename);
            TransitionToSong(volume, song);
        }

        public static void PlayMusic(string songName, float volume = 1.0f)
        {
            var filename = $"Content/{ songName }.mp3";
            var song = new LoopingFileReader(new AudioFileReader(filename));
            TransitionToSong(volume, song);
        }

        public static void StopMusic()
        {
            PlayMusic("Music/mute", 0);
        }

        private static void TransitionToSong(float volume, ISampleProvider song)
        {
            if (_musicTrack == null)
                _musicTrack = new DampeningSampleProvider(song, volume);
            else
            {
                var old = _musicTrack;
                _musicTrack = new DampeningSampleProvider(song, volume, old.Dampeners);
                old.Volume = 0;
            }
            AudioPlaybackEngine.Instance.Play(_musicTrack);
        }
    }
}