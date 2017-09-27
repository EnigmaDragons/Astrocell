using System;
using MonoDragons.Core.Entities;

namespace MonoDragons.Core.PhysicsEngine
{
    public class StepTriggerCollision : ISystem
    {
        public void Update(IEntities entities, TimeSpan delta)
        {
            entities.With<StepTrigger>((o, trigger) =>
                o.With<OnCollision>(onCollision =>
                    onCollision.GameObject.Get<Collision>().CollidingWith
                        .ForEach(x => onCollision.Action(x))));
        }
    }
}
