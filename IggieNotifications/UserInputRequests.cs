using System;
using System.Collections.Generic;
using System.Text;
using Serilog;

namespace IggieNotifications
{
    public class UserInputRequests
    {

        public static int UserIntInput(string requestMessage, ILogger Log)
        {
            Console.WriteLine(requestMessage);
            Log.Information("Message printed to console '{0}'", requestMessage);
            var input = Console.ReadLine();
            Log.Information("User has inputted [{0}]", input);
            var inputInt = 0;
            while (!int.TryParse(input, out inputInt))
            {
                Console.WriteLine("Your input must be a number, please try again.");
                Log.Information("Converting user input to int has not been successful");
                return UserIntInput(requestMessage, Log);
            }
            Log.Information("User input has sucessfully been converted to an int [{0}]", inputInt);
            return inputInt;
        }

        public static decimal UserDecInput(string requestMessage, ILogger Log)
        {
            Console.WriteLine(requestMessage);
            Log.Information("Message printed to console '{0}'", requestMessage);
            var input = Console.ReadLine();
            Log.Information("User has inputted [{0}]", input);
            decimal inputDec = 0m;

            while (!decimal.TryParse(input, out inputDec)) {
                Console.WriteLine("Your input must be a number, please try again.");
                Log.Information("Converting user input to int has not been successful");
                return UserIntInput(requestMessage, Log);
            }
            Log.Information("User input has sucessfully been converted to an decimal [{0}]", inputDec);
            return inputDec;
        }

        public static int UserIntInput(string requestMessage, ILogger Log, int min, int max)
        {
            Console.WriteLine(requestMessage);
            Log.Information("Message printed to console '{0}'", requestMessage);
            var input = Console.ReadLine();
            Log.Information("User has inputted [{0}]", input);
            var inputInt = 0;
            while (
                !(int.TryParse(input, out inputInt)
                && (inputInt >= min && inputInt <= max))
                ) {
                Console.WriteLine("Your input must be a number and be between {0} and {1}, please try again.", min, max);
            }
            Log.Information("User input has sucessfully been validated [{0}]", inputInt);
            return inputInt;
        }

        
    }
}
