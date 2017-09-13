using MonoDragons.Core.PhysicsEngine;

namespace MonoDragons.Core.Render.Viewports
{
    public class NoViewport : IViewport
    {
        public Transform2 Position => Transform2.Zero;

        public Transform2 GetScreenPosition(Transform2 transform)
        {
            return transform;
        }
    }
}
