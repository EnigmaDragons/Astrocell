using System;
using System.Collections.Generic;
using System.Reflection;

namespace MonoDragons.Core.Common.Reflection
{
    public static class PublicProperties
    {
        private static readonly Map<Type, List<PropertyInfo>> _properties;

        static PublicProperties()
        {
            _properties = new Map<Type, List<PropertyInfo>>();
        }

        public static Optional<T> GetPropertyValue<T>(this object obj, string property)
        {
            var props = obj.GetType().GetProperties();
            return (T)obj.GetType().GetProperty(property, typeof(T)).GetValue(obj);
        }
    }
}
