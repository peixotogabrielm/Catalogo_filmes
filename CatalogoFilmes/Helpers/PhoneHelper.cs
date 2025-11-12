using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CatalogoFilmes.Helpers
{
    public static class PhoneHelper
    {
        public static string SanitizePhoneNumber(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return string.Empty;

            var digits = Regex.Replace(phone, @"\D", "");

            if (digits.StartsWith("55") && digits.Length > 11)
                digits = digits.Substring(2);

            if (digits.Length < 10 || digits.Length > 11)
                return string.Empty;

            return digits;
        }
        public static string FormatPhoneNumber(string phone)
        {
            var digits = SanitizePhoneNumber(phone);
            if (string.IsNullOrEmpty(digits))
                return string.Empty;

            if (digits.Length == 10)
                return $"({digits[..2]}) {digits[2..6]}-{digits[6..]}";

            if (digits.Length == 11)
                return $"({digits[..2]}) {digits[2..7]}-{digits[7..]}";

            return digits;
        }
    }
}