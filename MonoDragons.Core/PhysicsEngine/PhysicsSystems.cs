using MonoDragons.Core.Entities;

namespace MonoDragons.Core.PhysicsEngine
{
    public static class PhysicsSystems
    {
        public static void RegisterAll(EntitySystem system)
        {
            system.Register(new ZGravitation());
            system.Register(new MotionBoxColliderStateSelector());
            system.Register(new BoxCollision());
            system.Register(new CollisionTriggers());
            system.Register(new MotionSystem());
            system.Register(new Travelling());
        }
    }
}
