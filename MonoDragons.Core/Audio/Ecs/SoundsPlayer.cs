using System;
using MonoDragons.Core.Common;
using MonoDragons.Core.Entities;

namespace MonoDragons.Core.Audio.Ecs
{
    public sealed class SoundsPlayer : ISystem
    {
        public void Update(IEntities entities, TimeSpan delta)
        {
            entities.With<Sounds>(s => 
                s.Dequeue().ForEach(x => Audio.PlaySound(x.Name, x.Volume)));
        }
    }
}
