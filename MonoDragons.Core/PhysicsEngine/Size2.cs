using System;
using Microsoft.Xna.Framework;

namespace MonoDragons.Core.PhysicsEngine
{
    public struct Size2
    {
        public static readonly Size2 Zero = new Size2(0, 0);

        public int Width { get; }
        public int Height { get; }

        public Size2(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public bool Equals(Size2 other)
        {
            return Width == other.Width && Height == other.Height;
        }

        public override string ToString()
        {
            return $"{Width}, {Height}";
        }

        public Point ToPoint()
        {
            return new Point(Width, Height);
        }

        public Vector2 ToVector()
        {
            return new Vector2(Width, Height);
        }

        public static Size2 operator +(Size2 s1, Size2 s2)
        {
            return new Size2(s1.Width + s2.Width, s1.Height + s2.Height);
        }

        public static Size2 operator -(Size2 s1, Size2 s2)
        {
            return new Size2(s1.Width - s2.Width, s1.Height - s2.Height);
        }

        public static Size2 operator *(Size2 size, float scale)
        {
            return new Size2((int) (size.Width * scale), (int) (size.Height * scale));
        }

        public static Size2 operator /(Size2 size, float scale)
        {
            return new Size2((int)(size.Width / scale), (int)(size.Height / scale));
        }

        public static Size2 Lerp(Size2 s1, Size2 s2, float amount)
        {
            return new Size2(
                Convert.ToInt32(MathHelper.Lerp(s1.Width, s2.Width, amount)),
                Convert.ToInt32(MathHelper.Lerp(s1.Height, s2.Height, amount)));
        }
    }
}
