using System;
using System.Collections.Generic;
using System.Text;

namespace IggieNotifications
{
    class UserSpecifications
    {

        public UserSpecifications()
        {
            Console.WriteLine("Welcome to the User Specifications");
        }
        public decimal SetHighestTempforJumper()
        {
            Console.WriteLine("At what nightly temperature would you like to recieve suggestions to put a jumper on your italian greyhounds?");
             var input = Console.ReadLine();
            return 0;
        }

     
        public decimal HighestTempforJumper { get; set; }
        public decimal LowestTempforHeatWarning { get; set; }

    }
}
