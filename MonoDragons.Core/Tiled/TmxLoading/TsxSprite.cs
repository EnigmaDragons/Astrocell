using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using MonoDragons.Core.PhysicsEngine;

namespace MonoDragons.Core.Tiled.TmxLoading
{
    public struct TsxSprite
    {
        public int Id;
        public Rotation2 Rotation;
        public bool IsStanding;
        public List<Rectangle> CollisionBoxes;

        public static TsxSprite Create(XElement sprite)
        {
            var objGroup = sprite.Element(XName.Get("objectgroup"));
            return new TsxSprite
            {
                Id = new XValue(sprite, "id").AsInt(),
                Rotation = StringAsRotation2(new XProperty(objGroup, "Direction").AsString()),
                IsStanding = new XProperty(objGroup, "Standing").AsBool(),
                CollisionBoxes = new XBoxCollisions(sprite).Get()
            };            
        }

        private static Rotation2 StringAsRotation2(string direction)
        {
            if (direction.ToLower().Equals("up"))
                return Rotation2.Up;
            if (direction.ToLower().Equals("right"))
                return Rotation2.Right;
            if (direction.ToLower().Equals("down"))
                return Rotation2.Down;
            if (direction.ToLower().Equals("left"))
                return Rotation2.Left;
            return Rotation2.Default;
        }
    }
}
