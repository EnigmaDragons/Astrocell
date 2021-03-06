﻿using System.Collections.Generic;
using MonoDragons.Core.Common;
using MonoDragons.Core.PhysicsEngine;

namespace MonoDragons.Core.Entities
{
    public static class Entity
    {
        internal static readonly EntityResources Resources = new EntityResources();
        internal static readonly GameObjects Objs = new GameObjects(Resources);
        internal static readonly EntitySystem _system = new EntitySystem(Objs);

        public static EntitySystem System => _system;
        public static int Count => Objs.Count;
        public static int ResourceCount => Resources.Count;

        public static void Register(ISystem system)
        {
            System.Register(system);
        }

        public static GameObject Create(string name)
        {
            return Create(name, Transform2.Zero);
        }

        public static GameObject Create(string name, Transform2 transform)
        {
            return Objs.Create(name, transform);
        }

        public static void Destroy(GameObject obj)
        {
            Objs.Destroy(obj);
        }

        public static void Destroy(IEnumerable<GameObject> objs)
        {
            objs.ForEach(Destroy);
        }
    }
}
