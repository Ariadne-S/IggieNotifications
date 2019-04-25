using System;
using System.Collections.Generic;
using System.Text;

namespace IggieNotifications
{
    public class DateHelperFunctions
    {
        public static bool IsNightTime(DateTime dateTime)
        {
            return dateTime.Hour >= 18 || dateTime.Hour <= 6;
        }
        

    }
}
