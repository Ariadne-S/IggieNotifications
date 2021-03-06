﻿using System;
using System.Threading.Tasks;
using System.Net.Http;

using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System.IO;
using Serilog;

namespace IggieNotifications
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .Enrich.FromLogContext()
                .WriteTo.Seq("http://localhost:5341")
                .CreateLogger();

            var builder = new ConfigurationBuilder()
                .AddJsonFile("appSettings.json", optional: false)
                .AddJsonFile("appSecrets.json", optional: true);
            var configuration = builder.Build();

            try {
                Log.Information("Starting app");
                await RunAsync(Log.Logger, configuration);
                return 0;
            } catch (Exception ex) {
                Log.Fatal(ex, "Top level exception, application will now exit");
                return 1;
            } finally {
                Log.Information("Application is about to close naturally");
                Log.CloseAndFlush();
                Console.ReadLine();
            }
        }

        private static async Task RunAsync(ILogger logger, IConfigurationRoot configuration)
        {
            UserSpecifications userSpecifications = null;

            if (Json.FileExists<UserSpecifications>()) {
                logger.Information("Prior userSpecification found");
                userSpecifications = Json.ReadJson<UserSpecifications>();
            } else {
                logger.Information("No prior userSpecification found");
                userSpecifications = new UserSpecifications();
                var userInterface = new UserInputRequests.ConsoleInput();
                userSpecifications.GetUserSpecifications(logger, userInterface);
                Json.WriteJson<UserSpecifications>(userSpecifications);
                logger.Information("userSpecification saved");
            }

            // Do something with {userSpecifications}
            var data = await WeatherForecastProcessor.RetrieveWeatherData(logger, configuration);
            logger.Information("The Weather Forecast Processor has retrieved Weather Data");
            JumperNotification jumperNotification = null;

            // get the data
            jumperNotification = new JumperNotification(userSpecifications, logger, data);
            var jumperNotificationMessage = jumperNotification.SetJumperIsNeeded();
            Console.WriteLine(jumperNotificationMessage);
        }
    }
}

