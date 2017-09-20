
namespace Astrocell.Battles.BattlePresentation
{
    public static class BattlePresenter
    {
        public static IBattlePresenter Instance { get; set; } = new NoPresenter();
    }
}
