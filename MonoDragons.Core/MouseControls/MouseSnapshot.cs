using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoDragons.Core.Engine;

namespace MonoDragons.Core.MouseControls
{
    public sealed class MouseSnapshot
    {
        private Microsoft.Xna.Framework.Input.MouseState _last;
        private Microsoft.Xna.Framework.Input.MouseState _current;

        public Point LastPosition => _last.Position;
        public Point Position => _current.Position;
        public Point MovedBy => Position - LastPosition;
        public bool LeftIsPressed => _current.LeftButton == ButtonState.Pressed;
        public bool RightIsPressed => _current.RightButton == ButtonState.Pressed;
        public bool LeftStillPressed => LeftIsPressed && _last.LeftButton == ButtonState.Pressed;
        public bool RightStillPressed => RightStillPressed && _last.RightButton == ButtonState.Pressed;
        public bool LeftButtonJustPressed => _last.LeftButton != ButtonState.Pressed && LeftIsPressed;
        public bool LeftButtonJustReleased => _last.LeftButton == ButtonState.Pressed && !LeftIsPressed;
        public bool RightButtonJustPressed => _last.RightButton != ButtonState.Pressed && RightIsPressed;
        public bool RightButtonJustReleased => _last.RightButton == ButtonState.Pressed && !RightIsPressed;
        public bool ButtonJustPressed => LeftButtonJustPressed || RightButtonJustPressed;
        public bool ButtonJustReleased => LeftButtonJustReleased || RightButtonJustReleased;
        public bool IsOnGameScreen => GameInstance.TheGame.IsActive;
        public int MouseWheelDelta => _current.ScrollWheelValue - _last.ScrollWheelValue;

        public MouseSnapshot()
            : this(new Microsoft.Xna.Framework.Input.MouseState()) { }

        public MouseSnapshot(Microsoft.Xna.Framework.Input.MouseState last)
        {
            _last = last;
            _current = Mouse.GetState();
        }

        public MouseSnapshot Current()
        {
            return new MouseSnapshot(_current);
        }
    }
}
