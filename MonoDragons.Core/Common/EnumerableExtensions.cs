using System;
using System.Collections.Generic;
using System.Linq;

namespace MonoDragons.Core.Common
{
    public static class EnumerableExtensions
    {
        public static bool None<T>(this IEnumerable<T> collection)
        {
            return !collection.Any();
        }

        public static IEnumerable<T> Copy<T>(this IEnumerable<T> collection)
        {
            return collection.ToList();
        }

        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            collection.ToList().ForEach(action);
        }

        public static void ForEachIndex<T>(this IEnumerable<T> collection, Action<T, int> indexAction)
        {
            var coll = collection.ToList();
            for (var i = 0; i < coll.Count; i++)
                indexAction(coll[i], i);
        }

        public static List<T> AsList<T>(this T obj)
        {
            return new List<T> {obj};
        }

        public static Optional<T> FirstAsOptional<T>(this IEnumerable<T> collection)
        {
            return collection.FirstOrDefault();
        }

        public static void PerformNTimes(this int count, Action action)
        {
            Enumerable.Range(0, count).ForEach(x => action());
        }

        public static IList<T> SelectN<T>(this int count, Func<int, T> selector)
        {
            return Enumerable.Range(0, count).Select(selector).ToList();
        }

        public static string CommaSeparated<T>(this IEnumerable<T> items, Func<T, string> valueSelector)
        {
            return string.Join(", ", items.Select(valueSelector));
        }

        public static void ForEach<TKey, TValue>(this IDictionary<TKey, TValue> items, Action<TValue> action)
        {
            foreach (var item in items)
                action(item.Value);
        }

        public static int DecrementCounter<TKey>(this IDictionary<TKey, int> counters, TKey key)
        {
            if (!counters.ContainsKey(key))
                counters[key] = 0;
            counters[key] = Math.Max(0, counters[key] - 1);
            return counters[key];
        }

        public static int IncrementCounter<TKey>(this IDictionary<TKey, int> counters, TKey key)
        {
            if (!counters.ContainsKey(key))
                counters[key] = 0;
            counters[key] = counters[key] + 1;
            return counters[key];
        }

        public static IList<TKey> KeysWhere<TKey, TValue>(this IDictionary<TKey, TValue> items, 
            Predicate<KeyValuePair<TKey, TValue>> condition)
        {
            var results = new List<TKey>();
            foreach (var item in items)
                if (condition(item))
                    results.Add(item.Key);
            return results;
        }
    }
}
