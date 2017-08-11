
using MonoGame.Extended.Entities;
using MonoGame.Extended.Sprites;

namespace Astrocell.Components
{
    [EntityComponent]
    public sealed class SpriteComponent : EntityComponent
    {
        public Sprite Sprite { get; set; }
    }
}
