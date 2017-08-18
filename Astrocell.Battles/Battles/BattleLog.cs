namespace Astrocell.Battles.Battles
{
    public static class BattleLog
    {
        public static ILog Instance { get; set; } = new NoLog();
    }
}
