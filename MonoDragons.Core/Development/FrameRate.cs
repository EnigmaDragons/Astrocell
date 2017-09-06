using System;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Entities;
using MonoDragons.Core.Timing;

namespace MonoDragons.Core.Development
{
    public sealed class FrameRateMonitoring : ISystem, IRenderer
    {
        public void Update(IEntities entities, TimeSpan delta)
        {
            FrameRate.Update(delta);
        }

        public void Draw(IEntities entities, SpriteBatch sprites)
        {
            FrameRate.OnFrameDrawn();
        }
    }

    public static class FrameRate
    {
        private static readonly FrameRateCounter Instance;

        static FrameRate()
        {
            Instance = new FrameRateCounter();
        }

        public static int PerSecond => Instance.FramesPerSecond;

        public static void Update(TimeSpan delta)
        {
            Instance.OnTimePassed(delta);
        }

        public static void OnFrameDrawn()
        {
            Instance.UpdateFrameCount();
        }

        private class FrameRateCounter
        {
            private readonly TimerAction _timer;
            private int _framesThisPeriod;

            public int FramesPerSecond { get; private set; }

            public FrameRateCounter()
            {
                _timer = new TimerAction {Action = Accumulate, Interval = TimeSpan.FromMilliseconds(500)};
            }

            public void OnTimePassed(TimeSpan delta)
            {
                _timer.Update(delta);
            }

            public void UpdateFrameCount()
            {
                _framesThisPeriod++;
            }

            private void Accumulate()
            {
                FramesPerSecond = _framesThisPeriod * 2;
                _framesThisPeriod = 0;
            }
        }
    }
}
