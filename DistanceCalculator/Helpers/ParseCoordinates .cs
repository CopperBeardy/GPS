using System.Globalization;
using System.Text.RegularExpressions;

namespace DistanceCalculator.Helpers
{
    public static partial class Helper
    {
        private static readonly string latitudePattern = @"^(?:\+|-)?(?:90(?:(?:(?:\.|,)0{1,6})?)|(?:[0-9]|[1-8][0-9])(?:(?:(?:\.|,)[0-9]{1,6})?))$";
        private static readonly string longitudePattern = @"^(?:\+|-)?(?:180(?:(?:(?:\.|,)0{1,6})?)|(?:[0-9]|[1-9][0-9]|1[0-7][0-9])(?:(?:(?:\.|,)[0-9]{1,6})?))$";

        public static bool TryParseLatitude(string input, out double latitude)
            => TryParseRegexToDouble(input, latitudePattern, out latitude);

        public static bool TryParseLongitude(string input, out double longitude)
            => TryParseRegexToDouble(input, longitudePattern, out longitude);

        private static bool TryParseRegexToDouble(string input, string regex, out double value)
        {
            var valid = !string.IsNullOrWhiteSpace(input) && Regex.IsMatch(input, regex);
            value = valid ? input.ToDouble() : 0;
            return valid;
        }
    }
}
