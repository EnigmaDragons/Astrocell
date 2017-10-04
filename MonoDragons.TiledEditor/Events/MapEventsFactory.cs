namespace MonoDragons.TiledEditor.Events
{
    public static class MapEventsFactory
    {
        public static MapEvents Create(string map)
        {
            return new MapEvents(map, new TeleportEvent());
        }
    }
}
