using System;
using System.Runtime.CompilerServices;

namespace Libs.Bitmasks
{
    internal static class EnumBits
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long ToInt64Bits<T>(T value) where T : struct, Enum
        // unchecked - treating bits, not semantics
            => unchecked((long)Convert.ToUInt64(value));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T FromInt64Bits<T>(long bits) where T : struct, Enum
        // unchecked - treating bits, not semantics
            => (T)Enum.ToObject(typeof(T), unchecked((ulong)bits));
    }
}