
namespace MonoDragons.Core.Render.Viewports
{
    public static class CurrentViewport
    {
        public static IViewport Instance { get; set; } = new NoViewport();
    }
}
