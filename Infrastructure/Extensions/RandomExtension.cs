using System;

namespace Infrastructure.Extensions
{
    public static class RandomExtension
    {
        public static Tuple<int, int> RollDices(this Random rnd)
        {
            var first = rnd.Next(1, 7);
            var second = rnd.Next(1, 7);

            var result = new Tuple<int, int>(first, second);
            return result;
        }
    }
}
