using Astrocell.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Entities;

namespace Astrocell.Systems
{
    [Aspect(AspectType.All, typeof(SpriteComponent), typeof(TransformComponent2D))]
    [EntitySystem(GameLoopType.Draw, Layer = 0)]
    public sealed class SpriteRenderer : EntityProcessingSystem
    {
        private SpriteBatch _spriteBatch;

        public override void Initialize()
        {
            base.Initialize();
            
            _spriteBatch = Game.Services.GetService<SpriteBatch>();
        }

        protected override void Process(GameTime time, Entity e)
        {
            e.With<SpriteComponent>(s => 
                e.With<TransformComponent2D>(t => 
                    _spriteBatch.Draw(s.Sprite.TextureRegion.Texture, t.WorldPosition, s.Sprite.TextureRegion.Bounds, Color.White,
                        t.WorldRotation, s.Sprite.Origin, t.WorldScale, SpriteEffects.None, s.Sprite.Depth)));
        }
    }
}
