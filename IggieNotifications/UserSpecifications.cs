using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Serilog;

namespace IggieNotifications
{
    class UserSpecifications
    {

        public UserSpecifications()
        {
            IggyNames = new List<string>();
        }

        public void GetUserSpecifications(ILogger log, UserInputRequests.IUserInput input)
        {
            Console.WriteLine("Welcome to the User Specifications");
            NumOfIggies(log, input);
            SetNamingConvention(log, input);
            SetJumperThreshold(log, input);
            SetHeatWarningThreshold(log, input);
        }

        public int NumOfIggies(ILogger log, UserInputRequests.IUserInput input)
        {
            var requestMessage = "How many Italian Greyhounds do you own?";
            NumberOfIggies = UserInputRequests.UserIntInput(requestMessage, log, input);
            log.Information("Number of Iggies has been set to {0}", NumberOfIggies);
            return NumberOfIggies;
        }

        public string SetNamingConvention(ILogger log, UserInputRequests.IUserInput input)
        {
            if (NumberOfIggies >= 4) {
                NamingConvention = "your italian greyhounds";
                log.Information("Naming Convention has been set to '{0}'", NamingConvention);
                return NamingConvention;
                }
            else {
                var dogNum = 1;
                var nameableIggie = Enumerable.Range(1, NumberOfIggies);
                foreach (int iggie in nameableIggie) {
                    var requestMessage = "What is your iggy's name?";
                    Console.WriteLine("[{0}] {1}", dogNum, requestMessage);
                    log.Information("Message printed to console '[{0}] {1}'", dogNum, requestMessage);
                    var iggyName = OutputHelpers.CasedString(input.GetInput());
                    log.Information("User has inputed {0}", iggyName);
                    IggyNames.Add(iggyName);
                    log.Information("{0} added to iggyNames list", iggyName);
                    dogNum++;
                }
                if (IggyNames.Count < 4) {
                    NamingConvention = OutputHelpers.JoinWords(IggyNames);
                    log.Information("Naming Convention has been set to '{0}'", NamingConvention);
                }
                return NamingConvention;
            }
        }

        public decimal SetJumperThreshold(ILogger log, UserInputRequests.IUserInput input)
        {
            var requestMessage = "At what min nightly temperature would you like to recieve suggestions to put a jumper on your italian greyhounds?";
            JumperThreshold = UserInputRequests.UserDecInput(requestMessage, log, input);
            return JumperThreshold;
        }


        public decimal SetHeatWarningThreshold(ILogger log, UserInputRequests.IUserInput input)
        {
            var requestMessage = "At what daily high temperature would you like to recieve heat warnings?";
            HeatWarningThreshold = UserInputRequests.UserDecInput(requestMessage, log, input);
            return HeatWarningThreshold;
        }

        public int NumberOfIggies { get; set; }
        public List<string> IggyNames { get; set; }
        public string NamingConvention { get; set; }
        public decimal JumperThreshold { get; set; }
        public decimal HeatWarningThreshold { get; set; }


    }
}
