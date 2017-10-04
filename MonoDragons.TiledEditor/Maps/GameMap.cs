using System;
using MonoDragons.Core.Common;
using MonoDragons.Core.Navigation;
using MonoDragons.Core.Scenes;

namespace MonoDragons.TiledEditor.Maps
{
    public static class GameMap
    {
        private static Map<string, Func<PlayerLocation, IScene>> _maps = new Map<string, Func<PlayerLocation, IScene>>();

        public static void Init(Map<string, Func<PlayerLocation, IScene>> maps)
        {
            _maps = maps;
        }

        public static void NavigateTo(PlayerLocation player)
        {
            Navigate.To(_maps[player.MapName](player));
            PlayerLocation.Current = player;
        }
    }
}
