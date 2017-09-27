using System;
using System.Collections.Generic;
using System.Linq;
using MonoDragons.Core.Entities;
using MonoDragons.Core.Common;

namespace MonoDragons.Core.MouseControls
{
    public sealed class MouseDraggingAndDropping : ISystem
    {
        private readonly List<GameObject> _targets = new List<GameObject>();

        private MouseSnapshot _mouse = new MouseSnapshot();
        
        public void Update(IEntities entities, TimeSpan delta)
        {
            _mouse = _mouse.Current();

            if (!_mouse.IsOnGameScreen)
                return;

            if (_mouse.LeftStillPressed)
                UpdateTarget();
            if (_mouse.LeftButtonJustPressed)
                SelectTarget(entities);
            if (!_mouse.LeftIsPressed)
                DropTarget(entities);
        }

        private void DropTarget(IEntities entities)
        {
            _targets.ForEach(
                t => t.With<MouseDragAndDrop>(m => m.If(m.IsEnabled(),
                    () => {
                        m.Drop(_mouse.WorldPosition);
                        entities.WithTopMost<MouseDropTarget>(m.DropPoint.Value, dt => dt.OnDrop(t));
                    })));
            _targets.Clear();
        }

        private void UpdateTarget()
        {
            _targets.ForEach(
                t =>  t.With<MouseDragAndDrop>(m => m.If(m.IsEnabled(), 
                    () => {
                        m.UpdateDragPoint(_mouse.WorldPosition);
                        t.World.Location += _mouse.MovedBy.ToVector2();
                    }))
                );
        }

        private void SelectTarget(IEntities entities)
        {
            var possibleTargets = new List<GameObject>();
            entities.With<MouseDragAndDrop>((o, m) => o.World.If(t => t.Intersects(_mouse.LastWorldPosition), t => possibleTargets.Add(o)));
            if (possibleTargets.Any())
                _targets.Add(possibleTargets.OrderByDescending(x => x.World.ZIndex.Value).First());
        }
    }
}
