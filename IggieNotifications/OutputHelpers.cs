using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace IggieNotifications
{
    public static class OutputHelpers
    {
        public static string JoinWords(List<string> words)
        {
            var builder = new StringBuilder();
            var onlyOneWord = words.Count == 1;
            var lastWordIndex = words.Count - 1;
            var secondLastWordIndex = lastWordIndex - 1;

            var index = 0;
            foreach (string word in words) {
                if (onlyOneWord) {
                    builder.Append(word);
                } else if (index != lastWordIndex) {
                    builder.Append(word);
                    if (!onlyOneWord && index != secondLastWordIndex) {
                        builder.Append(", ");
                    }
                } else {
                    builder.Append(" and ").Append(word);
                }

                index++;
            }

            return builder.ToString();
        }
        public static string CasedString(string input)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input);
        }

        public static string CapitaliseFirstLetter(string input)
        {
            if (string.IsNullOrEmpty(input)) {
                return string.Empty;
            }
            char[] a = input.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }

    }

}

