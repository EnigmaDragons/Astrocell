using System;
using MonoDragons.Core.Entities;

namespace MonoDragons.Core.PhysicsEngine
{
    public class ClearCollisions : ISystem
    {
        public void Update(IEntities entities, TimeSpan delta)
        {
            entities.With<Collision>(x => x.CollidingWith.Clear());
        }
    }
}
