using MonoDragons.Core.Entities;
using System;

namespace MonoDragons.Core.Inputs
{
    public sealed class DirectionHandler : ISystem
    {
        private Direction _unprocessedDirection = Direction.None;

        public DirectionHandler()
        {
            Input.OnDirection(DirectionChanged);
        }

        public void Update(IEntities entities, TimeSpan delta)
        {
            entities.With<Directable>(
                    (o, d) => d.Binding(_unprocessedDirection));
        }

        private void DirectionChanged(Direction direction)
        {
            _unprocessedDirection = direction;
        }
    }
}
