using MonoDragons.Core.Entities;

namespace MonoDragons.Core.KeyboardControls
{
    public static class KeyboardSystems
    {
        public static void RegisterAll(EntitySystem system)
        {
            system.Register(new KeyboardInput());
            system.Register(new KeyboardCommandProcessing());
        }
    }
}
