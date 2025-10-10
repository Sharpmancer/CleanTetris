using System.Runtime.CompilerServices;

namespace Libs.Bitmasks
{
    public static class Int64Extensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasExactlyOneFlag(this long mask) => 
            mask != 0 && (mask & (mask - 1)) == 0;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool EmptyOrSingleFlag(this long mask) =>
            Empty(mask) || HasExactlyOneFlag(mask);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AreDisjoint(this long a, long b) => 
            (a & b) == 0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long FirstSetFlag(this long mask) => 
            mask & -mask;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ContainsFlag(this long mask, long flag) =>
            (mask & flag) != 0;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long RemoveFlags(this long mask, long flags) =>
            mask & ~flags;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Empty(this long mask) => 
            mask == 0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool NotEmpty(this long mask) => 
            !Empty(mask);
    }
}