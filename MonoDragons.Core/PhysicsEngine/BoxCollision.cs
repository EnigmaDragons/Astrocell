using System;
using System.Collections.Generic;
using System.Linq;
using MonoDragons.Core.Common;
using MonoDragons.Core.Entities;

namespace MonoDragons.Core.PhysicsEngine
{
    public sealed class BoxCollision : ISystem
    {
        public void Update(IEntities entities, TimeSpan delta)
        {
            var colliderObjs = GetColliderObjects(entities);
            colliderObjs.ForEachIndex((obj, i) =>
            {
                var transform = obj.Get<BoxCollider>().Transform;
                obj.With<Motion2>(motion => transform = GetProposedMotion(delta, obj));
                colliderObjs.Skip(i + 1).Where(other => transform.Intersects(other.Get<BoxCollider>().Transform)).ForEach(other => 
                {
                    obj.With<Collision>(x => x.CollidingWith.Add(other));
                    other.With<Collision>(x => x.CollidingWith.Add(obj));
                });
            });
        }

        private List<GameObject> GetColliderObjects(IEntities entities)
        {
            var colliderObjs = new List<GameObject>();
            entities.With<BoxCollider>((o, x) => colliderObjs.Add(o));
            return colliderObjs;
        }

        private Transform2 GetProposedMotion(TimeSpan delta, GameObject obj)
        {
            return obj.Get<BoxCollider>().Transform + obj.Get<Motion2>().Velocity.GetDelta(delta);
        }
    }
}
