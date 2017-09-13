using MonoDragons.Core.Entities;
using MonoDragons.Core.PhysicsEngine;

namespace MonoDragons.Core.Render.Viewports
{
    public sealed class Camera : EntityComponent, IViewport
    {
        public Transform2 Position => GameObject.World;

        public Transform2 GetScreenPosition(Transform2 transform)
        {
            return transform - GameObject.World;
        }
    }
}
