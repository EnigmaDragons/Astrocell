using System;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Inputs;

namespace MonoDragons.Core.PhysicsEngine
{
    public struct Rotation2
    {
        public static Rotation2 None = new Rotation2(0);
        public static Rotation2 Default = new Rotation2(0);
        public static Rotation2 Up = new Rotation2(0);
        public static Rotation2 Right = new Rotation2(90);
        public static Rotation2 Down = new Rotation2(180);
        public static Rotation2 Left = new Rotation2(270);
        public static Rotation2 UpLeft = new Rotation2(315);
        public static Rotation2 UpRight= new Rotation2(45);
        public static Rotation2 DownLeft = new Rotation2(215);
        public static Rotation2 DownRight = new Rotation2(135);

        public float Degrees { get; }
        public float Radians { get; }

        public Rotation2(float degrees)
        {
            Degrees = degrees % 360;
            Radians = Degrees * .017453292519f;
        }

        public override bool Equals(object obj)
        {
            return Math.Abs(Degrees - ((Rotation2)obj).Degrees) < 0.01;
        }

        public override int GetHashCode()
        {
            return (int)Degrees;
        }

        public override string ToString()
        {
            return Degrees.ToString();
        }

        public static Rotation2 operator +(Rotation2 r1, Rotation2 r2)
        {
            return new Rotation2(r1.Degrees + r2.Degrees);
        }

        public static Rotation2 operator -(Rotation2 r1, Rotation2 r2)
        {
            return new Rotation2(r1.Degrees - r2.Degrees);
        }

        public Direction ToDirection()
        {
            var hDir = HorizontalDirection.None;
            if (0 < Degrees && Degrees < 180)
                hDir = HorizontalDirection.Right;
            if (180 < Degrees && Degrees < 360)
                hDir = HorizontalDirection.Left;
            var vDir = VerticalDirection.None;
            if (90 < Degrees && Degrees < 270)
                vDir = VerticalDirection.Down;
            if (270 < Degrees || Degrees < 90)
                vDir = VerticalDirection.Up;
            return new Direction(hDir, vDir);
        }

        public Rotation2 ToOrthogonal()
        {
            var degrees = (int)Degrees;
            if (338 <= Degrees || Degrees <= 22)
                return Rotation2.Up;
            if (23 <= Degrees && Degrees <= 67)
                return Rotation2.UpRight;
            if (68 <= Degrees && Degrees <= 112)
                return Rotation2.Right;
            if (113 <= Degrees && Degrees <= 157)
                return Rotation2.DownRight;
            if (158 <= Degrees && Degrees <= 202)
                return Rotation2.Down;
            if (203 <= Degrees && Degrees <= 247)
                return Rotation2.DownLeft;
            if (248 <= Degrees && Degrees <= 292)
                return Rotation2.Left;
            if (293 <= Degrees && Degrees <= 337)
                return Rotation2.UpLeft;
            throw new Exception("The developer made a horrible mistake!");
        }

        public Rotation2 ToCardinal()
        {
            if (315 <= Degrees || Degrees <= 22)
                return Rotation2.Up;
            if (45 < Degrees && Degrees < 135)
                return Rotation2.Right;
            if (135 <= Degrees && Degrees <= 225)
                return Rotation2.Down;
            if (225 < Degrees && Degrees < 315)
                return Rotation2.Left;
            throw new Exception("The developer made a horrible mistake!");
        }

        public Rotation2 ToCardinal(Rotation2 previousCardinalRotation)
        {
            var degrees = (int) Degrees;

            if (315 < degrees || degrees < 45)
                return Rotation2.Up;
            if (45 < degrees && degrees < 135)
                return Rotation2.Right;
            if (135 < degrees && degrees < 225)
                return Rotation2.Down;
            if (225 < degrees && degrees < 315)
                return Rotation2.Left;

            if (degrees == 45)
                return Math.Abs(previousCardinalRotation.Degrees) < 0.01 ? Rotation2.Right : Rotation2.Up;
            if (degrees == 135)
                return Math.Abs(previousCardinalRotation.Degrees - 180) < 0.01 ? Rotation2.Right : Rotation2.Down;
            if (degrees == 225)
                return Math.Abs(previousCardinalRotation.Degrees - 180) < 0.01 ? Rotation2.Left : Rotation2.Down;
            if (degrees == 315)
                return Math.Abs(previousCardinalRotation.Degrees) < 0.01 ? Rotation2.Left : Rotation2.Up;

            throw new Exception("The developer made a horrible mistake if you see this!");
        }

        public static float Difference(Rotation2 r1, Rotation2 r2)
        {
            var higher = Math.Max(r1.Degrees, r2.Degrees);
            var lower = Math.Min(r1.Degrees, r2.Degrees);
            return higher - lower;
        }

        public static Rotation2 Lerp(Rotation2 r1, Rotation2 r2, float amount)
        {
            var degrees = Difference(r1, r2);
            return new Rotation2(r1.Degrees + (amount * degrees));
        }
    }
}
