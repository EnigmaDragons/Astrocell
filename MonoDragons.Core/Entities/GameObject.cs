using System;
using MonoDragons.Core.Common;
using MonoDragons.Core.PhysicsEngine;

namespace MonoDragons.Core.Entities
{
    public sealed class GameObject
    {
        private readonly EntityResources _resources;
        private readonly Map<Type, EntityComponent> _components = new Map<Type, EntityComponent>();
        private readonly Position _position;

        public int Id { get; }
        public string Name { get; }
        public bool IsEnabled { get; set; }

        public Transform2 Local
        {
            get => _position.Local;
            set => _position.Local = value;
        }

        public Transform2 World => _position.World;

        internal GameObject(int id, string name, Transform2 transform, EntityResources resources)
        {
            _resources = resources;
            Id = id;
            Name = name;
            IsEnabled = true;
            _position = new Position(transform);
        }

        public GameObject Disable()
        {
            IsEnabled = false;
            return this;
        }

        public GameObject AttachTo(GameObject obj)
        {
            AttachTo(obj._position);
            return this;
        }

        public GameObject AttachTo(Transform2 transform)
        {
            AttachTo(new Position(transform));
            return this;
        }

        public GameObject AttachTo(IPosition other)
        {
            _position.AttachTo(other);
            return this;
        }

        public GameObject Detach()
        {
            _position.Detach();
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

        public override bool Equals(object obj)
        {
            return obj is GameObject && obj.GetHashCode().Equals(GetHashCode());
        }

        public override int GetHashCode()
        {
            return Id;
        }

        internal void Dispose()
        {
            _components.ForEach(x => x.Dispose());
            _components.Clear();
            _resources.Release(this);
        }
    }
}
