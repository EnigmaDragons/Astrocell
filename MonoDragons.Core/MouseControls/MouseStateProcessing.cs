using System;
using MonoDragons.Core.Entities;

namespace MonoDragons.Core.MouseControls
{
    public sealed class MouseStateProcessing : ISystem
    {
        private MouseSnapshot _mouse = new MouseSnapshot();
        
        public void Update(IEntities entities, TimeSpan delta)
        {
            _mouse = _mouse.Current();

            if (!_mouse.IsOnGameScreen)
                return;

            entities.WithTopMost<MouseStateActions>(_mouse.WorldPosition, 
                (o, m) =>
                {
                    if (!o.World.Intersects(_mouse.WorldPosition))
                        m.Exit();
                    else if (!o.World.Intersects(_mouse.LastWorldPosition))
                        m.Hover();
                    else if (_mouse.ButtonJustPressed)
                        m.Click();
                    else if (_mouse.ButtonJustReleased)
                        m.Release();
                },
                (o, m) => m.Exit());
        }
    }
}
