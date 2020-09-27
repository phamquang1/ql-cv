using Abp.Timing;
using System;
using System.Globalization;

namespace NCCTalentManagement.Utils
{
    public class DateTimeUtils
    {
        public const long TOTAL_MILLIS_IN_DAY = 86400000;
        public static DateTime LocalFirstDay1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).ToUniversalTime().AddHours(7);

        // All now function use Clock.Provider.Now
        public static DateTime GetNow()
        {
            return Clock.Provider.Now;
        }

        public static bool CustomTryParseExact(string s, string format, out DateTime result)
        {
            return DateTime.TryParseExact(s, format, CultureInfo.CurrentCulture, DateTimeStyles.AssumeLocal, out result);
        }

        public static DateTime DateTimeFromMilliseconds(long millis)
        {
            return LocalFirstDay1970.AddMilliseconds(millis);
        }
    }
}
