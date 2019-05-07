using System.Collections.Generic;

namespace Infrastructure.Extensions
{
    public static class IReadOnlyListExtension
    {
        public static int IndexOf<T>(this IReadOnlyList<T> list, T toFind)
        {
            for (var i = 0; i < list.Count; ++i)
                if (list[i].Equals(toFind))
                    return i;

            return -1;
        }
    }
}
