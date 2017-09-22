using System;
using MonoDragons.Core.Engine;

namespace MonoDragons.Core.Timing
{
    public sealed class TimerAction : UpdateComponent
    {
        private bool ShouldPerformAction => TimerMode == Mode.Recurring || !IsDone;

        private TimeSpan _elapsed;

        public TimeSpan Interval { get; set; } = TimeSpan.MaxValue;
        public Action Action { get; set; } = () => { };
        public Mode TimerMode { get; set; } = Mode.Recurring;
        public bool IsDone { get; private set; }

        public override void Update(TimeSpan delta)
        {
            _elapsed += delta;
            while (_elapsed > Interval && ShouldPerformAction)
            {
                Action();
                IsDone = true;
                _elapsed -= Interval;
            }
        }

        public void Reset()
        {
            _elapsed = TimeSpan.MinValue;
        }

        public enum Mode
        {
            Recurring,
            Once
        }
    }
}
