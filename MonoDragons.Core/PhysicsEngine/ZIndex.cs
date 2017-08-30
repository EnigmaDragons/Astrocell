using System;

namespace MonoDragons.Core.PhysicsEngine
{
    public class ZIndex
    {
        public int Value { get; set; }

        public ZIndex() : this(1) {}

        public ZIndex(int value)
        {
            Value = value;
        }

        public float AsDepth()
        {
            return Math.Min(Value / (float)int.MaxValue, 1.0f);
        }
    }
}
