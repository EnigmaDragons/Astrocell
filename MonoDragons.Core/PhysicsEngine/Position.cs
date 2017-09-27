using MonoDragons.Core.Common;

namespace MonoDragons.Core.PhysicsEngine
{
    public sealed class Position : IPosition
    {
        private Optional<IPosition> _parent = new Optional<IPosition>();

        public Transform2 Local { get; set; }

        public Transform2 World => _parent.IsPresent ? Local + _parent.Value.World : Local;

        public Position(Transform2 transform)
        {
            Local = transform;
        }
        
        public void AttachTo(IPosition parent)
        {
            _parent = new Optional<IPosition>(parent);
            Local = Local - parent.World;
        }

        public void Detach()
        {
            _parent = new Optional<IPosition>();
            Local = World;
        }
    }
}
