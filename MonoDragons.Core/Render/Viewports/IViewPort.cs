using MonoDragons.Core.PhysicsEngine;

namespace MonoDragons.Core.Render.Viewports
{
    public interface IViewport
    {
        Transform2 GetScreenPosition(Transform2 worldTransform);
        Transform2 GetWorldPosition(Transform2 screenTransform);
    }
}
