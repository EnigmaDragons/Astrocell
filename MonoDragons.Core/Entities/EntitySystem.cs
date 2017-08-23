using System;
using System.Collections.Generic;
using MonoDragons.Core.Common;
using Microsoft.Xna.Framework.Graphics;

namespace MonoDragons.Core.Entities
{
    public sealed class EntitySystem : IEntitySystemRegistration
    {
        private readonly HashSet<ISystem> _systems = new HashSet<ISystem>();
        private readonly HashSet<IRenderer> _renderers = new HashSet<IRenderer>();
        private readonly IEntities _entities;

        public EntitySystem(IEntities entities)
        {
            _entities = entities;
        }

        public void Register(ISystem system)
        {
            _systems.Add(system);
        }

        public void Register(IRenderer renderer)
        {
            _renderers.Add(renderer);
        }

        public void Update(TimeSpan delta)
        {
            _systems.ForEach(x => x.Update(_entities, delta));
        }

        public void Draw(SpriteBatch sprites)
        {
            _renderers.ForEach(x => x.Draw(_entities, sprites));
        }
    }
}
