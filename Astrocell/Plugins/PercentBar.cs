using System;
using MonoDragons.Core.Entities;
using MonoDragons.Core.PhysicsEngine;

namespace Astrocell.Plugins
{
    public sealed class PercentBar : EntityComponent
    {
        public Func<float> CurrentPercent { get; set; } = () => 1.0f;
        public int MaxWidth { get; set; } = 0;
    }

    public sealed class PercentBarUpdates : ISystem
    {
        public void Update(IEntities entities, TimeSpan delta)
        {
            entities.With<PercentBar>(p =>
                p.Local.Size = new Size2(Convert.ToInt32(p.CurrentPercent() * p.MaxWidth), p.Local.Size.Height));
        }
    }
}
