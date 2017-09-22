using MonoDragons.Core.Entities;

namespace MonoDragons.Core.PhysicsEngine
{
    public sealed class BoxCollider : EntityComponent
    {
        public Transform2 Transform { get; set; }

        public BoxCollider(Transform2 transform)
        {
            Transform = transform;
        }
    }
}
