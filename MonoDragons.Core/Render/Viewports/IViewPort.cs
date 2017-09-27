using MonoDragons.Core.PhysicsEngine;

namespace MonoDragons.Core.Render.Viewports
{
    public interface IViewport
    {
        Transform2 GetScreenPosition(Transform2 transform);
    }
}
