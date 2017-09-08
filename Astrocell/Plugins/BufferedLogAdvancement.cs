using System;
using MonoDragons.Core.Entities;

namespace Astrocell.Plugins
{
    public sealed class BufferedLogAdvancement : ISystem
    {
        public void Update(IEntities entities, TimeSpan delta)
        {
            entities.With<BufferedLog>(l => l.Update(delta));
        }
    }
}
