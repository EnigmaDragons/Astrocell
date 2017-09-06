using System;
using MonoDragons.Core.Entities;

namespace MonoDragons.Core.KeyboardControls
{
    public sealed class TypingInput : EntityComponent
    {
        private static readonly TimeSpan CursorBlinkInterval = TimeSpan.FromMilliseconds(350);

        private TimeSpan _timeUntilNextCursorBlink = CursorBlinkInterval;
        private bool _cursorIsVisible;

        public bool IsActive { get; set; }
        public int MaxChars { get; set; } = 32;
        public string Value { get; set; } = "";
        public bool ShowCursor { get; set; } = true;
        public string DisplayValue { get; private set; } = "";

        public void Update(TimeSpan delta)
        {
            _timeUntilNextCursorBlink -= delta;
            if (_timeUntilNextCursorBlink <= TimeSpan.Zero)
            {
                _cursorIsVisible = !_cursorIsVisible;
                _timeUntilNextCursorBlink = CursorBlinkInterval;
            }

            var cursor = IsActive && _cursorIsVisible ? "|" : "";
            DisplayValue = $"{Value}{cursor}";
        }

        public void Append(string val)
        {
            if (Value.Length < MaxChars)
                Value += val;
        }

        public void Backspace()
        {
            if (Value.Length > 0)
                Value = Value.Remove(Value.Length - 1);
        }

        public void Clear()
        {
            Value = "";
        }
    }
}
