using System;
using System.Linq;
using Xunit;
using Serilog;
using IggieNotifications;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace XUnitTestIggieNotifications
{
    public class NightlyTemperatureTests
    {
        private readonly ILogger _log;

        public NightlyTemperatureTests(Xunit.Abstractions.ITestOutputHelper output)
        {
            _log = output.ToLogger();
        }

        [Fact]
        public async Task NightlyMinTemperatureTests()
        {
            _log.Information("NightlyMinTemperatureTests started");

            var testFileNameList = new List<string> {
                "TemperatureTestData.json",
                "TempTestData2019-04-28.json",
                "TempTestData2019-04-30.json"
            };

            var expectations = new List<NightlyTemperature> {
                new NightlyTemperature {
                    ExpectedMinNightlyTemperature = 13.9m,
                    ExpectedMinNightlyTemperatureTime = new DateTime(2019, 04, 26, 05, 00, 00)
                },
                new NightlyTemperature {
                    ExpectedMinNightlyTemperature = 12.9m,
                    ExpectedMinNightlyTemperatureTime = new DateTime(2019, 04, 29, 05, 00, 00)
                },
                new NightlyTemperature {
                    ExpectedMinNightlyTemperature = 14.9m,
                    ExpectedMinNightlyTemperatureTime = new DateTime(2019, 05, 01, 05, 00, 00)
                }
            };

            var index = 1;
            foreach (var (file, expectation) in testFileNameList.Zip(expectations, (a, b) => (a, b))) {
                var api = new FileWeatherApi(_log, "TestData/" + file);
                var result = await WeatherForecastProcessor.GetForcastData(_log, 8190, DateTime.Now, api);
                _log.Information("{index} [Retrived] Min Tempt of {MinNightlyTemperature} at {MinNightlyTemperatureTime}\t[Expected] Min Tempt of {ExpectedMinNightlyTemperature} at {ExpectedMinNightlyTemperatureTime}", index.ToString(), result.ExpectedMinNightlyTemperature, result.ExpectedMinNightlyTemperatureTime.ToString(), expectation.ExpectedMinNightlyTemperature, expectation.ExpectedMinNightlyTemperatureTime.ToString());

                Assert.Equal(expectation.ExpectedMinNightlyTemperature, result.ExpectedMinNightlyTemperature);
                Assert.Equal(expectation.ExpectedMinNightlyTemperatureTime, result.ExpectedMinNightlyTemperatureTime);
                index += 1;
            }
        }

    }
}

