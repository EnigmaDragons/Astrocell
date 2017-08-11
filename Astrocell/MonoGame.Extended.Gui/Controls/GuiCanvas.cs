﻿using MonoGame.Extended.TextureAtlases;

namespace MonoGame.Extended.Gui.Controls
{
    public class GuiCanvas : GuiLayoutControl
    {
        public GuiCanvas()
            : base(null)
        {
        }

        public GuiCanvas(GuiSkin skin) 
            : base(skin)
        {
        }

        public override void Layout(IGuiContext context, RectangleF rectangle)
        {
            foreach (var control in Controls)
            {
                var desiredSize = control.GetDesiredSize(context, rectangle.Size);
                PlaceControl(context, control, control.Position.X, control.Position.Y, desiredSize.Width, desiredSize.Height);
            }
        }
    }
}