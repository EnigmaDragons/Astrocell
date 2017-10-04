using MonoDragons.Core.PhysicsEngine;

namespace MonoDragons.TiledEditor.Maps
{
    public class PlayerLocation
    {
        public static PlayerLocation Current { get; set; }

        public string MapName { get; set; }
        public Transform2 Transform { get; set; }
    }
}
