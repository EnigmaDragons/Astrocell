﻿using System;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Common;

namespace MonoDragons.Core.MouseControls
{
    public sealed class MouseDrag
    {
        public Func<bool> IsEnabled { get; set; } = () => true;

        public bool IsBeingDragged { get; private set; }
        public Point LastDragPoint { get; private set; }
        public Point DragPoint { get; private set; }
        public Optional<Point> DropPoint { get; private set; }

        public void UpdateDragPoint(Point location)
        {
            if (!IsEnabled())
                return;

            IsBeingDragged = true;
            LastDragPoint = DragPoint;
            DragPoint = location;
        }

        public void Drop(Point location)
        {
            IsBeingDragged = false;
            DragPoint = Point.Zero;
            DropPoint = location;
        }
    }
}
