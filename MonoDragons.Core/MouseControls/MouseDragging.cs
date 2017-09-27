using System;
using MonoDragons.Core.Entities;

namespace MonoDragons.Core.MouseControls
{
    public class MouseDragging : ISystem
    {
        private MouseSnapshot _mouse = new MouseSnapshot();

        public void Update(IEntities entities, TimeSpan delta)
        {
            _mouse = _mouse.Current();
            entities.With<MouseDrag>((o, drag) =>
            {
                if ((drag.Button == MouseButton.Right && _mouse.RightStillPressed)
                || (drag.Button == MouseButton.Left && _mouse.LeftStillPressed)
                || (drag.Button == MouseButton.Center && _mouse.RightButtonJustPressed))
                o.World.Location -= _mouse.MovedBy.ToVector2();
            });
        }
    }
}
