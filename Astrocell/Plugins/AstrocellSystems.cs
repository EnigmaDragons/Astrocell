using MonoDragons.Core.Entities;
using MonoDragons.Core.KeyboardControls;

namespace Astrocell.Plugins
{
    public static class AstrocellSystems
    {
        public static void RegisterAll(EntitySystem system)
        {
            Entity.Register(new TopDownController());
            system.Register(new BufferedLogAdvancement());
        }
    }
}
