using Microsoft.Xna.Framework;
using MonoDragons.Core.MouseControls;
using MonoDragons.Core.PhysicsEngine;

namespace MonoDragons.Core.Render.Viewports
{
    public class MouseViewport : IMousePositionProvider
    {
        public Point GetWorldPosition(Point mousePosition)
        {
            return CurrentViewport.Snapshot.GetWorldPosition(new Transform2(mousePosition.ToVector2())).Location.ToPoint();
        }
    }
}
