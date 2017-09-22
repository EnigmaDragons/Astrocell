using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Entities;
using MonoDragons.Core.Motion;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Render.Animations;
using MonoDragons.Core.Tiled.TmxLoading;
using Texture = MonoDragons.Core.Render.Texture;

namespace MonoDragons.Core.Tiled
{
    public class OrthographicMovingObjectFactory
    {
        public GameObject CreateMovingObject(Tsx tsx, Vector2 location, ZIndex zIndex)
        {
            return Entity.Create(new Transform2(location, new Size2(tsx.TileWidth, tsx.TileHeight), zIndex))
                .Add((o, r) => new Texture(r.LoadTexture(tsx.ImageSource, o)))
                .Add(o => new Animation(0, o.Get<Texture>()))
                .Add(o => CreateMotionAnimationStates(tsx, o.Get<Texture>().Value))
                .Add(new Motion2(new Velocity2()))
                .Add(new Collision())
                .Add(new BoxCollider(Transform2.Zero))
                .Add(new MotionState())
                .Add(CreateBoxColliderStates(tsx));
        }

        private MotionAnimationStates CreateMotionAnimationStates(Tsx tsx, Texture2D texture)
        {
            return new MotionAnimationStates(
                CreateAnimation(tsx, tsx.Sprites.Where(x => x.IsStanding && x.Rotation.Equals(Rotation2.Up)).Select(x => x.Id).ToList(), texture),
                CreateAnimation(tsx, tsx.Sprites.Where(x => x.IsStanding && x.Rotation.Equals(Rotation2.Right)).Select(x => x.Id).ToList(), texture),
                CreateAnimation(tsx, tsx.Sprites.Where(x => x.IsStanding && x.Rotation.Equals(Rotation2.Down)).Select(x => x.Id).ToList(), texture),
                CreateAnimation(tsx, tsx.Sprites.Where(x => x.IsStanding && x.Rotation.Equals(Rotation2.Left)).Select(x => x.Id).ToList(), texture),
                CreateAnimation(tsx, tsx.Sprites.Where(x => x.Rotation.Equals(Rotation2.Up)).Select(x => x.Id).ToList(), texture),
                CreateAnimation(tsx, tsx.Sprites.Where(x => x.Rotation.Equals(Rotation2.Right)).Select(x => x.Id).ToList(), texture),
                CreateAnimation(tsx, tsx.Sprites.Where(x => x.Rotation.Equals(Rotation2.Down)).Select(x => x.Id).ToList(), texture),
                CreateAnimation(tsx, tsx.Sprites.Where(x => x.Rotation.Equals(Rotation2.Left)).Select(x => x.Id).ToList(), texture));
        }

        private Animation CreateAnimation(Tsx tsx, List<int> spriteIds, Texture2D texture)
        {
            return new Animation(tsx.MsPerFrame, spriteIds.Select(x => CreateTexture(tsx, x, texture)).ToArray());
        }

        private Texture CreateTexture(Tsx tsx, int id, Texture2D texture)
        {
            return new Texture(texture, new SpriteSheetRectangle(id, tsx.Columns, tsx.TileWidth, tsx.TileHeight, tsx.Spacing).Get());
        }

        private MotionBoxColliderStates CreateBoxColliderStates(Tsx tsx)
        {
            return new MotionBoxColliderStates(
                CreateBoxCollider(tsx.Sprites.Where(x => x.IsStanding && x.Rotation.Equals(Rotation2.Up)).ToList()),
                CreateBoxCollider(tsx.Sprites.Where(x => x.IsStanding && x.Rotation.Equals(Rotation2.Right)).ToList()),
                CreateBoxCollider(tsx.Sprites.Where(x => x.IsStanding && x.Rotation.Equals(Rotation2.Down)).ToList()),
                CreateBoxCollider(tsx.Sprites.Where(x => x.IsStanding && x.Rotation.Equals(Rotation2.Left)).ToList()),
                CreateBoxCollider(tsx.Sprites.Where(x => x.Rotation.Equals(Rotation2.Up)).ToList()),
                CreateBoxCollider(tsx.Sprites.Where(x => x.Rotation.Equals(Rotation2.Right)).ToList()),
                CreateBoxCollider(tsx.Sprites.Where(x => x.Rotation.Equals(Rotation2.Down)).ToList()),
                CreateBoxCollider(tsx.Sprites.Where(x => x.Rotation.Equals(Rotation2.Left)).ToList()));
        }

        private BoxCollider CreateBoxCollider(List<TsxSprite> sprites)
        {
            //TODO: allow multiple boxes
            return new BoxCollider(new Transform2(sprites.First(x => x.CollisionBoxes.Any()).CollisionBoxes.First()));
        }
    }
}
