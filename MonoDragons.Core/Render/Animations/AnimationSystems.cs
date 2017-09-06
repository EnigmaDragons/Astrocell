using MonoDragons.Core.Entities;

namespace MonoDragons.Core.Render.Animations
{
    public static class AnimationSystems
    {
        public static void RegisterAll()
        {
            Entity.Register(new MotionAnimationStateSelector());
            Entity.Register(new Animator());
        }
    }
}
