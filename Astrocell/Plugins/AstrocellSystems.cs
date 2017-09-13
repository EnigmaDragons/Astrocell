using Microsoft.Xna.Framework;
using MonoDragons.Core.Entities;
using MonoDragons.Core.KeyboardControls;

namespace Astrocell.Plugins
{
    public static class AstrocellSystems
    {
        public static void RegisterAll(EntitySystem system)
        {
            system.Register(new TopDownController());
            system.Register(new BufferedLogAdvancement());
            system.Register(new MakeCameraFollowPlayer(new Vector2(800, 450)));
        }
    }
}
