using Microsoft.Xna.Framework;
using MonoDragons.Core.PhysicsEngine;

namespace MonoDragons.Core.Render.Viewports
{
    public class SnapshotViewport : IViewport
    {
        public Transform2 Position { get; }

        public SnapshotViewport(IViewport viewport)
        {
            Position = new Transform2
            {
                Location = new Vector2((int)viewport.Position.Location.X, (int)viewport.Position.Location.Y),
                Size = viewport.Position.Size,
                ZIndex = new ZIndex(viewport.Position.ZIndex.Value),
                Rotation = viewport.Position.Rotation,
            };
        }

        public Transform2 GetScreenPosition(Transform2 transform)
        {
            return transform - Position;
        }
    }
}
