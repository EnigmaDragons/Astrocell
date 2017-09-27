using System;
using System.Linq;
using MonoDragons.Core.Entities;

namespace MonoDragons.Core.PhysicsEngine
{
    public class RigidBodyCollision : ISystem
    {
        public void Update(IEntities entities, TimeSpan delta)
        {
            entities.With<Motion2>((o, motion) => o.With<Collision>(collision =>
            {
                if (o.Get<Collision>().IsBlocking && collision.CollidingWith.Any(x => x.Get<Collision>().IsBlocking))
                    motion.Velocity.Speed = 0;
            }));
        }
    }
}
