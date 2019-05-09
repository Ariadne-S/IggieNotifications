using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Serilog;

namespace IggieNotifications
{
    class UserSpecifications
    {

        public UserSpecifications(ILogger Log)
        {
            Console.WriteLine("Welcome to the User Specifications");
            NumOfIggies(Log);
            SetNamingConvention(Log);
            SetJumperThreshold(Log);
        }

        public int NumOfIggies(ILogger Log)
        {
            var requestMessage = "How many Italian Greyhounds do you own?";
            NumberOfIggies = UserInputRequests.UserIntInput(requestMessage, Log);
            Log.Information("Number of Iggies has been set to {0}", NumberOfIggies);
            return NumberOfIggies;
        }

        public string SetNamingConvention(ILogger Log)
        {
            if (NumberOfIggies >= 3) {
                NamingConvention = "your italian greyhounds";
                Log.Information("Naming Convention has been set to '{0}'", NamingConvention);
                return NamingConvention;
                }
            else {
                var dogNum = 1;
                var nameableIggie = Enumerable.Range(1, NumberOfIggies);
                IggieNames = new List<string>();
                foreach (int iggie in nameableIggie) {
                    var requestMessage = "What is your iggie's name?";
                    Console.WriteLine("[{0}] {1}", dogNum, requestMessage);
                    Log.Information("Message printed to console '[{0}] {1}'", dogNum, requestMessage);
                    var input = OutputHelpers.CasedString(Console.ReadLine());
                    Log.Information("User has inputed {0}", input);
                    IggieNames.Add(input);
                    Log.Information("{0} added to iggieNames list", input);
                    dogNum++;
                }
                if (IggieNames.Count == 2) {
                    NamingConvention = string.Join<string>(" and ", IggieNames);
                    Log.Information("Naming Convention has been set to '{0}'", NamingConvention);
                }
                return NamingConvention;
            }

        }

        public int SetJumperThreshold(ILogger Log)
        {
            var requestMessage = "At what nightly temperature would you like to recieve suggestions to put a jumper on your italian greyhounds?";
            JumperThreshold = UserInputRequests.UserIntInput(requestMessage, Log);
            return JumperThreshold;
        }

        
        public int NumberOfIggies { get; set; }
        public List<string> IggieNames { get; set; }
        public string NamingConvention { get; set; }
        public int JumperThreshold { get; set; }
        public decimal HeatWarningThreshold { get; set; }

    }
}
