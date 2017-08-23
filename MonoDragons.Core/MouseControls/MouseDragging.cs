﻿using System;
using System.Collections.Generic;
using System.Linq;
using MonoDragons.Core.Entities;
using MonoDragons.Core.Common;

namespace MonoDragons.Core.MouseControls
{
    public sealed class MouseDragging : ISystem
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
                t => t.With<MouseDrag>(m => m.If(m.IsEnabled(),
                    () => {
                        m.Drop(_mouse.Position);
                        entities.WithTopMost<MouseDropTarget>(m.DropPoint.Value, dt => dt.OnDrop(t));
                    })));
            _targets.Clear();
        }

        private void UpdateTarget()
        {
            _targets.ForEach(
                t =>  t.With<MouseDrag>(m => m.If(m.IsEnabled(), 
                    () => {
                        m.UpdateDragPoint(_mouse.Position);
                        t.Transform.Location += _mouse.MovedBy.ToVector2();
                    }))
                );
        }

        private void SelectTarget(IEntities entities)
        {
            var possibleTargets = new List<GameObject>();
            entities.With<MouseDrag>((o, m) => o.Transform.If(t => t.Intersects(_mouse.LastPosition), t => possibleTargets.Add(o)));
            if (possibleTargets.Any())
                _targets.Add(possibleTargets.OrderByDescending(x => x.Transform.ZIndex).First());
        }
    }
}
