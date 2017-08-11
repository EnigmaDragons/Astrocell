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
            var garethSideView = ecs.EntityManager.CreateEntity();
            garethSideView.Attach<SpriteComponent>(x => x.Sprite = new Sprite(garethBattler.GetRegion(0)));
            garethSideView.Attach<TransformComponent2D>(x => x.Position = new Vector2(400, 128));
        }

        private Texture2D FromFile(GraphicsDevice gfx, string filePath)
        {
            using (var file = File.OpenRead(filePath))
                return Texture2D.FromStream(gfx, file);
        }
    }
}
