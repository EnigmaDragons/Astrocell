using MonoDragons.Core.Entities;

namespace MonoDragons.Core.Development
{
    public static class DevelopmentSystems
    {
        public static void RegisterAll(EntitySystem system)
        {
            system.Register((IRenderer)new FrameRateMonitoring());
            system.Register((ISystem)new FrameRateMonitoring());
            system.Register(new UpdateRateMonitoring());
            system.Register(new PressEscapeToQuit());
        }
    }
}
