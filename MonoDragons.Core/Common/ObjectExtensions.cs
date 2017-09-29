using System;

namespace MonoDragons.Core.Common
{
    public static class ObjectExtensions
    {
        public static void If(this object obj, bool condition, Action action)
        {
            if (condition)
                action();
        }

        public static void If<T>(this T obj, Predicate<T> condition, Action action)
        {
            if (condition(obj))
                action();
        }

        public static void If<T>(this T obj, Predicate<T> condition, Action action, Action elseAction)
        {
            if (condition(obj))
                action();
            else
                elseAction();
        }

        public static void If<T>(this T obj, Predicate<T> condition, Action<T> action)
        {
            if (condition(obj))
                action(obj);
        }

        public static void IfEquals<T>(this T obj, object other, Action action)
        {
            if (obj.Equals(other))
                action();
        }

        public static void IfEquals<T>(this T obj, object other, Action<T> action)
        {
            if (obj.Equals(other))
                action(obj);
        }
    }
}
