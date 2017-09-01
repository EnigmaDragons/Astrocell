using System;

namespace Astrocell.Battles.Effects
{
    public static class StatMath
    {
        public static int StatMultiplyBy(this int value, float factor)
        {
            return Convert.ToInt32(Math.Ceiling(value * factor));
        }
    }
}
