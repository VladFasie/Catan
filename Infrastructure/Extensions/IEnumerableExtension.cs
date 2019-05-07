using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Extensions
{
    public static class IEnumerableExtension
    {
        private static readonly Random rng = new Random();

        public static T RandomELement<T>(this IEnumerable<T> items)
        {
            var idx = rng.Next(items.Count());
            return items.ElementAt(idx);
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> items)
        {
            var list = items.ToList();
            var n = list.Count;
            while (n > 1)
            {
                n--;
                var k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }

            return list;
        }
    }
}
