using System;
using System.Threading.Tasks;
using System.Net.Http;

using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace IggieNotifications
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appSettings.json", optional: false)
                .AddJsonFile("appSecrets.json", optional: true);
            var configuration = builder.Build();

            var apiKey = configuration["api-key"];

            using (HttpClient client = new HttpClient()) {
                // Call asynchronous network methods in a try/catch block to handle exceptions
                try {
                    HttpResponseMessage response = await client.GetAsync($"https://api.willyweather.com.au/v2/{apiKey}/locations/8190/weather.json?forecasts=temperature&days=2&startDate=2019-04-25");
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();

                    var forcastResponse = JsonConvert.DeserializeObject<WillyWeatherForecastTempResponse>(responseBody);

                    foreach (var day in forcastResponse.Forecasts.Temperature.Days) {
                        foreach (var entry in day.Entries) {
                            var entryDateTime = entry.DateTime.ToString("f");
                            var entryTemperature = entry.Temperature.ToString();
                            Console.WriteLine("The temperature at {0} is {1} celsius", entryDateTime, entryTemperature);
                        }
                    }

                } catch (HttpRequestException e) {
                    Console.WriteLine("\nException Caught!");
                    Console.WriteLine("Message :{0} ", e.Message);
                }

                Console.ReadLine();
            }
        }
    }
}

