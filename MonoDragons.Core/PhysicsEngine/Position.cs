using MonoDragons.Core.Common;

namespace MonoDragons.Core.PhysicsEngine
{
    public sealed class Position
    {
        private Optional<Transform2> _parent = new Optional<Transform2>();

        public Transform2 Local { get; set; }

        public Transform2 World
        {
            get => _parent.IsPresent ? Local + _parent.Value : Local;
            //set => Local = _parent.IsPresent ? value - _parent.Value : value;
        }

        public Position(Transform2 transform)
        {
            Local = transform;
        }

        public void AttachTo(Transform2 parent)
        {
            _parent = parent;
            Local = Local - parent;
        }

        public void Detach()
        {
            _parent = new Optional<Transform2>();
            Local = World;
        }
    }
}
