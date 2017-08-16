using System;
using System.Collections.Generic;
using System.Linq;

namespace Astrocell.Battles
{
    public static class Rng
    {
        private static Random _instance = new Random(Guid.NewGuid().GetHashCode());

        public static void SetSeed(int seed)
        {
            _instance = new Random(seed);
        }

        public static bool Bool()
        {
            return Int(2) == 1;
        }

        public static int Int()
        {
            return Int(int.MaxValue);
        }

        public static int Int(int max)
        {
            return _instance.Next(max);
        }

        public static int Int(int min, int max)
        {
            return _instance.Next(min, max);
        }

        public static double Dbl()
        {
            return _instance.NextDouble();
        }

        public static KeyValuePair<T, T2> Random<T, T2>(this Dictionary<T, T2> dictionary)
        {
            return dictionary.ElementAt(Int(dictionary.Count));
        }

        public static T Random<T>(this T[] array)
        {
            return array[Int(array.Length)];
        }

        public static T Random<T>(this List<T> list)
        {
            return list[Int(list.Count)];
        }

        public static T Between<T>(T primary, T other, double primaryWeight)
        {
            return Dbl() <= primaryWeight ? primary : other;
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            for (var n = list.Count - 1; n > 1; n--)
            {
                var k = _instance.Next(n + 1);
                var value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static IList<T> Shuffled<T>(this IEnumerable<T> items)
        {
            var list = items.ToList();
            Shuffle(list);
            return list;
        }
    }
}
