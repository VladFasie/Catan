using System;
using System.Text;

namespace Infrastructure.Extensions
{
    public static class RandomExtension
    {
        private static string _lowers = "abcdefghijklmnopqrstuvwxyz";
        private static string _uppers = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static string _digits = "0123456789";

        public static Tuple<int, int> RollDices(this Random rnd)
        {
            var first = rnd.Next(1, 7);
            var second = rnd.Next(1, 7);

            var result = new Tuple<int, int>(first, second);
            return result;
        }

        public static string GenerateId(this Random rnd, int length, bool digits = true, bool upperCase = true, bool lowerCase = true)
        {
            if (!digits && !lowerCase && !upperCase)
                throw new Exception("no characters");

            var characters = "";
            if (digits)
                characters += _digits;
            if (lowerCase)
                characters += _lowers;
            if (upperCase)
                characters += _uppers;

            var builder = new StringBuilder();
            for (var i = 0; i < length; ++i)
                builder.Append(characters[rnd.Next(0, characters.Length)]);

            return builder.ToString();
        }
    }
}
