using System;
using System.Text.RegularExpressions;

namespace NCCTalentManagement.Extensions
{
    public static class ExtensionMethod
    {
        private static readonly string[] VietnameseSigns = new string[]
        {
            "aAeEoOuUiIdDyY",
            "áàạảãâấầậẩẫăắằặẳẵ",
            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
            "éèẹẻẽêếềệểễ",
            "ÉÈẸẺẼÊẾỀỆỂỄ",
            "óòọỏõôốồộổỗơớờợởỡ",
            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
            "úùụủũưứừựửữ",
            "ÚÙỤỦŨƯỨỪỰỬỮ",
            "íìịỉĩ",
            "ÍÌỊỈĨ",
            "đ",
            "Đ",
            "ýỳỵỷỹ",
            "ÝỲỴỶỸ"
        };

        private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Local);

        public static string ToIdentifier(this string src, bool changeToLowerCase = false)
        {
            if (string.IsNullOrWhiteSpace(src))
                return src;
            if (changeToLowerCase)
                src = src.ToLower();
            src = Regex.Replace(src, @"[^A-Za-z0-9_]+", "-", RegexOptions.None);
            src = Regex.Replace(src, @"^[-_]+|[-_]+$", "");
            src = Regex.Replace(src, @"[^A-Za-z0-9-]+", "", RegexOptions.Compiled);
            return src;
        }

        public static DateTime FirstDayOfWeek(this DateTime dt)
        {
            var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
            var diff = dt.DayOfWeek - culture.DateTimeFormat.FirstDayOfWeek;

            if (diff < 0)
                diff += 7;

            return dt.AddDays(-diff).Date;
        }

        public static DateTime LastDayOfWeek(this DateTime dt) => dt.FirstDayOfWeek().AddDays(6);
        public static string RemoveSign4VietnameseString(this string str)
        {
            for (int i = 1; i < VietnameseSigns.Length; i++)
            {
                for (int j = 0; j < VietnameseSigns[i].Length; j++)
                    str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
            }
            return str;
        }

        public static DateTime TimeStampUnixToDate(this long timestamp)
        {
            return UnixEpoch.AddMilliseconds(timestamp).Date;
        }

        public static string StripHTML(this string input)
        {
            return Regex.Replace(input, "<.*?>", string.Empty);
        }
    }
}
