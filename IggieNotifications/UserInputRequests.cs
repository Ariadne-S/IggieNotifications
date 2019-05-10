using System;
using System.Collections.Generic;
using System.Text;
using Serilog;

namespace IggieNotifications
{
    public class UserInputRequests
    {

        public interface IUserInput
        {
            string GetInput();
        }

        public class ConsoleInput : IUserInput
        {
            public string GetInput()
            {
                return Console.ReadLine();
            }
        }

        public static int UserIntInput(string requestMessage, ILogger log, IUserInput input)
        {
            Console.WriteLine(requestMessage);
            log.Information("Message printed to console '{0}'", requestMessage);
            var inputString = input.GetInput();
            log.Information("User has inputted [{0}]", inputString);
            var inputInt = 0;
            while (!int.TryParse(inputString, out inputInt))
            {
                Console.WriteLine("Your input must be a number, please try again.");
                log.Information("Converting user input to int has not been successful");
                return UserIntInput(requestMessage, log, input);
            }
            log.Information("User input has sucessfully been converted to an int [{0}]", inputInt);
            return inputInt;
        }

        public static decimal UserDecInput(string requestMessage, ILogger Log, IUserInput input)
        {
            Console.WriteLine(requestMessage);
            Log.Information("Message printed to console '{0}'", requestMessage);
            var inputString = input.GetInput();
            Log.Information("User has inputted [{0}]", inputString);
            decimal inputDec = 0m;

            while (!decimal.TryParse(inputString, out inputDec)) {
                Console.WriteLine("Your input must be a number, please try again.");
                Log.Information("Converting user input to decimal has not been successful");
                return UserDecInput(requestMessage, Log, input);
            }
            Log.Information("User input has sucessfully been converted to an decimal [{0}]", inputDec);
            return inputDec;
        }

        public static int UserIntInput(string requestMessage, ILogger Log, IUserInput input, int min, int max)
        {
            Console.WriteLine(requestMessage);
            Log.Information("Message printed to console '{0}'", requestMessage);
            var inputString = input.GetInput();
            Log.Information("User has inputted [{0}]", inputString);
            var inputInt = 0;
            while (
                !(int.TryParse(inputString, out inputInt)
                && (inputInt >= min && inputInt <= max))
                ) {
                Console.WriteLine("Your input must be a number and be between {0} and {1}, please try again.", min, max);
            }
            Log.Information("User input has sucessfully been validated [{0}]", inputInt);
            return inputInt;
        }

        
    }
}
