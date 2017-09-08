using System;
using Microsoft.Xna.Framework;

namespace MonoDragons.Core.PhysicsEngine
{
    public class Transform2
    {
        public static Transform2 Zero = new Transform2(Vector2.Zero, Size2.Zero);

        public Vector2 Location { get; set; } = Vector2.Zero;
        public Rotation2 Rotation { get; set; } = Rotation2.None;
        public float Scale { get; set; } = 1.0f;
        public Size2 Size { get; set; } = Size2.Zero;
        public ZIndex ZIndex { get; set; } = new ZIndex { Value = 1 };

        public Transform2()
        {
        }

        public Transform2(Rectangle rectangle)
            : this(rectangle, new ZIndex()) { }

        public Transform2(Rectangle rectangle, ZIndex zIndex) 
            : this(new Vector2(rectangle.Location.X, rectangle.Location.Y), Rotation2.Default, new Size2(rectangle.Size.X, rectangle.Size.Y), 1, zIndex) {}

        public Transform2(float scale)
            : this(Vector2.Zero, Rotation2.Default, Size2.Zero, scale) { }

        public Transform2(Rotation2 rotation)
            : this(Vector2.Zero, rotation, Size2.Zero, 1) { }

        public Transform2(Vector2 location)
            : this(location, Rotation2.Default, Size2.Zero, 1) { }

        public Transform2(Vector2 location, float scale)
            : this(location, Rotation2.Default, Size2.Zero, scale) { }

        public Transform2(Size2 size)
            : this(Vector2.Zero, Rotation2.Default, size, 1) { }

        public Transform2(Vector2 location, Rotation2 rotation, float scale)
            : this(location, rotation, Size2.Zero, scale) { }

        public Transform2(Vector2 location, Size2 size)
            : this(location, Rotation2.Default, size, 1) { }

        public Transform2(Vector2 location, Rotation2 rotation, Size2 size, float scale)
            : this(location, rotation, size, scale, new ZIndex()) { }

        public Transform2(Vector2 location, Rotation2 rotation, Size2 size, float scale, int zIndex)
            : this(location, rotation, size, scale, new ZIndex(zIndex)) { }

        public Transform2(Vector2 location, Rotation2 rotation, Size2 size, float scale, ZIndex zIndex)
        {
            Location = location;
            Rotation = rotation;
            Size = size;
            Scale = scale;
            ZIndex = zIndex;
        }

        public Vector2 Center
        {
            get { return new Vector2(Location.X + (Size.Width / 2f), Location.Y + (Size.Height / 2f)); }
            set { Location = new Vector2(value.X - (Size.Width / 2f), value.Y - (Size.Height / 2f)); }
        }

        public void SetTo(Transform2 other)
        {
            Location = other.Location;
            Rotation = other.Rotation;
            Size = other.Size;
            Scale = other.Scale;
            ZIndex = other.ZIndex;
        }

        public bool Intersects(Point point)
        {
            return ToRectangle().Contains(point);
        }

        public bool Intersects(Transform2 other)
        {
            return ToRectangle().Intersects(other.ToRectangle());
        }

        public Transform2 WithSize(Size2 size)
        {
            return new Transform2(Location, Rotation, size, Scale, ZIndex);
        }

        public Rectangle ToRectangle()
        {
            return new Rectangle((Location * Scale).ToPoint(), (Size * Scale).ToPoint());
        }

        public Transform2 WithPadding(int x, int y)
        {
            return WithPadding(new Size2(x, y));
        }

        public Transform2 WithPadding(Size2 paddingAmount)
        {
            return new Transform2(Location + paddingAmount.ToVector(), Rotation, Size - (paddingAmount * 2), Scale, ZIndex);
        }

        public override string ToString()
        {
            return $"{Location} {Size} {Rotation} {Scale} {ZIndex}";
        }

        public static Transform2 operator +(Transform2 t1, Transform2 t2)
        {
            return new Transform2(t1.Location + t2.Location, t1.Rotation + t2.Rotation, t1.Size + t2.Size, t1.Scale * t2.Scale, t1.ZIndex);
        }

        public static Transform2 operator +(Transform2 t1, Vector2 by)
        {
            return new Transform2(t1.Location + by, t1.Rotation, t1.Size, t1.Scale, t1.ZIndex);
        }

        public static Transform2 operator +(Transform2 t1, float scale)
        {
            return new Transform2(t1.Location, t1.Rotation, t1.Size, t1.Scale * scale);
        }

        public static Transform2 Lerp(Transform2 t1, Transform2 t2, float amount)
        {
            return new Transform2(
                Vector2.Lerp(t1.Location, t2.Location, amount),
                Rotation2.Lerp(t1.Rotation, t2.Rotation, amount),
                Size2.Lerp(t1.Size, t2.Size, amount),
                MathHelper.Lerp(t1.Scale, t2.Scale, amount),
                Convert.ToInt32(MathHelper.Lerp(t1.ZIndex.Value, t2.ZIndex.Value, amount)));

        }

        public Transform2 ToScale(float scale)
        {
            return this + new Transform2(Vector2.Zero, Rotation2.Default, Size2.Zero, Scale / scale, ZIndex);
        }

        public Transform2 Expanded(Size2 size)
        {
            return new Transform2(Location - size.ToVector(), Rotation, Size + size * 2, Scale, ZIndex);
        }
    }
}
