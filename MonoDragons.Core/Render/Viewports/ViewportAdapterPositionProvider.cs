using Microsoft.Xna.Framework;
using MonoDragons.Core.MouseControls;
using MonoDragons.Core.PhysicsEngine;

namespace MonoDragons.Core.Render.Viewports
{
    public class ViewportAdapterPositionProvider : IMousePositionProvider
    {
        public Point WorldPosition(Point mousePosition)
        {
            return CurrentViewport.Snapshot.GetWorldPosition(new Transform2(mousePosition.ToVector2())).Location.ToPoint();
        }
    }
}
