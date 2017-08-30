using System;
using System.Collections.Generic;
using MonoDragons.Core.Common;
using MonoDragons.Core.Common.Reflection;

namespace Astrocell.Battles.Characters
{
    public static class ReflectionStat
    {
        public static int Get<T>(this T stat, object fromObject) where T : struct, IConvertible
        {
            try
            {
                return fromObject.GetPropertyValue<int>(stat.ToString()).Value;
            }
            catch (Exception ex)
            {
                throw new KeyNotFoundException($"Unknown stat on type {fromObject.GetType()}: {stat}");
            }
        }
    }
}
