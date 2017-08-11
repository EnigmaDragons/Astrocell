using System.IO;
using Astrocell.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.TextureAtlases;

namespace Astrocell.Scenes
{
    public sealed class BattleScene : IScene
    {
        public void Setup(EntityComponentSystem ecs, GraphicsDevice gfx)
        {
            var garethBattler = TextureAtlas.Create("Gareth Battler", FromFile(gfx, "Content/Sprites/gareth-battler.png"), 64, 64);
            var gareth = ecs.EntityManager.CreateEntity();
            gareth.Attach<SpriteComponent>(x => x.Sprite = new Sprite(garethBattler.GetRegion(0)));
            gareth.Attach<TransformComponent2D>(x => 
            {
                x.Position = new Vector2(600, 128);
                x.Scale = new Vector2(1.5f, 1.5f);
            });

            var leviBattler = TextureAtlas.Create("Levi Battler", FromFile(gfx, "Content/Sprites/levi-battler.png"), 64, 64);
            var levi = ecs.EntityManager.CreateEntity();
            levi.Attach<SpriteComponent>(x => x.Sprite = new Sprite(leviBattler.GetRegion(0)));
            levi.Attach<TransformComponent2D>(x =>
            {
                x.Position = new Vector2(600, 280);
                x.Scale = new Vector2(1.5f, 1.5f);
            });
        }

        private Texture2D FromFile(GraphicsDevice gfx, string filePath)
        {
            using (var file = File.OpenRead(filePath))
                return Texture2D.FromStream(gfx, file);
        }
    }
}
