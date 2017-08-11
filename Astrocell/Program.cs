using System;
using Astrocell.Scenes;

namespace Astrocell
{
#if WINDOWS || LINUX
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            using (var game = new GameMain(() => new BattleScene()))
                game.Run();
        }
    }
#endif
}
