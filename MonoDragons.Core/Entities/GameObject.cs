using System;
using MonoDragons.Core.Common;
using MonoDragons.Core.PhysicsEngine;

namespace MonoDragons.Core.Entities
{
    public sealed class GameObject
    {
        private readonly EntityResources _resources;
        private readonly Map<Type, EntityComponent> _components = new Map<Type, EntityComponent>();

        private readonly Transform2 _localTransform;
        private Optional<Tuple<Transform2, Transform2>> _anchor = new Optional<Tuple<Transform2, Transform2>>();

        public int Id { get; }
        public bool IsEnabled { get; set; }

        public Transform2 Transform => _anchor.IsPresent
            ? _localTransform + _anchor.Value.Item1 + _anchor.Value.Item2
            : _localTransform;

        internal GameObject(int id, Transform2 transform, EntityResources resources)
        {
            _resources = resources;
            Id = id;
            IsEnabled = true;
            _localTransform = transform;
        }

        public override bool Equals(object obj)
        {
            return obj is GameObject && obj.GetHashCode().Equals(GetHashCode());
        }

        public override int GetHashCode()
        {
            return Id;
        }

        public GameObject Disable()
        {
            IsEnabled = false;
            return this;
        }

        public GameObject AnchorTo(Transform2 parent, Transform2 offset)
        {
            _anchor = new Optional<Tuple<Transform2, Transform2>>(new Tuple<Transform2, Transform2>(parent, offset));
            return this;
        }

        public GameObject Add(Func<GameObject, EntityResources, EntityComponent> componentBuilder)
        {
            Add(componentBuilder(this, _resources));
            return this;
        }

        public GameObject Add(Func<GameObject, EntityComponent> componentBuilder)
        {
            Add(componentBuilder(this));
            return this;
        }

        public GameObject Add<T>(T component) 
            where T : EntityComponent
        {
            return Add(component, typeof(T));
        }

        public GameObject Add(EntityComponent component)
        {
            return Add(component, component.GetType());
        }

        public GameObject Add(EntityComponent component, Type componentType)
        {
            if (_components.ContainsKey(componentType))
                throw new InvalidOperationException($"Cannot add more than one {componentType.Name} component.");
            _components.Add(componentType, component);
            component.Init(this);
            return this;
        }

        public T Get<T>() 
            where T : EntityComponent
        {
            return (T)_components[typeof(T)];
        }

        public void With<T>(Action<T> action) 
            where T : EntityComponent
        {
            var type = typeof(T);
            if (IsEnabled && _components.ContainsKey(type))
                action((T)_components[type]);
        }

        internal void Dispose()
        {
            _components.ForEach(x => x.Dispose());
            _components.Clear();
            _resources.Release(this);
        }
    }
}
