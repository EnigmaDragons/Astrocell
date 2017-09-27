using Microsoft.Xna.Framework;

namespace MonoDragons.Core.MouseControls
{
    public interface IMousePositionProvider
    {
        Point WorldPosition(Point mousePosition);
    }
}
