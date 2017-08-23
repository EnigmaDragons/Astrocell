using Microsoft.Xna.Framework;

namespace MonoDragons.Core.Common
{
    public static class FancyMaths
    {
        public static Vector2 RotateAroundOrigin(Vector2 point, Vector2 origin, float degrees)
        {
            return Vector2.Transform(point - origin, Matrix.CreateRotationZ(MathHelper.ToRadians(degrees))) + origin;
        }
    }
}
