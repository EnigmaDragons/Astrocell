using System;

namespace MonoDragons.Core.PhysicsEngine
{
    public class DelegatePosition : IPosition
    {
        private readonly Func<Transform2> _world;
        public Transform2 World => _world();

        public DelegatePosition(Func<Transform2> world)
        {
            _world = world;
        }
    }
}
