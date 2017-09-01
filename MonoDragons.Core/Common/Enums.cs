using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MonoDragons.Core.Common
{
    public static class Enums
    {
        public static string WithSpaces(this Enum src)
        {
            return Regex.Replace(src.ToString(), "(\\B[A-Z])", " $1");
        }

        public static IEnumerable<T> Values<T>(this Type enumType) 
            where T : struct, IConvertible
        {
            return (T[])Enum.GetValues(enumType);
        }

        public static void ForEach<T>(Action<T> enumAction) 
            where T : struct, IConvertible
        {
            Values<T>(typeof(T)).ForEach(enumAction);
        }
    }
}
