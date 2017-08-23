using System;
using MonoDragons.Core.Entities;

namespace MonoDragons.Core.PhysicsEngine
{
    public sealed class MotionSystem : ISystem
    {
        public void Update(IEntities entities, TimeSpan delta)
        {
            entities.With<Motion2>(
                (o, m) => o.Transform.Location = o.Transform.Location + m.Velocity.GetDelta(delta));
        }
    }
}
