using MonoDragons.Core.Entities;
using System;

namespace MonoDragons.Core.Characters
{
    public sealed class Reaper : ISystem
    {
        public void Update(IEntities entities, TimeSpan delta)
        {
            entities.With<Mortal>(
                (o, mortal) => o.With<Health>(
                    (health) => { if (health.HP < 1) mortal.OnDeath(); }));
        }
    }
}
