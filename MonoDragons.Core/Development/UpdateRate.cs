using System;
using MonoDragons.Core.Entities;
using MonoDragons.Core.Timing;

namespace MonoDragons.Core.Development
{
    public sealed class UpdateRateMonitoring : ISystem
    {
        public void Update(IEntities entities, TimeSpan delta)
        {
            UpdateRate.Update(delta);
        }
    }

    public static class UpdateRate
    {
        private static readonly UpdateRateCounter Instance;

        static UpdateRate()
        {
            Instance = new UpdateRateCounter();
        }

        public static int PerSecond => Instance.UpdatesPerSecond;

        public static void Update(TimeSpan delta)
        {
            Instance.OnUpdateOccurred(delta);
        }

        private class UpdateRateCounter
        {
            private readonly TimerAction _timer;
            private int _updatesThisPeriod;

            public int UpdatesPerSecond { get; private set; }

            public UpdateRateCounter()
            {
                _timer = new TimerAction { Action = Accumulate, Interval = TimeSpan.FromMilliseconds(500) };
            }

            public void OnUpdateOccurred(TimeSpan delta)
            {
                _updatesThisPeriod++;
                _timer.Update(delta);
            }

            private void Accumulate()
            {
                UpdatesPerSecond = _updatesThisPeriod * 2;
                _updatesThisPeriod = 0;
            }
        }
    }
}
