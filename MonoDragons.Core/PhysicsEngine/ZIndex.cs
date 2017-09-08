using System;

namespace MonoDragons.Core.PhysicsEngine
{
    public sealed class ZIndex
    {
        public static ZIndex Max = new ZIndex(255);

        public int Value { get; set; }

        public ZIndex() : this(1) {}

        public ZIndex(int value)
        {
            Value = Normalize(value);
        }

        public bool Equals(ZIndex other)
        {
            return Value == other.Value;
        }

        public float AsDepth()
        {
            return Math.Min(Value / 256f, 1.0f);
        }

        public static implicit operator ZIndex(int value)
        {
            return new ZIndex(value);
        }

        public static ZIndex operator +(ZIndex z1, ZIndex z2)
        {
            return z1.Plus(z2.Value);
        }

        public static ZIndex operator -(ZIndex z1, ZIndex z2)
        {
            return z1.Plus(-z2.Value);
        }

        public ZIndex Plus(int i)
        {
            return new ZIndex(Normalize(Value + i));
        }

        private static int Normalize(int val)
        {
            return Math.Min(val, 255);
        }
    }
}
