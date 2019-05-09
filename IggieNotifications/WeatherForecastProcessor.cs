using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using System.Linq;

namespace IggieNotifications
{
    public interface IWeatherApi
    {
        Task<string> GetForcastTemperatureData(DateTime startDate, int locationId);
    }

    public class HttpWeatherApi : IWeatherApi
    {
        private readonly ILogger log;
        private readonly string apiKey;

        public HttpWeatherApi(ILogger log, string apiKey)
        {
            this.log = log;
            this.apiKey = apiKey;
        }

        public async Task<string> GetForcastTemperatureData(DateTime startDate, int locationId)
        {
            var startDateFormatted = startDate.ToString("yyyy-MM-dd");
            log.Information("The start date that will be used for API Forecast query is {0}", startDateFormatted);

            using (HttpClient client = new HttpClient()) {
                HttpResponseMessage response = await client.GetAsync($"https://api.willyweather.com.au/v2/{apiKey}/locations/{locationId}/weather.json?forecasts=temperature&days=2&startDate={startDateFormatted}");
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                log.Information("Forecast data has been retrieved from API");
                return responseBody;
            }
        }
    }

   public class FileWeatherApi : IWeatherApi
    {
        private readonly ILogger log;
        private readonly string file;

        public FileWeatherApi(ILogger log, string file)
        {
            this.log = log;
            this.file = file;
        }

        public async Task<string> GetForcastTemperatureData(DateTime startDate, int locationId)
        {
            var responseBody = await File.ReadAllTextAsync(file);
            log.Information("Forecast data has been retrieved from {file}", file);
            return responseBody;
        }
    }

    public class WeatherForecastProcessor
    {
        internal static async Task GetForecastData(ILogger log, IConfigurationRoot configuration)
        {
            var apiKey = configuration["api-key"];
            var devMode = string.Equals(configuration["devMode"], "true", StringComparison.InvariantCultureIgnoreCase);
            log.Information("devMode is {devMode}", devMode);

            var apiLocationId = 8190;
            var now = DateTime.Now;

            IWeatherApi api = null;

            if (devMode) {
                api = new FileWeatherApi(log, "TestData/temperatureTestData.json");
            } else {
                api = new HttpWeatherApi(log, apiKey);
            }

            NightlyTemperature nightlyTemperatureResults = await GetForcastData(log, apiLocationId, now, api);

            Console.WriteLine("The expected minimum temperature will be {0} at {1}", nightlyTemperatureResults.ExpectedMinNightlyTemperature, nightlyTemperatureResults.ExpectedMinNightlyTemperatureTime.ToString("h:mm tt"));
        }

        public static async Task<NightlyTemperature> GetForcastData(ILogger log, int apiLocationId, DateTime now, IWeatherApi api)
        {
            var forecastTempResponse = await GetForecastTemperatureData(log, api, now, apiLocationId);
            var nightlyTemperatureResults = ProcessResults(forecastTempResponse);
            return nightlyTemperatureResults;
        }

        private static async Task<WillyWeatherForecastTempResponse> GetForecastTemperatureData(ILogger log, IWeatherApi api, DateTime currentDateforApi, int apiLocationId)
        {
            var responseBody = await api.GetForcastTemperatureData(currentDateforApi, apiLocationId);
            var forcastResponse = JsonConvert.DeserializeObject<WillyWeatherForecastTempResponse>(responseBody);
            log.Information("Forecast has been successefully Deserialised");

            return forcastResponse;
        }

        private static NightlyTemperature ProcessResults(WillyWeatherForecastTempResponse forcastResponse)
        {
            List<EntryResults> nightEntries = GetNightEntries(forcastResponse);
            var expectedMinNightlyTemperature = nightEntries.Min(e => e.Temperature);
            var expectedMinNightlyTemperatureTime = nightEntries.First(e => e.Temperature == expectedMinNightlyTemperature).DateTime;

            var nightlyTemperatureResults = new NightlyTemperature() {
                NightEntries = nightEntries,
                ExpectedMinNightlyTemperature = expectedMinNightlyTemperature,
                ExpectedMinNightlyTemperatureTime = expectedMinNightlyTemperatureTime
            };
       
            return nightlyTemperatureResults;
        }

        private static List<EntryResults> GetNightEntries(WillyWeatherForecastTempResponse forcastResponse)
        {
            var date1entryResults = forcastResponse.Forecasts.Temperature.Days[0].Entries.Where(e => e.DateTime.Hour >= 12).ToList();
            var date2entryResults = forcastResponse.Forecasts.Temperature.Days[1].Entries.Where(e => e.DateTime.Hour <= 12).ToList();

            var allNightEntries = date1entryResults.Concat(date2entryResults);

            var nightEntries = allNightEntries.Where(e => DateHelperFunctions.IsNightTime(e.DateTime)).ToList();
            return nightEntries;
        }
    }
}
