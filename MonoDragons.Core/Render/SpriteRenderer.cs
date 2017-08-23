using System;
using MonoDragons.Core.Entities;
using MonoDragons.Core.PhysicsEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Memory;

namespace MonoDragons.Core.Render
{
    public sealed class SpriteRenderer : IRenderer
    {
        public void Draw(IEntities entities, SpriteBatch sprites)
        {
            entities.Collect<Sprite>()
                .ForEach(t => t.With<Sprite>(s =>
                    sprites.Draw(Resources.Load<Texture2D>(s.Name), null, t.Transform.ToRectangle(), null, null,
                        GetRotation(t), new Vector2(1, 1), null, SpriteEffects.None, GetDepth(t))));
        }

        private float GetRotation(GameObject t)
        {
            return t.Transform.Rotation.Value * .017453292519f;
        }

        private float GetDepth(GameObject t)
        {
            return Math.Min(t.Transform.ZIndex / (float)int.MaxValue, 1.0f);
        }
    }
}
