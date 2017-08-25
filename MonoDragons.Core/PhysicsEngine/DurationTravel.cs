using System;
using MonoDragons.Core.Entities;

namespace MonoDragons.Core.PhysicsEngine
{
    public sealed class DurationTravel : EntityComponent
    {
        private Transform2 _target;
        private TimeSpan _remainingDuration;

        public Transform2 Target
        {
            get { return _target; }
            set
            {
                _target = value;
                _remainingDuration = Duration;
            }
        }

        public TimeSpan Duration { get; set; } = TimeSpan.FromMilliseconds(250);

        public Transform2 GetNewTransform(Transform2 current, TimeSpan delta)
        {
            if (_remainingDuration.TotalMilliseconds <= 0)
                return current;

            var tripPercent = Convert.ToSingle(delta.TotalMilliseconds / _remainingDuration.TotalMilliseconds);
            var result = Transform2.Lerp(current, Target, tripPercent);
            _remainingDuration -= delta;
            return result;
        }
    }
}
