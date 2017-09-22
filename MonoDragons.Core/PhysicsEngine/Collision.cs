using System.Collections.Generic;
using MonoDragons.Core.Entities;

namespace MonoDragons.Core.PhysicsEngine
{
    public class Collision : EntityComponent
    {
        public bool IsBlocking { get; set; } = true;
        public List<GameObject> CollidingWith { get; set; }

        public Collision()
        {
            CollidingWith = new List<GameObject>();
        }
    }
}
