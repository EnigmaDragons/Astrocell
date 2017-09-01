using System;
using System.Collections.Generic;
using MonoDragons.Core.Common.Reflection;

namespace Astrocell.Battles.Characters
{
    public static class ReflectionStat
    {
        public static int GetStatValue<T>(this object fromObject, T stat) where T : struct, IConvertible
        {
            try
            {
                return fromObject.GetPropertyValue<int>(stat.ToString()).Value;
            }
            catch (Exception)
            {
                throw new KeyNotFoundException($"Unknown stat on type {fromObject.GetType()}: {stat}");
            }
        }
    }
}
