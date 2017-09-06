using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Entities;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Render.Animations;
using MonoDragons.Core.Tiled.TmxLoading;
using Texture = MonoDragons.Core.Render.Texture;

namespace MonoDragons.Core.Tiled
{
    public class OrthographicCharacterFactory
    {
        public GameObject CreateCharacter(Tsx tsx, Vector2 location)
        {
            return Entity.Create(new Transform2(location, new Size2(tsx.TileWidth, tsx.TileHeight)))
                .Add((o, r) => new Texture(r.LoadTexture(tsx.ImageSource, o)))
                .Add(o => new Animation(0, o.Get<Texture>()))
                .Add(o => CreateCharacterAnimation(tsx, o.Get<Texture>().Value));
        }

        private MotionAnimationStates CreateCharacterAnimation(Tsx tsx, Texture2D texture)
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
    }
}
