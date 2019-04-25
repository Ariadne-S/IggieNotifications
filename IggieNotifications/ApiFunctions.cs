using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace IggieNotifications
{
    internal class ApiFunctions
    {
        internal static async Task GetForecastData(ILogger Log, IConfigurationRoot configuration)
        {
            var apiKey = configuration["api-key"];
            var devMode = string.Equals(configuration["devMode"], "true", StringComparison.InvariantCultureIgnoreCase);
            Log.Information("devmode is {devMode}", devMode);


            using (HttpClient client = new HttpClient()) {
                // Call asynchronous network methods in a try/catch block to handle exceptions
                try {

                    string responseBody = null;

                    if (devMode) {
                        var file = "TestData/temperatureTestData.json";
                        responseBody = await File.ReadAllTextAsync(file);
                        Log.Information("Forecast data has been retrieved from {file}", file);
                    } else {
                        HttpResponseMessage response = await client.GetAsync($"https://api.willyweather.com.au/v2/{apiKey}/locations/8190/weather.json?forecasts=temperature&days=2&startDate=2019-04-25");
                        response.EnsureSuccessStatusCode();
                        responseBody = await response.Content.ReadAsStringAsync();
                        Log.Information("Forecast data has been retrieved from API");
                    }

                    var forcastResponse = JsonConvert.DeserializeObject<WillyWeatherForecastTempResponse>(responseBody);
                    Log.Information("Forecast has been successefully Deserialised");

                    foreach (var day in forcastResponse.Forecasts.Temperature.Days) {
                        foreach (var entry in day.Entries) {
                            var entryDateTime = entry.DateTime.ToString("f");
                            var entryTemperature = entry.Temperature.ToString();
                            Console.WriteLine("The temperature at {0} is {1} celsius", entryDateTime, entryTemperature);
                        }
                    }

                } catch (HttpRequestException e) {
                    Log.Error(e, "Top level exception, application will now exit");
                }

                Console.ReadLine();
            }
        }
    }
}
