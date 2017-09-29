using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using MonoDragons.Core.Common;
using MonoDragons.Core.PhysicsEngine;

namespace MonoDragons.Core.Entities
{
    public sealed class GameObjects : IEntities
    {
        private readonly EntityResources _resources;
        private readonly Map<int, GameObject> _entities = new Map<int, GameObject>();

        private int _nextId;

        public int Count => _entities.Count;

        public GameObjects(EntityResources resources)
        {
            _resources = resources;
        }

        public GameObject Create(string name, Transform2 transform)
        {
            var id = Interlocked.Increment(ref _nextId);
            var obj = new GameObject(id, name, transform, _resources, () => _entities.Remove(id));
            _entities.Add(obj.Id, obj);
            return obj;
        }

        public void ForEach(Action<GameObject> action)
        {
            _entities.ToList().ForEach(e => action(e.Value));
        }

        public void With<T>(Action<GameObject, T> action)
            where T : EntityComponent
        {
            _entities.Values.ToList().ForEach(o => o.With<T>(c => action(o, c)));
        }

        public void Destroy(GameObject gameObject)
        {
            Destroy(gameObject.Id);
        }

        public void Destroy(IEnumerable<GameObject> objs)
        {
            objs.ForEach(x => Destroy(x.Id));
        }

        public void Destroy(int id)
        {
            if (_entities.ContainsKey(id))
                _entities[id].Dispose();
            _entities.Remove(id);
        }
    }
}
