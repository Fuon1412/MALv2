using System.Text.RegularExpressions;

namespace back_end.Utils
{
    public static partial class InputValidator
    {
        private static readonly Regex SqlInjectionPattern = MyRegex();

        [GeneratedRegex(@"(?i)(\b(SELECT|INSERT|UPDATE|DELETE|DROP|UNION|--|;|#|')\b)", RegexOptions.Compiled, "en-US")]
        private static partial Regex MyRegex();

        public static bool ContainsSqlInjection(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            return SqlInjectionPattern.IsMatch(input);
        }
    }
}