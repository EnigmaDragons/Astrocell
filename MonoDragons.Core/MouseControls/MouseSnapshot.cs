using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoDragons.Core.Engine;

namespace MonoDragons.Core.MouseControls
{
    public sealed class MouseSnapshot
    {
        public static IMousePositionProvider MousePositionProvider { private get; set; }

        private Microsoft.Xna.Framework.Input.MouseState _last;
        private Microsoft.Xna.Framework.Input.MouseState _current;

        public Point LastScreenPosition => _last.Position;
        public Point LastWorldPosition => MousePositionProvider.GetWorldPosition(LastScreenPosition);
        public Point ScreenPosition => _current.Position;
        public Point WorldPosition => MousePositionProvider.GetWorldPosition(ScreenPosition);
        public Point MovedBy => ScreenPosition - LastScreenPosition;
        public bool LeftIsPressed => _current.LeftButton == ButtonState.Pressed;
        public bool RightIsPressed => _current.RightButton == ButtonState.Pressed;
        public bool MiddleIsPressed => _current.MiddleButton == ButtonState.Pressed;
        public bool LeftStillPressed => LeftIsPressed && _last.LeftButton == ButtonState.Pressed;
        public bool RightStillPressed => RightIsPressed && _last.RightButton == ButtonState.Pressed;
        public bool MiddleStillPressed => MiddleIsPressed && _last.MiddleButton == ButtonState.Pressed;
        public bool LeftButtonJustPressed => _last.LeftButton != ButtonState.Pressed && LeftIsPressed;
        public bool LeftButtonJustReleased => _last.LeftButton == ButtonState.Pressed && !LeftIsPressed;
        public bool RightButtonJustPressed => _last.RightButton != ButtonState.Pressed && RightIsPressed;
        public bool RightButtonJustReleased => _last.RightButton == ButtonState.Pressed && !RightIsPressed;
        public bool MiddleButtonJustPressed => _last.MiddleButton != ButtonState.Pressed && MiddleIsPressed;
        public bool MiddleButtonJustReleased => _last.MiddleButton == ButtonState.Pressed && !MiddleIsPressed;
        public bool ButtonJustPressed => LeftButtonJustPressed || RightButtonJustPressed || MiddleButtonJustPressed;
        public bool ButtonJustReleased => LeftButtonJustReleased || RightButtonJustReleased || RightButtonJustPressed;
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
