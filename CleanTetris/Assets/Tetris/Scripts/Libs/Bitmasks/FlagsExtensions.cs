using System;
using System.Runtime.CompilerServices;

namespace Libs.Bitmasks
{
        public static class FlagsExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasAnyFlags<T>(this T value, T mask) where T : struct, Enum
            => EnumBits.ToInt64Bits(value).ContainsFlag(EnumBits.ToInt64Bits(mask));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasAnyFlags<T>(this T value) where T : struct, Enum
            => !value.Empty();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Empty<T>(this T value) where T : struct, Enum
            => Int64Extensions.Empty(EnumBits.ToInt64Bits(value));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool EmptyOrExactlyOneFlag<T>(this T value) where T : struct, Enum
            => value.Empty() || value.HasExactlyOneFlag();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasExactlyOneFlag<T>(this T value) where T : struct, Enum
            => Int64Extensions.HasExactlyOneFlag(EnumBits.ToInt64Bits(value));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasAllFlags<T>(this T value, T mask) where T : struct, Enum
        {
            var v = EnumBits.ToInt64Bits(value);
            var m = EnumBits.ToInt64Bits(mask);
            return (v & m) == m;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T AddFlags<T>(this T value, T mask) where T : struct, Enum
        {
            var v = EnumBits.ToInt64Bits(value);
            var m = EnumBits.ToInt64Bits(mask);
            return EnumBits.FromInt64Bits<T>(v | m);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T RemoveFlags<T>(this T value, T mask) where T : struct, Enum
        {
            var v = EnumBits.ToInt64Bits(value);
            var m = EnumBits.ToInt64Bits(mask);
            return EnumBits.FromInt64Bits<T>(Int64Extensions.RemoveFlags(v, m));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T FirstSetFlag<T>(this T value) where T : struct, Enum
        {
            var bits = EnumBits.ToInt64Bits(value);
            var first = Int64Extensions.FirstSetFlag(bits);
            return EnumBits.FromInt64Bits<T>(first);
        }
    }
}