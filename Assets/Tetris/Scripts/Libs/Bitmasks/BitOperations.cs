namespace Libs.Bitmasks
{
    public static class BitOperations
    {
        /// <summary>
        /// Counts the number of trailing zero bits in a 32-bit unsigned integer.
        /// Returns 32 if the value is zero.
        /// </summary>
        public static int TrailingZeroCount(this uint value)
        {
            if (value == 0)
                return 32;

            var count = 0;
            while ((value & 1u) == 0)
            {
                value >>= 1;
                count++;
            }

            return count;
        }
    }
}