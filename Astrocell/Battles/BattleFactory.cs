using MonoDragons.Core.Scenes;

namespace Astrocell.Battles
{
    public static class BattleFactory
    {
        public static IScene Create()
        {
            return new BattleScene();
        }
    }
}
