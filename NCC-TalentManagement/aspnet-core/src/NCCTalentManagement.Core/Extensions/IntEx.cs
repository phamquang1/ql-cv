namespace NCCTalentManagement.Extensions
{
    public static class IntEx
    {
        /// <summary>
        /// Determines if the Int input is between two values. 
        /// By default the lower and upper are NOT inclusive.
        /// An optional Inclusive bool is supplied if you would like the lower and upper bounds to be included in the test.
        /// </summary>
        public static bool Between(this int input, int lower, int upper, bool inclusive = false)
        {
            if (inclusive)
            {
                lower--;
                upper++;
            }
            return input > lower && input < upper;
        }

        public static bool BetweenEx(this int value, int? lo, int? hi)
        {
            return (lo == null || value >= lo) && (hi == null || value <= hi);
        }
    }
}
