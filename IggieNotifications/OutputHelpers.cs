using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace IggieNotifications
{
    public static class OutputHelpers
    {
        public static string addCommasandAnd(List<string> List)
        {
            var outputString = "";
            /*
            var listLength = List.Count;
            var loop = 1;
            var lastItem = List[-1];
            List<string> listWithoutItem = List.RemoveAt(listLength -1);


            StringBuilder builder = new StringBuilder();
            foreach (string element in List) {
                if (loop <= listLength) {
                    builder.Append(element).Append(", ");
                }
                else builder.Append("and ").Append(element);
            }

            string result = builder.ToString();
            Console.WriteLine(result);

            */
            return outputString;
        }
        public static string CasedString(string input)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input);
        }
    }

   
}
