using System;
using MonoDragons.Core.Entities;

namespace MonoDragons.Core.PhysicsEngine
{
    public class OnCollision : EntityComponent
    {
        public Action<GameObject> Action { get; set; }
    }
}
