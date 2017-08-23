using MonoDragons.Core.Entities;

namespace MonoDragons.Core.Audio.Ecs
{
    public static class SoundSystems
    {
        public static void RegisterAll(EntitySystem system)
        {
            system.Register(new SoundsPlayer());
            system.Register(new MusicPlayer());
        }
    }
}
