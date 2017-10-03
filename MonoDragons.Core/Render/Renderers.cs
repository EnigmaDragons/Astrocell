using MonoDragons.Core.Entities;

namespace MonoDragons.Core.Render
{
    public static class Renderers
    {
        public static void RegisterAll(EntitySystem system)
        {
            // Drawn in Front-To-Back Order
            system.Register(new TextRenderer());
            system.Register(new BorderRenderer());
            system.Register(new TextureRenderer());
            system.Register(new HighlightRenderer());
            system.Register(new ScreenBackgroundRenderer());
        }
    }
}
