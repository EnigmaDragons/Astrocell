using System;
using System.Diagnostics;
using MonoDragons.Core.Entities;

namespace MonoDragons.Core.MouseControls
{
    public sealed class MouseWheelScaling : ISystem
    {
        private MouseSnapshot _mouse = new MouseSnapshot();

        public void Update(IEntities entities, TimeSpan delta)
        {
            _mouse = _mouse.Current();

            var zoomDirection = _mouse.MouseWheelDelta.CompareTo(0);
            if (zoomDirection == 0)
                return;
            
            entities.With<MouseWheelScale>((o, x) =>
            {
                var newValue = o.Transform.Scale + x.ScaleAmount * (_mouse.MouseWheelDelta / 120f);
                Debug.WriteLine(newValue);
                o.Transform.Scale = newValue;
                int i = 0;
            });
        }
    }
}
