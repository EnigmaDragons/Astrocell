using MonoDragons.Core.Common;

namespace MonoDragons.Core.PhysicsEngine
{
    public sealed class Position
    {
        private Optional<Position> _parent = new Optional<Position>();

        public Transform2 Local { get; set; }

        public Transform2 World => _parent.IsPresent ? Local + _parent.Value.World : Local;

        public Position(Transform2 transform)
        {
            Local = transform;
        }
        
        public void AttachTo(Position parent)
        {
            _parent = parent;
            Local = Local - parent.World;
        }

        public void Detach()
        {
            _parent = new Optional<Position>();
            Local = World;
        }
    }
}
