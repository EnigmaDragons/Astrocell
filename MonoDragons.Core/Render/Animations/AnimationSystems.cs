using MonoDragons.Core.Entities;

namespace MonoDragons.Core.Render.Animations
{
    public static class AnimationSystems
    {
        public static void RegisterAll(EntitySystem system)
        {
            system.Register(new MotionAnimationStateSelector());
            system.Register(new Animator());
        }
    }
}
