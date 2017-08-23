using System;

namespace MonoDragons.Core.MouseControls
{
    public sealed class MouseStateActions
    {
        private MouseState CurrentState { get; set; } = MouseState.None;
        private DateTime ClickedAt { get; set; } = DateTime.MinValue;

        public Func<bool> IsEnabled { get; set; } = () => true;
        public Action OnReleased { get; set; } = () => {};
        public Action OnHover { get; set; } = () => {};
        public Action OnPressed { get; set; } = () => {};
        public Action OnExit { get; set; } = () => {};

        public void Hover()
        {
            if (!IsEnabled())
                return;

            OnHover();
            CurrentState = MouseState.Hovered;
        }

        public void Exit()
        {
            if (!IsEnabled())
                return;

            if (CurrentState != MouseState.None)
                OnExit();
            CurrentState = MouseState.None;
        }

        public void Click()
        {
            if (!IsEnabled())
                return;

            ClickedAt = DateTime.Now;
            OnPressed();
            CurrentState = MouseState.Pressed;
        }

        public void Release()
        {
            if (!IsEnabled())
                return;

            OnHover();
            if ((DateTime.Now - ClickedAt).Milliseconds < 150)
                OnReleased();
            CurrentState = MouseState.Hovered;
        }
    }
}
