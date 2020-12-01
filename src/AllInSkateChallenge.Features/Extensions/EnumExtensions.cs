using System;
using System.Linq;

namespace AllInSkateChallenge.Features.Extensions
{
    public static class EnumExtensions
    {
        public static TEnum Next<TEnum>(this TEnum value) where TEnum : struct
        {
            return Enum.GetValues(value.GetType()).Cast<TEnum>().Concat(new[] { default(TEnum) }).SkipWhile(e => !value.Equals(e)).Skip(1).First();
        }

        public static TEnum Previous<TEnum>(this TEnum value) where TEnum : struct
        {
            return Enum.GetValues(value.GetType()).Cast<TEnum>().Concat(new[] { default(TEnum) }).Reverse().SkipWhile(e => !value.Equals(e)).Skip(1).First();
        }
    }
}
