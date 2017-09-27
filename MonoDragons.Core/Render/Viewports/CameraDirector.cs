using System;
using MonoDragons.Core.Entities;

namespace MonoDragons.Core.Render.Viewports
{
    public class CameraDirector : ISystem
    {
        public void Update(IEntities entities, TimeSpan delta)
        {
            entities.With<Camera>((o, cam) =>
            {
                if (cam.ShouldFocus)
                    CurrentViewport.FocusedCamera = o;
                cam.ShouldFocus = false;
            });
        }
    }
}
