using Microsoft.Xna.Framework;
using MonoDragons.Core.PhysicsEngine;

namespace MonoDragons.Core.Render.Viewports
{
    public class SnapToPixelViewport : IViewport
    {
        private readonly Transform2 _position;

        public SnapToPixelViewport(Transform2 cameraPosition)
        {
            _position = new Transform2
            {
                Location = new Vector2((int)cameraPosition.Location.X, cameraPosition.Location.Y),
                Size = cameraPosition.Size,
                ZIndex = new ZIndex(cameraPosition.ZIndex.Value),
                Rotation = cameraPosition.Rotation,
            };
        }

        public Transform2 GetScreenPosition(Transform2 transform)
        {
            return transform - _position;
        }
    }
}
