
using MonoDragons.Core.Entities;
using MonoDragons.Core.PhysicsEngine;

namespace MonoDragons.Core.Render.Viewports
{
    public static class CurrentViewport
    {
        public static GameObject FocusedCamera { private get; set; } = Entity.Create("Default Camera", Transform2.CameraZero);
        public static IViewport Snapshot => new SnapToPixelViewport(Position.World);

        public static IPosition Position { get; } = new DelegatePosition(() =>
        {
            var position = Transform2.CameraZero;
            FocusedCamera.With<Camera>(_ => position = FocusedCamera.World);
            return position;
        });
    }
}
