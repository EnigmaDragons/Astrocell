using System;
using System.Collections.Generic;
using System.Linq;

namespace Astrocell.Battles
{
    public static class EnumerableExtensions
    {
        public static void PerformNTimes(this int count, Action action)
        {
            Enumerable.Range(0, count).ForEach(x => action());
        }

        public static IList<T> SelectN<T>(this int count, Func<int, T> selector)
        {
            return Enumerable.Range(0, count).Select(selector).ToList();
        }

        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            items.ToList().ForEach(action);
        }

        public static string CommaSeparated<T>(this IEnumerable<T> items, Func<T, string> valueSelector)
        {
            return string.Join(",", items.Select(valueSelector));
        }
    }
}
