using System;
using System.Collections.Generic;
using System.Linq;
using MonoDragons.Core.Common;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;

namespace MonoDragons.Core.Entities
{
    public sealed class GameObjects : IEntities
    {
        private readonly Map<int, GameObject> _entities = new Map<int, GameObject>();

        private int _nextId;

        public int Count => _entities.Count;

        public GameObject Create(Transform2 transform)
        {
            var obj = new GameObject(_nextId++, transform);
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

        public void Remove(GameObject gameObject)
        {
            Remove(gameObject.Id);
        }

        public void Remove(IEnumerable<GameObject> objs)
        {
            objs.ForEach(x => Remove(x.Id));
        }

        public void Remove(int id)
        {
            if (_entities.ContainsKey(id))
                _entities[id].Dispose();
            _entities.Remove(id);
        }
    }
}
