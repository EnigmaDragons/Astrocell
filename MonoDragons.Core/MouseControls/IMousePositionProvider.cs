using Microsoft.Xna.Framework;

namespace MonoDragons.Core.MouseControls
{
    public interface IMousePositionProvider
    {
        Point GetWorldPosition(Point mousePosition);
    }
}
