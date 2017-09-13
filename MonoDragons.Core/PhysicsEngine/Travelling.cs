using System;
using MonoDragons.Core.Entities;

namespace MonoDragons.Core.PhysicsEngine
{
    public sealed class Travelling : ISystem
    {
        public void Update(IEntities entities, TimeSpan delta)
        {
            entities.With<DurationTravel>(
                (o, x) => o.Local.SetTo(x.GetNewTransform(o.Local, delta)));
        }
    }
}
